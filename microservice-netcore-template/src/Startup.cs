
using Amazon;
using Amazon.CloudWatchLogs;
using Amazon.Runtime;
using Api.Repository;
using Api.Repository.Impl;
using Api.Services;
using Api.Services.Impl;
using Api.Common.Schedulers;
using Api.Common.Error;
using Common.Attributes.HttpMiddleware;
using Common.Configuration;
using Common.Database.Conexion;
using Common.Logging;
using Common.Logging.Remote.Aws;
using Common.Middleware;
using Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.AwsCloudWatch;
using Serilog.Templates;
using System;
using System.Reflection;
using Common.Attributes;
using System.Linq;
using Api.Schedulers;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly LoggerUtil loggerUtil = new LoggerUtil();

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ValidateRequiredVariables();

            services.AddTransient<ApiGlobalConfiguration>();
            services.AddTransient<ApiConfiguration>();

            ConfigureLogService(services);

            services.AddSingleton<DatabaseManager>();
            
            services.AddTransient<HttpMiddlewareLogConfigurer>();
            services.AddTransient<HttpMiddlewareAuthConfigurer>();

            services.AddControllers();

            bool singletonJobFactoryWasLoaded = false;

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types) 
            {
                var customSchedulerAttributes = type.GetCustomAttributes(typeof(Scheduler), true).FirstOrDefault() as Scheduler;
                var customServiceAttributes = type.GetCustomAttributes(typeof(Service), true).FirstOrDefault() as Service;

                if(customSchedulerAttributes!=null){
                    foreach (var iType in type.GetInterfaces())
                    {
                        if(!singletonJobFactoryWasLoaded){
                            services.AddSingleton<SingletonJobFactory>();
                            singletonJobFactoryWasLoaded = true;
                        }

                        services.AddTransient(type);
                        Log.Information($"Adding as Transient {type}");
                    }
                }else if(customServiceAttributes!=null){
                    foreach (var iType in type.GetInterfaces())
                    {
                        if(customServiceAttributes.Scope=="Transient"){
                            services.AddTransient(iType, type);
                        }else if(customServiceAttributes.Scope=="Singleton"){
                            services.AddSingleton(iType, type);
                        }                            
                        Log.Information($"Added {iType} as {customServiceAttributes.Scope}");
                    }
                }  
                
            }

            ConfigureSwaggerService(services);
            Log.Information("services were configured successfully");
        }

        public void ValidateRequiredVariables() {
            if (this.Configuration["META_APP_NAME"] == null || this.Configuration["META_APP_NAME"] == "")
            {
                throw new Exception("META_APP_NAME is required");
            }

            if (this.Configuration["META_LOG_PATH"] == null || this.Configuration["META_LOG_PATH"] == "")
            {
                throw new Exception("META_LOG_PATH is required");
            }

            if (this.Configuration["META_ENV"] == null || this.Configuration["META_ENV"] == "")
            {
                throw new Exception("META_ENV is required");
            }

            if (this.Configuration["META_API_KEY"] == null || this.Configuration["META_API_KEY"] == "")
            {
                throw new Exception("META_API_KEY is required");
            }
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.ihostingenvironment?view=aspnetcore-5.0
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiGlobalConfiguration apiGlobalConfiguration, DatabaseManager databaseManager, SingletonJobFactory singletonJobFactory, ApiConfiguration apiConfiguration)
        {
            databaseManager.Configure();

            //quartzConfigurator.Configure();
            ConfigureQuartzService(singletonJobFactory, apiGlobalConfiguration);

            ConfigureDeveloperOptions(app);
            app.UseRouting();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // will order middlewares based on attributes on the middleware classes
            app.UseMiddlewares<Startup>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            Log.Information("application was started successfully");
        }

        private void ConfigureLogService(IServiceCollection services)
        {
            //https://freesoft.dev/program/54248908
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            LoggingLevelSwitch baseSourceLevelSwitch = new LoggingLevelSwitch(loggerUtil.getLevelFromString(this.Configuration["META_LOG_BASE_LEVEL"], LogEventLevel.Information));
            LoggingLevelSwitch microsoftSourceLevelSwitch = new LoggingLevelSwitch(loggerUtil.getLevelFromString(this.Configuration["META_LOG_MICROSOFT_LEVEL"], LogEventLevel.Warning));

            CustomLoggingLevelSwitchers customLoggingLevelSwitchers = new CustomLoggingLevelSwitchers();
            customLoggingLevelSwitchers.baseSourceLevelSwitch = baseSourceLevelSwitch;
            customLoggingLevelSwitchers.microsoftSourceLevelSwitch = microsoftSourceLevelSwitch;

            services.AddSingleton(customLoggingLevelSwitchers);

            var logPath = this.Configuration["META_LOG_PATH"];
            
            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + " Information   Log path is: " + logPath);

            //TODO: Should we put RequestPath in file log ?
            LoggerConfiguration logConfiguration = new LoggerConfiguration()
                        .MinimumLevel.ControlledBy(baseSourceLevelSwitch)
                        .MinimumLevel.Override("Microsoft", microsoftSourceLevelSwitch)
                        .Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate:
                            "{Timestamp:dd-MM-yyyy HH:mm:ss} {Level} {RequestIdentifier} {AppIdentifier} {ConsumerIdentifier} {SubjectIdentifier} {SourceContext} {Message} {Exception}{NewLine}")
                        .WriteTo.File(path: $@"{logPath}",
                        outputTemplate:
                            "{Timestamp:dd-MM-yyyy HH:mm:ss} {Level} {RequestIdentifier} {AppIdentifier} {ConsumerIdentifier} {SubjectIdentifier} {SourceContext} {Message} {Exception}{NewLine}",
                              rollingInterval: RollingInterval.Day);

            if (!string.IsNullOrEmpty(this.Configuration["META_LOG_AWS_ACCESS_KEY_ID"]))
            {
                var awsCredentials = new BasicAWSCredentials(this.Configuration["META_LOG_AWS_ACCESS_KEY_ID"], this.Configuration["META_LOG_AWS_SECRET_ACCESS_KEY"]);

                var options = new CloudWatchSinkOptions
                {
                    LogGroupName = this.Configuration["META_LOG_AWS_GROUP_NAME"],
                    LogStreamNameProvider = new CloudWatchStreamNameEnvironmentProvider(),
                    CreateLogGroup = true,
                    MinimumLogEventLevel = LogEventLevel.Debug,
                    //more details
                    //https://nblumhardt.com/2021/06/customize-serilog-text-output/
                    TextFormatter = new ExpressionTemplate(
                    "{'Level'} = {@l:u4} " +                    
                    "{'AppIdentifier'} = {@p.AppIdentifier} " +
                    "{'RequestIdentifier'} = {@p.RequestIdentifier} " +
                    "{'RequestPath'} = {RequestPath} " +
                    "{'ConsumerIdentifier'} = {@p.ConsumerIdentifier} " +
                    "{'SubjectIdentifier'} = {@p.SubjectIdentifier} " +
                    "{'SourceContext'} = {SourceContext} " +
                    "{'Datetime'} = {@t:dd-MM-yyyy HH:mm:ss} " +
                    "{'Message'} = {@m} " +
                    "{@x}"
                    ),
                };
                var region = RegionEndpoint.GetBySystemName(this.Configuration["META_LOG_AWS_DEFAULT_REGION"]);
                var cloudwatchClient = new AmazonCloudWatchLogsClient(awsCredentials, region);
                //TODO: remote lgos to aws cannot be switched. Just the initial level is taken
                logConfiguration
                    .WriteTo.AmazonCloudWatch(options, cloudwatchClient);
            }

            Log.Logger = logConfiguration.CreateLogger();
            LogContext.PushProperty("AppIdentifier", String.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("META_APP_NAME")) ? "unkown-app" : System.Environment.GetEnvironmentVariable("META_APP_NAME"));
        }

        private void ConfigureSwaggerService(IServiceCollection services)
        {
            if (this.Configuration["META_ENV"] != null && this.Configuration["META_ENV"] == "development")
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = String.IsNullOrEmpty(this.Configuration["META_APP_NAME"]) ? "Api" : this.Configuration["META_APP_NAME"],
                    });
                });
            }
        }

        private void ConfigureDeveloperOptions(IApplicationBuilder app)
        {
            if (!String.IsNullOrEmpty(this.Configuration["META_ENV"]) && this.Configuration["META_ENV"] == "development")
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Api.WebApi.xml");
                });
                Log.Information("swagger will be ready to use at: ip:port/swagger/index.html or domain.com/swagger/index.html");
            }
        }
        private void ConfigureQuartzService(SingletonJobFactory singletonJobFactory, ApiGlobalConfiguration apiGlobalConfiguration) {

            if (!String.IsNullOrEmpty(this.Configuration["ENABLE_SCHEDULERS"]) && this.Configuration["ENABLE_SCHEDULERS"] == "true") {
                //enable if scheduler are used
                QuartzConfigurator quartz = new QuartzConfigurator(singletonJobFactory, apiGlobalConfiguration, this.Configuration);
                //TODO automatic add: get all Scheduler and add them
                quartz.Configure();
            }
            else
            {
                Log.Information("Schedulers are disabled");
            }

        }

        public void RegisterServices(IServiceCollection services) {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<ISimpleService, SimpleService>();
        }
    }
}

using Api.Common.Schedulers;
using Common.Attributes;
using Common.Configuration;
using Quartz;
using Quartz.Impl;
using Serilog;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Api.Common.Schedulers
{
    public class QuartzConfigurator
    {

        private string[] types;
        private SingletonJobFactory singletonJobFactory;
        private ApiGlobalConfiguration apiGlobalConfiguration;
        private readonly ILogger<QuartzConfigurator> logger;
        private IConfiguration config;

        public QuartzConfigurator(SingletonJobFactory singletonJobFactory, ApiGlobalConfiguration apiGlobalConfiguration, IConfiguration config) {
            this.singletonJobFactory = singletonJobFactory;
            this.apiGlobalConfiguration = apiGlobalConfiguration;
            this.config = config;
        }

        public void AddJobsAsStrings(params string[] types)
        {
            this.types = types;
        }      

        public async void Configure()
        {
            // 1. Create a scheduler Factory
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();            
            // 2. Get and start a scheduler
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            scheduler.JobFactory = this.singletonJobFactory;

            await scheduler.Start();

            int configuredSchedulers = 0;

            // 3. Add jobs
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type type in types) 
            {
                var att = type.GetCustomAttributes(typeof(Scheduler), true).FirstOrDefault() as Scheduler;
                if (att == null) continue;
                
                Log.Information($"Job {type} is annotated with [Scheduler]. Configuring...");
                bool isSchedulerEnabled = false;
                try{
                  isSchedulerEnabled = this.apiGlobalConfiguration.GetBooleanByAbsoluteKey(type+":Enabled");
                }catch(Exception ex){
                    Log.Information(ex,"Error while "+type+":Enabled was being retrieved");
                }

                if(!isSchedulerEnabled){
                    Log.Information($"scheduler is disabled because it don't have this value or is false {type}:Enabled");
                    continue;
                }

                if (att.JobName == null || att.TriggerGroup == null || att.CronExpression == null)
                {
                    Log.Information($"Job {type} is not annotated with [Scheduler] but don't have all the required attributes JobName, TriggerGroup, CronExp");
                }

                string cronExpressionValue = (string)this.apiGlobalConfiguration.GetObjecByAbsoluteKey(att.CronExpression);

                if (String.IsNullOrEmpty(cronExpressionValue)) {
                    throw new Exception($"{type} cannot obtain its CronExpression from appsettings.json: {att.CronExpression}");
                }

                Log.Information($"Job {type} will be scheduled at: {cronExpressionValue}");

                IJobDetail job = JobBuilder.Create(type).WithIdentity(att.JobName, att.TriggerGroup).Build();
                
                // 4. Create a trigger
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(att.JobName, att.TriggerGroup)
                    .WithCronSchedule(cronExpressionValue).StartAt(DateTime.UtcNow.AddSeconds(30))
                    .Build();
                // 5. Schedule the job using the job and trigger 
                await scheduler.ScheduleJob(job, trigger);
                configuredSchedulers++;
            }

            if(configuredSchedulers>0) Log.Information("Schedulers were configured successfully using quartz-scheduler.net");
        }

  }
}

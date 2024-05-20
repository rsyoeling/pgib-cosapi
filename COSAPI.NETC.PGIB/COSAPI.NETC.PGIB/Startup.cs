using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COSAPI.NETC.PGIB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                    AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.LoginPath = "/Account/LogOn";
                        options.LogoutPath = "/Account/LogOut";
                    });

            services.AddDistributedMemoryCache();

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing.
            //    //options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //    // Make the session cookie essential
            //    options.Cookie.IsEssential = true;
            //});            

            services.AddHttpContextAccessor();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //descomentar YR
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            services.AddHttpClient();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().AddSessionStateTempDataProvider();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(36000);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;

            });


            services.Configure<CookieTempDataProviderOptions>(options => {
                options.Cookie.IsEssential = true;
            });


            services.AddMemoryCache();

            services.AddHealthChecks();

            //descomentar YR
            //services.AddSingleton<IApplicationConfiguration, ApplicationConfiguration>(
            //    e => Configuration.GetSection("AppSettings")
            //            .Get<ApplicationConfiguration>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);


            // authentication 
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            //descomentar
            //services.AddTransient(
            //    m => new UserManager(
            //       /*Configuration
            //           .GetValue<string>(
            //               DEFAULT_CONNECTIONSTRING //this is a string constant
            //           )*/
            //       Configuration.GetSection("AppSettings").Get<AppSettings>()
            //        )
            //    );

            services.AddDistributedMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection(); comentado yr
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization(); comentado yr
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LogOn}"); //  {controller=Home}/{action=Index}/{id?}
            });
        }
    }
}

using ConfigurationSubstitution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Text.RegularExpressions;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") +" Information   application is starting");
            CreateHostBuilder(args)
            .ConfigureAppConfiguration((ctx, builder) =>
            {
                builder.EnableSubstitutions("$(", ")");
            }).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
                    options.Limits.MaxConcurrentConnections = 10000;
                    options.Limits.MaxConcurrentUpgradedConnections = 10000;
                    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
                    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(2);

                }).
                UseStartup<Startup>();
            }).
            UseSerilog();
        }
    }
}

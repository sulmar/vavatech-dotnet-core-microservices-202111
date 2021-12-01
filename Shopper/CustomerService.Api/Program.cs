using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // dotnet add package Serilog.AspNetCore

            // Uruchomienie Seq w konterze dockera
            // docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
            // dotnet add package Serilog.Sinks.Seq


            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)                
                .WriteTo.File(new CompactJsonFormatter(), "logs/log.json")
                .WriteTo.Seq("http://localhost:5341")
                .Enrich.WithEnvironmentName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMemoryUsage()
                .CreateLogger();

            try
            {
                Log.Information("Application starting...");

                CreateHostBuilder(args).Build().Run();
            }

            catch(Exception e)
            {
                Log.Fatal(e, "Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    string environmentName = hostingContext.HostingEnvironment.EnvironmentName;

                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddXmlFile("appsettings.xml", optional: true); 
                    config.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
                    config.AddUserSecrets<Program>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}

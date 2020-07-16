using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Host.HostedServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codibly.Services.Mailer.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                    // place to configure distributed logging, etc.
                }) 
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.Local.json", optional: true);
                })
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddHostedService<EmailSenderHostedService>();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}
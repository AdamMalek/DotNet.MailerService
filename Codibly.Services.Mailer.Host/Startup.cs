using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codibly.Services.Mailer.Domain.Adapters;
using Codibly.Services.Mailer.Domain.Commands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codibly.Services.Mailer.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var commandHandlers = typeof(ICommandHandler)
                .Assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass)
                .Where(x => typeof(ICommandHandler).IsAssignableFrom(x))
                .ToList();


            foreach (var commandHandler in commandHandlers)
            {
                foreach (var @interface in commandHandler.GetInterfaces())
                {
                    services.AddTransient(@interface, commandHandler);
                }

                services.AddScoped(commandHandler);
            }

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("", async context => await context.Response.WriteAsync("Mailer works!"));
                endpoints.MapControllers();
            });
        }
    }
}
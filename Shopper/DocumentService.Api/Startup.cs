using DocumentService.Api.Notifications;
using DocumentService.Domain;
using DocumentService.Infrastructure;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));

            // dotnet add package Hangfire.InMemory
            // services.AddHangfire(configuration => configuration.UseInMemoryStorage());

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DocumentsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // dotnet add package Hangfire.SqlServer
            services.AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString));

            services.AddScoped<IDocumentService, PdfDocumentService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard(); // http://localhost:5040/hangfire

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Przekierowanie 
                // endpoints.MapGet("/", async context => context.Response.Redirect("hangfire"));

                // Przekierowanie z u¿yciem w³asnej metody rozszerzaj¹cej
                endpoints.Redirect("/", "hangfire");

                endpoints.MapPost("/documents", async context =>
                {
                    var customer = await context.Request.ReadFromJsonAsync<Customer>();

                    // TODO: queue

                    IMediator mediator = context.RequestServices.GetRequiredService<IMediator>();

                    await mediator.Publish(new CreateDocumentNotification(customer));

                    context.Response.StatusCode = StatusCodes.Status202Accepted;

                });

                endpoints.MapHangfireDashboard();
            });

        }
    }
}

using Bogus;
using CustomerService.Domain;
using CustomerService.Intrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api
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
            services.AddScoped<ICustomerRepositoryAsync, DbCustomerRepository>();
            services.AddScoped<IMessageService, SendGridMessageService>();

            services.Configure<SendGridOptions>(Configuration.GetSection("SendGrid"));

            // dotnet add package Microsoft.EntityFrameworkCore.InMemory
            //services.AddDbContext<CustomerContext>(options =>
            //{
            //    options.UseInMemoryDatabase("CustomersInMemory");
            //    options.EnableSensitiveDataLogging();
            //});

            string connectionString = Configuration.GetConnectionString("CustomerConnectionString");

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseSqlServer(connectionString);
               
                options.EnableSensitiveDataLogging(bool.Parse(Configuration["EnableSensitiveDataLogging"]));
               
            });

            services.AddHealthChecks()
                .AddCheck("Ping", () => HealthCheckResult.Healthy());

            // dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllers()
                .AddNewtonsoftJson();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerService.Api", Version = "v1" });
            });

            // dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomerContext context, ILogger<Startup> logger)
        {

            context.Database.EnsureCreated();

#if DEBUG
            Console.WriteLine("XXXX");
#endif

            // ASPNETCORE_ENVIRONMENT
            // if (env.EnvironmentName=="Blabla")

            if (env.IsEnvironment("Blabla"))
            {
               
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerService.Api v1"));

                // int customerQuantity = int.Parse(Configuration["CustomerQuantity"]);

                int customerQuantity = int.Parse(Configuration["Customers:Quantity"]);

                string lifetime = Configuration["Logging:LogLevel:Microsoft.Hosting.Lifetime"];

                // Pobranie obiektu na podstawie sekcji w konfiguracji
                var customerOptions = Configuration.GetSection("Customers").Get<CustomerOptions>();

                if (!context.Customers.Any())
                {
                    // dotnet add package Bogus
                    var customers = new Faker<Customer>()
                         .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                         .RuleFor(p => p.LastName, f => f.Person.LastName)
                         .RuleFor(p => p.Email, f => f.Person.Email)
                         .Generate(customerQuantity);

                    context.Customers.AddRange(customers);
                    context.SaveChanges();
                }
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            string instance = Configuration["instance"];

            logger.LogInformation("Instance {0}", instance);

            // dotnet run --instance InstanceA --urls "http://localhost:5010;https://localhost:5011"
            // dotnet run --instance InstanceB --urls "http://localhost:5012;https://localhost:5013"

            // Dodawanie w³asnych nag³ówków do odpowiedzi
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-Instance", instance);

                    return Task.CompletedTask;
                });

                await next();
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}

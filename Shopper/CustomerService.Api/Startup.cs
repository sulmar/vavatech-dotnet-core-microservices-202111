using Bogus;
using CustomerService.Domain;
using CustomerService.Intrastructure;
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

            // dotnet add package Microsoft.EntityFrameworkCore.InMemory
            services.AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase("CustomersInMemory"));

            // dotnet add package Microsoft.EntityFrameworkCore.SqlServer
            // services.AddDbContext<CustomerContext>(options => options.UseSqlServer(connectionString));

            services.AddHealthChecks()
                .AddCheck("Ping", () => HealthCheckResult.Healthy());


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerService.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CustomerContext context)
        {

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

                // dotnet add package Bogus
                var customers = new Faker<Customer>()
                     .RuleFor(p => p.FirstName, f => f.Person.FirstName)
                     .RuleFor(p => p.LastName, f => f.Person.LastName)
                     .RuleFor(p => p.Email, f => f.Person.Email)
                     .Generate(100);

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}

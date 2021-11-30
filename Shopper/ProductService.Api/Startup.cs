using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Api.Middlewares;
using ProductService.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // Logger Middleware
            #region Logger Middleware

            //app.Use(async (context, next) =>
            //{
            //    // request
            //    logger.LogInformation("{0} {1}", context.Request.Method, context.Request.Path);

            //    await next();

            //    // response
            //    logger.LogInformation("{0}", context.Response.StatusCode);

            //});

            // app.UseMiddleware<LoggerMiddleware>();

            #endregion

            app.UseLogger();

            // Authorization Middleware
            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Headers.ContainsKey("Authorization"))
            //    {
            //        await next();
            //    }
            //    else
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    }
            //});

            // Middleware Products

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path == "/api/products" && context.Request.Method == "GET")
            //    {
            //        await context.Response.WriteAsync("Products!");
            //    }
            //    else
            //    {
            //        await next();
            //    }                
            //});

            // Logic Middleware
            // app.Run(context => context.Response.WriteAsync("Hello World!"));

            // Routing Middleware
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    async context => await context.Response.WriteAsync("Hello World!"));

                endpoints.MapGet("/api/products", 
                    async context => await context.Response.WriteAsync("Products!"));

                endpoints.MapGet("/api/products/{id:int}",
                    async context =>
                    {
                        IProductRepository productRepository = (IProductRepository) context.RequestServices.GetRequiredService(typeof(IProductRepository));

                        var id = context.Request.RouteValues["id"];

                        Product product = new Product { Id = 10, Name = "My product", BarCode = "1245" };

                        // await context.Response.WriteAsync($"Product {id}!");

                        await context.Response.WriteAsJsonAsync(product);
                    });

            });


        }
    }
}

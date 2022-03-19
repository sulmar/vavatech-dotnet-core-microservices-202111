using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductService.Api.Middlewares;
using ProductService.Domain;
using ProductService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Extensions;

namespace ProductService.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, DbProductRepository>();

            // dotnet add package Microsoft.EntityFrameworkCore.InMemory
            services.AddDbContextPool<ProductContext>(options => options.UseInMemoryDatabase("ProductsInMemory"), poolSize: 3);
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


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Routing Middleware
            app.UseRouting();

            // Route-To-Code

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",
                    async context => await context.Response.WriteAsync("Hello World!"));

                endpoints.MapGet("/api/products",
                    async context =>
                    {
                        IProductRepository productRepository = (IProductRepository)context.RequestServices.GetRequiredService(typeof(IProductRepository));

                        var products = await productRepository.Get();

                        await context.Response.WriteAsJsonAsync(products);
                    }
                    );

                endpoints.MapGet("/api/products/{id:int}",
                    async context =>
                    {
                        IProductRepository productRepository = (IProductRepository)context.RequestServices.GetRequiredService(typeof(IProductRepository));

                        var id = Convert.ToInt32(context.Request.RouteValues["id"]);

                        Product product = await productRepository.Get(id);

                        await context.Response.WriteAsJsonAsync(product);
                    });

                endpoints.MapPost("/api/products", async context =>
                {
                    IProductRepository productRepository = (IProductRepository)context.RequestServices.GetRequiredService(typeof(IProductRepository));

                    var product = await context.Request.ReadFromJsonAsync<Product>();

                    throw new NotImplementedException();

                    await productRepository.Add(product);

                    context.Response.StatusCode = (int) HttpStatusCode.Created;                    

                });

                endpoints.MapHead("/api/products/{id:int}", async context =>
                {
                    IProductRepository productRepository = (IProductRepository)context.RequestServices.GetRequiredService(typeof(IProductRepository));

                    var id = Convert.ToInt32(context.Request.RouteValues["id"]);

                    Product product = await productRepository.Get(id);

                    if (product==null)
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                });

            });


        }
    }
}

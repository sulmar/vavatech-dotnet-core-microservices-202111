using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProtoBuf.Grpc.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingService.gRPC.Services;

namespace TrackingService.gRPC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // dotnet add package protobuf-net.Grpc.AspNetCore
            services.AddCodeFirstGrpc(options =>
            {
                options.Interceptors.Add<Interceptors.ServerLoggingInterceptor>();
            });

            // dotnet add package Grpc.AspNetCore.Server.Reflection 
            services.AddGrpcReflection();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
                endpoints.MapGrpcService<DeliveryService>();

                if (env.IsDevelopment())
                {
                    endpoints.MapGrpcReflectionService();
                }
            });
        }
    }
}

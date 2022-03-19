using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using System;
using System.Threading.Tasks;
using TrackingService.Contracts;

namespace TrackingService.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            await SayHelloTest();
            await ConfirmDeliveryTest();
        }

        private static async Task ConfirmDeliveryTest()
        {
            // dotnet add package Grpc.Net.Client --version 2.44.0
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IDeliveryService>();

            var reply = await client.ConfirmDeliveryAsync(new DeliveryRequest { OrderId = 1, Status = DeliveryStatus.Delivered });

            Console.WriteLine(reply);
        }

        private static async Task SayHelloTest()
        {
            // dotnet add package Grpc.Net.Client --version 2.44.0
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();

            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });

            Console.WriteLine($"Greeting: {reply.Message}");
        }
    }
}

using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingService.Contracts;

namespace TrackingService.gRPC.Services
{
    // dotnet add package protobuf-net.Grpc.AspNetCore --version 1.0.152

    public class GreeterService : IGreeterService
    {
        public Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default)
        {
            return Task.FromResult(new HelloReply { Message = $"Hello {request.Name}" });
        }
    }
}

using ProtoBuf.Grpc;
using System.Threading.Tasks;
using TrackingService.Contracts;

namespace TrackingService.gRPC.Services
{
    public class DeliveryService : IDeliveryService
    {
        public Task<DeliveryReply> ConfirmDeliveryAsync(DeliveryRequest request, CallContext context = default)
        {
            return Task.FromResult(new DeliveryReply { Confirmed = true });
        }
    }
}

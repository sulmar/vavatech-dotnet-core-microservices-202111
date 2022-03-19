using ProtoBuf.Grpc;
using System.ServiceModel;
using System.Threading.Tasks;

namespace TrackingService.Contracts
{
    [ServiceContract]
    public interface IDeliveryService
    {
        [OperationContract]
        Task<DeliveryReply> ConfirmDeliveryAsync(DeliveryRequest request, CallContext context = default);
    }
}

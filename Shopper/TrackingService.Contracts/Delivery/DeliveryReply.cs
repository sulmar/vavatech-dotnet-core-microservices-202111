using System.Runtime.Serialization;

namespace TrackingService.Contracts
{
    [DataContract]
    public class DeliveryReply
    {
        [DataMember(Order = 1)]
        public bool Confirmed { get; set; }
    }
}

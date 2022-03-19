using System.Runtime.Serialization;

namespace TrackingService.Contracts
{
    [DataContract]
    public class DeliveryRequest
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [DataMember(Order = 2)]
        public DeliveryStatus Status { get; set; }
    }

    public enum DeliveryStatus
    {
        Delivered,
        Absent
    }
}

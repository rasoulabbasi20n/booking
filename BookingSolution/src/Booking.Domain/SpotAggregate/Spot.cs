using Framework.Domain;

namespace Booking.Domain.SpotAggregate
{
    public class Spot : AggregateRootWithGuid
    {
        public string Name { get; set; }
        public DateTime ReservedDate { get; set; }
        public TimeOnly ReservedTime { get; set; }
    }
}

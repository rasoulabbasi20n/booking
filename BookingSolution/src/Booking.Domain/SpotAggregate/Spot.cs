using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.SpotAggregate
{
    public class Spot : AggregateRootWithGuid
    {
        public string Name { get; set; }
        public DateTime ReservedDate { get; set; }
        public TimeOnly ReservedTime { get; set; }
    }
}

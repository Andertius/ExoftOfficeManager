using System.Collections.Generic;

namespace ExoftOfficeManager.Domain.Entities
{
    public class WorkPlace : EntityBase
    {
        public int FloorNumber { get; set; }

        public int PlaceNumber { get; set; }


        public ICollection<Booking> Bookings { get; set; }
    }
}

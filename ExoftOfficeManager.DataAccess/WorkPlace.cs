using System.Collections.Generic;

namespace ExoftOfficeManager.DataAccess
{
    public class WorkPlace : IEntity
    {
        public long Id { get; set; }

        public int FloorNumber { get; set; }

        public int PlaceNumber { get; set; }


        public ICollection<Booking> Bookings { get; set; }
    }
}

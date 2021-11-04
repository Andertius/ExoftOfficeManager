using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.Domain.Dtos
{
    public class WorkPlaceDto
    {
        public Guid Id { get; set; }

        public int FloorNumber { get; set; }

        public int PlaceNumber { get; set; }

        public ICollection<BookingDto> Bookings{ get; set; }
    }
}

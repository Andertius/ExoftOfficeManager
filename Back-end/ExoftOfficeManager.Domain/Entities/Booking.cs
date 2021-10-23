using System;

namespace ExoftOfficeManager.Domain.Entities
{
    public class Booking : EntityBase
    {
        public DateTime Date { get; set; }

        public BookingType Type { get; set; }

        public BookingStatus Status { get; set; }

        public int? DayNumber { get; set; }


        public User User { get; set; }

        public WorkPlace WorkPlace { get; set; }
    }
}

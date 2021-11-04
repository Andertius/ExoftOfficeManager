using System;

using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Domain.Entities
{
    //TODO rework dates (start date - end date)
    public class Booking : EntityBase
    {
        public DateTime? Date { get; set; }

        public BookingType Type { get; set; }

        public BookingStatus Status { get; set; }

        public int? DayNumber { get; set; }


        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid WorkPlaceId { get; set; }

        public WorkPlace WorkPlace { get; set; }
    }
}

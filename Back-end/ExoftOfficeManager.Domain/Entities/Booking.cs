using System;

namespace ExoftOfficeManager.Domain.Entities
{
    public class Booking : EntityBase
    {
        public DateTime Date { get; set; }

        public WorkPlaceStatus Status { get; set; }


        public long UserId { get; set; }

        public User User { get; set; }


        public long WorkPlaceId { get; set; }

        public WorkPlace WorkPlace { get; set; }
    }
}

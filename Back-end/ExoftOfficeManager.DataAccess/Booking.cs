using System;

namespace ExoftOfficeManager.DataAccess
{
    public class Booking : IEntity
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public WorkPlaceStatus Status { get; set; }


        public long UserId { get; set; }

        public User User { get; set; }


        public long WorkPlaceId { get; set; }

        public WorkPlace WorkPlace { get; set; }
    }
}

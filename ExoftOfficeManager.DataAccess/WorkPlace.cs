using System;

namespace ExoftOfficeManager.DataAccess
{
    public class WorkPlace : IEntity
    {
        public long Id { get; set; }

        public int FloorNumber { get; set; }

        public int PlaceNumber { get; set; }

        public DateTime Date { get; set; }

        public WorkPlaceStatus Status { get; set; }


        public long DeveloperId { get; set; }

        public User Developer { get; set; }
    }
}

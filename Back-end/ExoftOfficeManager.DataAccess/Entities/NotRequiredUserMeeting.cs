using System;

namespace ExoftOfficeManager.DataAccess.Entities
{
    public class NotRequiredUserMeeting : EntityBase
    {
        public long UserId { get; set; }

        public User NotRequiredUser { get; set; }


        public long MeetingId { get; set; }

        public Meeting Meeting { get; set; }
    }
}

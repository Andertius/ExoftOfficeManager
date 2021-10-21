using System;

namespace ExoftOfficeManager.Domain.Entities
{
    public class RequiredUserMeeting : EntityBase
    {
        public long UserId { get; set; }

        public User RequiredUser { get; set; }


        public long MeetingId { get; set; }

        public Meeting Meeting { get; set; }
    }
}

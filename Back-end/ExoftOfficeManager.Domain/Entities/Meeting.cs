using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.Domain.Entities
{
    public class Meeting : EntityBase
    {
        public DateTime DateAndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int RoomNumber { get; set; }

        public string MeetingPurpose { get; set; }


        public long OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<RequiredUserMeeting> RequiredUserMeetings { get; set; }

        public ICollection<NotRequiredUserMeeting> NotRequiredUserMeetings { get; set; }
    }
}

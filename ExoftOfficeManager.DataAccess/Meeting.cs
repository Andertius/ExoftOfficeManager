using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.DataAccess
{
    public class Meeting
    {
        public long Id { get; set; }

        public DateTime DateAndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int RoomNumber { get; set; }

        public string MeetingPurpose { get; set; }

        public long OwnerId { get; set; }


        public User Owner { get; set; }

        public ICollection<User> UsersRequired { get; set; }

        public ICollection<User> UsersNotRequired { get; set; }
    }
}

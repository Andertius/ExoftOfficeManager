using System.Collections.Generic;

namespace ExoftOfficeManager.Domain.Entities
{
    public class User : EntityBase
    {
        public string FullName { get; set; }

        public string Avatar { get; set; }

        public UserRole Role { get; set; }


        public ICollection<Booking> Bookings { get; set; }

        public ICollection<Meeting> OwnerMeetings { get; set; }

        public ICollection<RequiredUserMeeting> RequiredUserMeetings { get; set; }

        public ICollection<NotRequiredUserMeeting> NotRequiredUserMeetings { get; set; }
    }
}

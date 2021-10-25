using System;
using System.Collections.Generic;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Domain.Dtos
{
    public class MeetingDto
    {
        public Guid Id { get; set; }

        public DateTime DateAndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int RoomNumber { get; set; }

        public string MeetingPurpose { get; set; }

        public User Owner { get; set; }

        public ICollection<User> RequiredUsers { get; set; }
        
        public ICollection<User> NonRequiredUsers { get; set; }
    }
}

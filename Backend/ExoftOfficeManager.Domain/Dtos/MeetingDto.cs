using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.Domain.Dtos
{
    public class MeetingDto
    {
        public Guid Id { get; set; }

        public DateTime DateAndTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int RoomNumber { get; set; }

        public string MeetingPurpose { get; set; }

        public UserDto Owner { get; set; }

        public ICollection<UserDto> RequiredUsers { get; set; }
        
        public ICollection<UserDto> NonRequiredUsers { get; set; }
    }
}

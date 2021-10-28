using System;
using System.Collections.Generic;

using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Domain.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string AvatarUrl { get; set; }

        public UserRole Role { get; set; }

        public ICollection<BookingDto> Bookings { get; set; }
    }
}

using System;

using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Domain.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Avatar { get; set; }

        public UserRole Role { get; set; }
    }
}
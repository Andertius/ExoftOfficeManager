using System;

using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace ExoftOfficeManager.Infrastructure.Identity
{
    public class AppIdentityUser : IdentityUser, IIdentityUser
    {
        public Guid ApiUserId { get; set; }

        public User ApiUser { get; set; }
    }
}

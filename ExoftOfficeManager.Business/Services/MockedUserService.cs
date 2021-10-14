using System.Collections.Generic;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services
{
    public class MockedUserService : IUserService
    {
        private readonly List<User> _developers = new()
        {
            new Admin { Id = 1, FullName = "Norbert Moses", Avatar = "avatar_path" },
            new Developer { Id = 2, FullName = "John Doe", Avatar = "avatar_path" },
            new Admin { Id = 3, FullName = "Painis Dickens" , Avatar = "avatar_path" },
            new Developer { Id = 4, FullName = "Pootis Pencer" , Avatar = "avatar_path" },
            new Developer { Id = 5, FullName = "Bob" , Avatar = "avatar_path" },
            new Developer { Id = 6, FullName = "James Hetfield" , Avatar = "avatar_path" },
            new Developer { Id = 7, FullName = "Dave Mustaine" , Avatar = "avatar_path" },
            new Developer { Id = 8, FullName = "John Petrucci" , Avatar = "avatar_path" },
        };

        public User Find(long id)
            => _developers.Find(x => x.Id == id);
    }
}

using System.Collections.Generic;

using ExoftOfficeManager.Services.Interfaces;

namespace ExoftOfficeManager.Services
{
    public class MockedDeveloperService : IDeveloperService
    {
        private readonly List<Developer> _developers = new()
        {
            new Developer { Id = 1, FullName = "Norbert Moses", Avatar = "avatar_path" },
            new Developer { Id = 2, FullName = "John Doe", Avatar = "avatar_path" },
            new Developer { Id = 3, FullName = "Painis Dickens" , Avatar = "avatar_path" },
            new Developer { Id = 4, FullName = "Pootis Pencer" , Avatar = "avatar_path" },
            new Developer { Id = 5, FullName = "Bob" , Avatar = "avatar_path" },
            new Developer { Id = 6, FullName = "James Hetfield" , Avatar = "avatar_path" },
            new Developer { Id = 7, FullName = "Dave Mustaine" , Avatar = "avatar_path" },
            new Developer { Id = 8, FullName = "John Petrucci" , Avatar = "avatar_path" },
        };

        public Developer Find(long id)
            => _developers.Find(x => x.Id == id);
    }
}

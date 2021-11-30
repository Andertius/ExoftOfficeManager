using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(x => x.Bookings)
                .ToArrayAsync();
        }

        public async Task<User> FindUserById(Guid userId)
        {
            var user = await _context.Users
                .Include(x => x.Bookings)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _context.Users
                .Include(x => x.Bookings)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}

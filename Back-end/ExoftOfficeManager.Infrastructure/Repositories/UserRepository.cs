using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Dtos;

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

        public async Task<IList<UserDto>> GetAllUsers()
        {
            return await _context.Users
                .Include(x => x.Bookings)
                .Select(x => UserMapper.MapIntoDto(x))
                .ToArrayAsync();
        }

        public async Task<UserDto> FindUserById(Guid userId)
        {
            var user = await _context.Users
                .Include(x => x.Bookings)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == userId);

            return UserMapper.MapIntoDto(user);
        }

        public async Task AddUser(UserDto userDto)
        {
            await _context.Users.AddAsync(UserMapper.MapFromDto(userDto));
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IList<UserDto>> GetAllUsers();

        Task<UserDto> FindUserById(Guid userId);

        Task AddUser(UserDto userDto);

        Task Commit();
    }
}

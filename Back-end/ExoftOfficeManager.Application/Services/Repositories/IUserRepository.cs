using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllUsers();

        Task<User> FindUserById(Guid userId);

        Task AddUser(User user);

        Task Commit();
    }
}

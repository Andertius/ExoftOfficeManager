using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess.Entities;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Find(long id, IEnumerable<string> inclusion);

        IEnumerable<User> GetAll(IEnumerable<string> inclusion);

        Task Add(User user);
    }
}

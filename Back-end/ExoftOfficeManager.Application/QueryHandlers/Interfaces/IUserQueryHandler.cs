using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers.Interfaces
{
    public interface IUserQueryHandler
    {
        Task<User> FindQuery(long id);

        IEnumerable<User> GetAllQuery();
    }
}

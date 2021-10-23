using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class UserQueryHandler : IUserQueryHandler
    {
        public Task<User> FindQuery(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllQuery()
        {
            throw new NotImplementedException();
        }
    }
}

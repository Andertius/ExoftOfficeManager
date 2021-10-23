using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        public Task AddCommand(User user)
        {
            throw new NotImplementedException();
        }
    }
}

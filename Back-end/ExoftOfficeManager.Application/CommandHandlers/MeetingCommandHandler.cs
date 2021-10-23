using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class MeetingCommandHandler : IMeetingCommandHandler
    {
        public Task<bool> AddCommand(Meeting meet)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCommand(long meetingId)
        {
            throw new NotImplementedException();
        }

        public Task<Meeting> UpdateCommand(Meeting meet)
        {
            throw new NotImplementedException();
        }
    }
}

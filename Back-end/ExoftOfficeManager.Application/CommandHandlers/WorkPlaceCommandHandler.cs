using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class WorkPlaceCommandHandler : IWorkPlaceCommandHandler
    {
        public Task BookCommand(long id, long developerId, BookingType status, DateTime date, int days)
        {
            throw new NotImplementedException();
        }

        public Task<WorkPlace> UpdateCommand(WorkPlace place)
        {
            throw new NotImplementedException();
        }
    }
}

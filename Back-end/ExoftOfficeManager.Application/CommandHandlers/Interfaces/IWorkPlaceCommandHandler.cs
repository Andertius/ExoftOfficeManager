using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers.Interfaces
{
    public interface IWorkPlaceCommandHandler
    {
        Task BookCommand(long id, long developerId, BookingType status, DateTime date, int days);

        Task<WorkPlace> UpdateCommand(WorkPlace place);
    }
}

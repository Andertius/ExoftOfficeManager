using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.Services.Interfaces
{
    public interface IWorkPlaceService
    {
        IEnumerable<WorkPlace> GetAll();

        IEnumerable<WorkPlace> GetAllBooked(DateTime date);

        IEnumerable<WorkPlace> GetAllAvailable(DateTime date);

        Task<WorkPlace> Find(long id);

        Task Book(long id, long developerId, BookingType status, DateTime date, int days);

        Task<WorkPlace> Update(WorkPlace place);
    }
}

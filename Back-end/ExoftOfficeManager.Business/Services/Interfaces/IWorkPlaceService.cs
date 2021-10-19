using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess;
using ExoftOfficeManager.DataAccess.Entities;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IWorkPlaceService
    {
        IEnumerable<WorkPlace> GetAll(IEnumerable<string> inclusion);

        IEnumerable<WorkPlace> GetAllBooked(DateTime date, IEnumerable<string> inclusion);

        IEnumerable<WorkPlace> GetAllAvailable(DateTime date, IEnumerable<string> inclusion);

        IEnumerable<Booking> GetAllUserBooked(long id, IEnumerable<string> inclusion);

        Task<WorkPlace> Find(long id, IEnumerable<string> inclusion);

        Task Book(long id, long developerId, WorkPlaceStatus status, DateTime date, int days);

        Task MakeAvailable(long id, DateTime date, long devId);

        Task<WorkPlace> Update(WorkPlace place);
    }
}

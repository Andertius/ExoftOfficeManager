using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IWorkPlaceService
    {
        IEnumerable<WorkPlace> GetAll();

        IEnumerable<WorkPlace> GetAllBooked(DateTime date);

        IEnumerable<WorkPlace> GetAllAvailable(DateTime date);

        IEnumerable<Booking> GetAllUserBooked(long id);

        WorkPlace Find(long id);

        Task Book(long id, long developerId, WorkPlaceStatus status, DateTime date, int days);

        Task MakeAvailable(long id, DateTime date, long devId);

        Task<WorkPlace> Update(WorkPlace place);
    }
}

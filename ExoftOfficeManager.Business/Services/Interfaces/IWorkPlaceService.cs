using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IWorkPlaceService
    {
        IEnumerable<WorkPlace> GetAll(DateTime date);

        IEnumerable<WorkPlace> GetAllBooked(DateTime date);

        IEnumerable<WorkPlace> GetAllAvailable(DateTime date);

        WorkPlace Find(long id);

        Task Book(long id, long developerId, WorkPlaceStatus status);

        Task MakeAvailable(long id);

        Task<WorkPlace> Update(WorkPlace place);
    }
}

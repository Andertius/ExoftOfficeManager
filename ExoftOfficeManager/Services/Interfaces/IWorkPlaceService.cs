using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.Services.Interfaces
{
    public interface IWorkPlaceService
    {
        IEnumerable<WorkPlace> GetAll(DateTime date);

        IEnumerable<WorkPlace> GetAllBooked(DateTime date);

        IEnumerable<WorkPlace> GetAllAvailable(DateTime date);

        WorkPlace Find(long id);

        void Book(long id, long developerId, WorkPlaceStatus status);

        void MakeAvailable(long id);

        bool Update(WorkPlace place);
    }
}

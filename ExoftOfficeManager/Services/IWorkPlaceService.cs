using System.Collections.Generic;

namespace ExoftOfficeManager.Services
{
    public interface IWorkPlaceService
    {
        public IEnumerable<WorkPlace> GetAll();

        public IEnumerable<WorkPlace> GetAllOccupied();

        public IEnumerable<WorkPlace> GetAllUnoccupied();

        public WorkPlace Find(long id);

        public bool ChangeOccupation(long id, long developerId);

        public bool Update(long id, WorkPlace place);
    }
}

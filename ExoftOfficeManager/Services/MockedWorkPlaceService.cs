using System.Collections.Generic;
using System.Linq;

namespace ExoftOfficeManager.Services
{
    public class MockedWorkPlaceService : IWorkPlaceService
    {
        private readonly List<WorkPlace> _workPlaces = new()
        {
            new WorkPlace { Id = 1, FloorNumber = 5, PlaceNumber = 1, DeveloperId = 0 },
            new WorkPlace { Id = 2, FloorNumber = 5, PlaceNumber = 2, DeveloperId = 1 },
            new WorkPlace { Id = 3, FloorNumber = 5, PlaceNumber = 3, DeveloperId = 0 },
            new WorkPlace { Id = 4, FloorNumber = 5, PlaceNumber = 4, DeveloperId = 2 },
            new WorkPlace { Id = 5, FloorNumber = 5, PlaceNumber = 5, DeveloperId = 3 },
            new WorkPlace { Id = 6, FloorNumber = 4, PlaceNumber = 1, DeveloperId = 4 },
            new WorkPlace { Id = 7, FloorNumber = 4, PlaceNumber = 2, DeveloperId = 5 },
            new WorkPlace { Id = 8, FloorNumber = 4, PlaceNumber = 3, DeveloperId = 0 },
            new WorkPlace { Id = 9, FloorNumber = 4, PlaceNumber = 4, DeveloperId = 6 },
            new WorkPlace { Id = 10, FloorNumber = 4, PlaceNumber = 5, DeveloperId = 0 },
        };

        public IEnumerable<WorkPlace> GetAll()
            => _workPlaces.Select(x => x);

        public IEnumerable<WorkPlace> GetAllOccupied()
            => _workPlaces.Where(x => x.DeveloperId != 0);

        public IEnumerable<WorkPlace> GetAllUnoccupied()
            => _workPlaces.Where(x => x.DeveloperId == 0);

        public WorkPlace Find(long id)
            => _workPlaces.Find(x => x.Id == id);

        public bool ChangeOccupation(long id, long developerId)
        {
            int index = _workPlaces.FindIndex(x => x.Id == id);

            if (index == -1)
            {
                return false;
            }

            _workPlaces[index].DeveloperId = _workPlaces[index].DeveloperId == developerId ? 0 : developerId;
            return true;
        }

        public bool Update(long id, WorkPlace place)
        {
            int index = _workPlaces.FindIndex(x => x.Id == id);

            if (index == -1)
            {
                return false;
            }

            _workPlaces[index] = place;
            return true;
        }
    }
}

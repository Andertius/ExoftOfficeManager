using System;
using System.Collections.Generic;
using System.Linq;

using ExoftOfficeManager.Services.Interfaces;

namespace ExoftOfficeManager.Services
{
    public class MockedWorkPlaceService : IWorkPlaceService
    {
        private readonly List<WorkPlace> _workPlaces = new()
        {
            new WorkPlace { Id = 1,  Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 1, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 2,  Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 2, Status = WorkPlaceStatus.BookedPermanently },
            new WorkPlace { Id = 3,  Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 3, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 4,  Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 4, Status = WorkPlaceStatus.BookedPermanently },
            new WorkPlace { Id = 5,  Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 5, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 6,  Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 1, Status = WorkPlaceStatus.FirstHalfBooked },
            new WorkPlace { Id = 7,  Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 2, Status = WorkPlaceStatus.Booked },
            new WorkPlace { Id = 8,  Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 3, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 9,  Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 4, Status = WorkPlaceStatus.Booked },
            new WorkPlace { Id = 10, Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 5, Status = WorkPlaceStatus.Available },

            new WorkPlace { Id = 11, Date = new DateTime(2021, 10, 11), FloorNumber = 5, PlaceNumber = 1, Status = WorkPlaceStatus.FirstHalfBooked | WorkPlaceStatus.SecondHalfBooked },
            new WorkPlace { Id = 12, Date = new DateTime(2021, 10, 11), FloorNumber = 5, PlaceNumber = 2, Status = WorkPlaceStatus.BookedPermanently },
            new WorkPlace { Id = 13, Date = new DateTime(2021, 10, 11), FloorNumber = 5, PlaceNumber = 3, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 14, Date = new DateTime(2021, 10, 11), FloorNumber = 5, PlaceNumber = 4, Status = WorkPlaceStatus.BookedPermanently },
            new WorkPlace { Id = 15, Date = new DateTime(2021, 10, 11), FloorNumber = 5, PlaceNumber = 5, Status = WorkPlaceStatus.Booked },
            new WorkPlace { Id = 16, Date = new DateTime(2021, 10, 11), FloorNumber = 4, PlaceNumber = 1, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 17, Date = new DateTime(2021, 10, 11), FloorNumber = 4, PlaceNumber = 2, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 18, Date = new DateTime(2021, 10, 11), FloorNumber = 4, PlaceNumber = 3, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 19, Date = new DateTime(2021, 10, 11), FloorNumber = 4, PlaceNumber = 4, Status = WorkPlaceStatus.Available },
            new WorkPlace { Id = 20, Date = new DateTime(2021, 10, 11), FloorNumber = 4, PlaceNumber = 5, Status = WorkPlaceStatus.Booked },
        };

        private bool IsBooked(long id)
        {
            var place = Find(id);
            return place.Status == WorkPlaceStatus.Booked ||
                place.Status == WorkPlaceStatus.BookedPermanently ||
                place.Status == (WorkPlaceStatus.FirstHalfBooked | WorkPlaceStatus.SecondHalfBooked);
        }

        public IEnumerable<WorkPlace> GetAll(DateTime date)
            => _workPlaces.Where(x => x.Date == date);

        public IEnumerable<WorkPlace> GetAllBooked(DateTime date)
            => _workPlaces.Where(x => x.Date == date && IsBooked(x.Id));

        public IEnumerable<WorkPlace> GetAllAvailable(DateTime date)
            => _workPlaces.Where(x => x.Date == date && !IsBooked(x.Id));

        public WorkPlace Find(long id)
            => _workPlaces.Find(x => x.Id == id);

        public void Book(long id, long developerId, WorkPlaceStatus status)
        {
            if (status == WorkPlaceStatus.Available)
            {
                throw new ArgumentException($"Cannot book with status '{status}'.");
            }

            if (IsBooked(id))
            {
                throw new ArgumentException($"The work place with id = {id} is already fully booked");
            }
            else
            {
                var place = Find(id);

                if (status == place.Status)
                {
                    throw new ArgumentException($"Cannot book with status '{status}', because the work place already has that status.");
                }

                if (place.Status == WorkPlaceStatus.FirstHalfBooked && status == WorkPlaceStatus.SecondHalfBooked ||
                    place.Status == WorkPlaceStatus.SecondHalfBooked && status == WorkPlaceStatus.FirstHalfBooked)
                {
                    place.Status |= status;
                }
                else
                {
                    place.Status = status;
                }

                place.DeveloperId = developerId;
                Update(place);
            }
        }

        public void MakeAvailable(long id)
        {
            var place = Find(id);
            place.Status = WorkPlaceStatus.Available;
            Update(place);
        }

        public bool Remove(long id)
        {
            return _workPlaces.Remove(Find(id));
        }

        public bool Update(WorkPlace place)
        {
            int index = _workPlaces.FindIndex(x => x.Id == place.Id);

            if (index == -1)
            {
                return false;
            }

            _workPlaces[index] = place;
            return true;
        }
    }
}

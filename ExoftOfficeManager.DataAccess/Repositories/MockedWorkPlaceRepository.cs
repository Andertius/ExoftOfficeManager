using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public class MockedWorkPlaceRepository : MockedRepository<WorkPlace>
    {
        public MockedWorkPlaceRepository(IEnumerable<WorkPlace> list)
            : base(list)
            => EnsurePopulated();

        private void EnsurePopulated()
        {
            _entities.AddRange(new[]
            {
                new WorkPlace { Id = 1, Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 1, Status = WorkPlaceStatus.Available },
                new WorkPlace { Id = 2, Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 2, Status = WorkPlaceStatus.BookedPermanently },
                new WorkPlace { Id = 3, Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 3, Status = WorkPlaceStatus.Available },
                new WorkPlace { Id = 4, Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 4, Status = WorkPlaceStatus.BookedPermanently },
                new WorkPlace { Id = 5, Date = new DateTime(2021, 10, 10), FloorNumber = 5, PlaceNumber = 5, Status = WorkPlaceStatus.Available },
                new WorkPlace { Id = 6, Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 1, Status = WorkPlaceStatus.FirstHalfBooked },
                new WorkPlace { Id = 7, Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 2, Status = WorkPlaceStatus.Booked },
                new WorkPlace { Id = 8, Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 3, Status = WorkPlaceStatus.Available },
                new WorkPlace { Id = 9, Date = new DateTime(2021, 10, 10), FloorNumber = 4, PlaceNumber = 4, Status = WorkPlaceStatus.Booked },
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
            });
        }
    }
}

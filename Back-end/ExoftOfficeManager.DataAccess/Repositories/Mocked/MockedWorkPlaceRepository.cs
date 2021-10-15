using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExoftOfficeManager.DataAccess.Repositories.Mocked
{
    public class MockedWorkPlaceRepository : MockedRepository<WorkPlace>
    {
        private readonly List<Booking> _bookings;
        private readonly IRepository<Booking> _bookingRepository;

        public MockedWorkPlaceRepository(IEnumerable<WorkPlace> list, IRepository<Booking> bookingRepository)
            : base(list)
        {
            _bookingRepository = bookingRepository;
               _bookings = new();
            EnsureBookingsPopulated();
            EnsureWorkPlacesPopulated();
        }

        private void EnsureWorkPlacesPopulated()
        {
            _entities.AddRange(new[]
            {
                new WorkPlace { Id = 1, FloorNumber = 5, PlaceNumber = 1, Bookings = new List<Booking> { _bookings[5], _bookings[6] } },
                new WorkPlace { Id = 2, FloorNumber = 5, PlaceNumber = 2, Bookings = new List<Booking> { _bookings[0], _bookings[7] } },
                new WorkPlace { Id = 3, FloorNumber = 5, PlaceNumber = 3, Bookings = new List<Booking>() },
                new WorkPlace { Id = 4, FloorNumber = 5, PlaceNumber = 4, Bookings = new List<Booking> { _bookings[1], _bookings[8] } },
                new WorkPlace { Id = 5, FloorNumber = 5, PlaceNumber = 5, Bookings = new List<Booking> { _bookings[9] } },
                new WorkPlace { Id = 6, FloorNumber = 4, PlaceNumber = 1, Bookings = new List<Booking> { _bookings[2] } },
                new WorkPlace { Id = 7, FloorNumber = 4, PlaceNumber = 2, Bookings = new List<Booking> { _bookings[3] } },
                new WorkPlace { Id = 8, FloorNumber = 4, PlaceNumber = 3, Bookings = new List<Booking>() },
                new WorkPlace { Id = 9, FloorNumber = 4, PlaceNumber = 4, Bookings = new List<Booking>() },
                new WorkPlace { Id = 10, FloorNumber = 4, PlaceNumber = 5, Bookings = new List<Booking> { _bookings[4], _bookings[10] } },
            });
        }

        private void EnsureBookingsPopulated()
        {
            _bookings.AddRange(new[]
            {
                new Booking { Id = 1, Date = new DateTime(2021, 10, 10), UserId = 1, Status = WorkPlaceStatus.BookedPermanently },
                new Booking { Id = 2, Date = new DateTime(2021, 10, 10), UserId = 2, Status = WorkPlaceStatus.BookedPermanently },
                new Booking { Id = 3, Date = new DateTime(2021, 10, 10), UserId = 5, Status = WorkPlaceStatus.FirstHalfBooked },
                new Booking { Id = 4, Date = new DateTime(2021, 10, 10), UserId = 6, Status = WorkPlaceStatus.Booked },
                new Booking { Id = 5, Date = new DateTime(2021, 10, 10), UserId = 3, Status = WorkPlaceStatus.Booked },
                new Booking { Id = 6, Date = new DateTime(2021, 10, 11), UserId = 4, Status = WorkPlaceStatus.FirstHalfBooked },
                new Booking { Id = 7, Date = new DateTime(2021, 10, 11), UserId = 5, Status = WorkPlaceStatus.SecondHalfBooked },
                new Booking { Id = 8, Date = new DateTime(2021, 10, 11), UserId = 1, Status = WorkPlaceStatus.BookedPermanently },
                new Booking { Id = 9, Date = new DateTime(2021, 10, 11), UserId = 2, Status = WorkPlaceStatus.BookedPermanently },
                new Booking { Id = 10, Date = new DateTime(2021, 10, 11), UserId = 7, Status = WorkPlaceStatus.Booked },
                new Booking { Id = 11, Date = new DateTime(2021, 10, 11), UserId = 3, Status = WorkPlaceStatus.Booked },
            });
        }

        public override async Task Remove(long id)
        {
            var place = Find(id);

            foreach (var item in place.Bookings)
            {
                await _bookingRepository.Remove(item);
            }

            await base.Remove(id);
        }

        public override async Task Remove(WorkPlace place)
        {
            foreach (var item in place.Bookings)
            {
                await _bookingRepository.Remove(item);
            }

            await base.Remove(place);
        }
    }
}

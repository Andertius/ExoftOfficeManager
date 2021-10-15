using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public class MockedBookingRepository : MockedRepository<Booking>
    {
        public MockedBookingRepository(IEnumerable<Booking> bookings)
            : base(bookings)
        {
            EnsureBookingsPopulated();
        }

        private void EnsureBookingsPopulated()
        {
            _entities.AddRange(new[]
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
    }
}

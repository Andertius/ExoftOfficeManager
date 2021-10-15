using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public class MockedWorkPlaceRepository : MockedRepository<WorkPlace>
    {
        private readonly List<Booking> _bookings;
        private int highestBookingId = 1;

        public MockedWorkPlaceRepository(IEnumerable<WorkPlace> list)
            : base(list)
        {
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

        public override async Task<WorkPlace> Update(WorkPlace entity)
        {
            int index = await Task.Run(() => _entities.FindIndex(x => x.Id == entity.Id));

            if (index == -1)
            {
                return null;
            }

            var oldValue = entity.Bookings.Where(x => x.Id == 0).FirstOrDefault();

            if (oldValue is not null)
            {
                var newValue = entity.Bookings.Where(x => x.Id == 0).First();
                newValue.Id = highestBookingId;
                highestBookingId++;
                entity.Bookings.Swap(oldValue, newValue);
            }

            _entities[index] = entity;

            return entity;
        }
    }

    internal static class SwapExtension
    {
        public static void Swap<T>(this ICollection<T> collection, T oldValue, T newValue)
        {
            if (collection is IList<T> collectionAsList)
            {
                var oldIndex = collectionAsList.IndexOf(oldValue);
                collectionAsList.RemoveAt(oldIndex);
                collectionAsList.Insert(oldIndex, newValue);
            }
            else
            {
                collection.Remove(oldValue);
                collection.Add(newValue);
            }
        }
    }
}

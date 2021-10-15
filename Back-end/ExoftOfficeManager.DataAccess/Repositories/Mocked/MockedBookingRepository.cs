using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExoftOfficeManager.DataAccess.Repositories.Mocked
{
    public class MockedBookingRepository : MockedRepository<Booking>
    {
        private readonly IServiceProvider _services;

        public MockedBookingRepository(IEnumerable<Booking> bookings, IServiceProvider services)
            : base(bookings)
        {
            _services = services;
            EnsurePopulated();
        }

        private void EnsurePopulated()
        {
            _entities.AddRange(new[]
            {
                new Booking { Id = 1, Date = new DateTime(2021, 10, 10), UserId = 1, Status = WorkPlaceStatus.BookedPermanently, WorkPlaceId = 2 },
                new Booking { Id = 2, Date = new DateTime(2021, 10, 10), UserId = 2, Status = WorkPlaceStatus.BookedPermanently, WorkPlaceId = 3 },
                new Booking { Id = 3, Date = new DateTime(2021, 10, 10), UserId = 5, Status = WorkPlaceStatus.FirstHalfBooked, WorkPlaceId = 5 },
                new Booking { Id = 4, Date = new DateTime(2021, 10, 10), UserId = 6, Status = WorkPlaceStatus.Booked, WorkPlaceId = 6 },
                new Booking { Id = 5, Date = new DateTime(2021, 10, 10), UserId = 3, Status = WorkPlaceStatus.Booked, WorkPlaceId = 10 },
                new Booking { Id = 6, Date = new DateTime(2021, 10, 11), UserId = 4, Status = WorkPlaceStatus.FirstHalfBooked, WorkPlaceId = 1 },
                new Booking { Id = 7, Date = new DateTime(2021, 10, 11), UserId = 5, Status = WorkPlaceStatus.SecondHalfBooked, WorkPlaceId = 1 },
                new Booking { Id = 8, Date = new DateTime(2021, 10, 11), UserId = 1, Status = WorkPlaceStatus.BookedPermanently, WorkPlaceId = 2 },
                new Booking { Id = 9, Date = new DateTime(2021, 10, 11), UserId = 2, Status = WorkPlaceStatus.BookedPermanently, WorkPlaceId = 3 },
                new Booking { Id = 10, Date = new DateTime(2021, 10, 11), UserId = 7, Status = WorkPlaceStatus.Booked, WorkPlaceId = 4 },
                new Booking { Id = 11, Date = new DateTime(2021, 10, 11), UserId = 3, Status = WorkPlaceStatus.Booked, WorkPlaceId = 10 },
            });
        }

        public override async Task<Booking> Add(Booking entity)
        {
            entity.Id = highestId;
            _entities.Add(entity);
            highestId++;

            var repo = (IRepository<WorkPlace>)_services.GetService(typeof(IRepository<WorkPlace>));

            WorkPlace place = repo.Find(entity.WorkPlaceId);
            place.Bookings.Add(entity);
            await repo.Update(place);

            return entity;
        }

        public override async Task<Booking> Update(Booking entity)
        {
            int index = await Task.Run(() => _entities.FindIndex(x => x.Id == entity.Id));

            if (index == -1)
            {
                return null;
            }

            var repo = (IRepository<WorkPlace>)_services.GetService(typeof(IRepository<WorkPlace>));
            var work = repo.Find(_entities[index].WorkPlaceId);
            work.Bookings.Swap(_entities[index], entity);
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

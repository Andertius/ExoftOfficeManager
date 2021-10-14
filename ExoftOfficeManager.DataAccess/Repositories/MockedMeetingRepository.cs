using System;
using System.Collections.Generic;

namespace ExoftOfficeManager.DataAccess.Repositories
{
    public class MockedMeetingRepository : MockedRepository<Meeting>
    {
        public MockedMeetingRepository(IEnumerable<Meeting> list)
            : base(list)
            => EnsurePopulated();

        private void EnsurePopulated()
            => _entities.AddRange(new[]
            {
                new Meeting { Id = 1, DateAndTime = new DateTime(2021, 10, 15, 10, 0, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 1 },
                new Meeting { Id = 3, DateAndTime = new DateTime(2021, 10, 15, 10, 30, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 1 },
                new Meeting { Id = 6, DateAndTime = new DateTime(2021, 10, 16, 11, 30, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 1 },
                new Meeting { Id = 7, DateAndTime = new DateTime(2021, 10, 15, 12, 0, 0), Duration = new TimeSpan(2, 0, 0), RoomNumber = 1 },
                new Meeting { Id = 2, DateAndTime = new DateTime(2021, 10, 15, 10, 0, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 2 },
                new Meeting { Id = 4, DateAndTime = new DateTime(2021, 10, 15, 11, 0, 0), Duration = new TimeSpan(1, 0, 0), RoomNumber = 2 },
                new Meeting { Id = 5, DateAndTime = new DateTime(2021, 10, 15, 13, 0, 0), Duration = new TimeSpan(1, 30, 0), RoomNumber = 2 },
            });
    }
}

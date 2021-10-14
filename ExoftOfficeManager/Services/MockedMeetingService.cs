using System;
using System.Collections.Generic;
using System.Linq;

using ExoftOfficeManager.Services.Interfaces;

namespace ExoftOfficeManager.Services
{
    public class MockedMeetingService : IMeetingService
    {
        private readonly List<Meeting> _meetings = new()
        {
            new Meeting { Id = 1, Date = new DateTime(2021, 10, 15), StartTime = 10, EndTime = 11, RoomNumber = 1 },
            new Meeting { Id = 2, Date = new DateTime(2021, 10, 15), StartTime = 14, EndTime = 15, RoomNumber = 2 },
            new Meeting { Id = 3, Date = new DateTime(2021, 10, 15), StartTime = 12, EndTime = 13, RoomNumber = 1 },
            new Meeting { Id = 4, Date = new DateTime(2021, 10, 15), StartTime = 12, EndTime = 13, RoomNumber = 2 },
            new Meeting { Id = 5, Date = new DateTime(2021, 10, 15), StartTime = 9, EndTime = 11, RoomNumber = 2 },
            new Meeting { Id = 6, Date = new DateTime(2021, 10, 15), StartTime = 12, EndTime = 13, RoomNumber = 1 },
            new Meeting { Id = 7, Date = new DateTime(2021, 10, 15), StartTime = 17, EndTime = 20, RoomNumber = 1 },
        };

        public IEnumerable<Meeting> GetAll()
            => _meetings.Select(x => x);

        public Meeting Find(long id)
            => _meetings.Find(x => x.Id == id);

        public void Add(Meeting meet)
            => _meetings.Add(meet);

        public bool Update(long id, Meeting meet)
        {
            int index = _meetings.FindIndex(x => x.Id == id);

            if (index == -1)
            {
                return false;
            }

            _meetings[index] = meet;
            return true;
        }

        public bool Remove(long id)
            => _meetings.Remove(_meetings.Find(x => x.Id == id));
    }
}

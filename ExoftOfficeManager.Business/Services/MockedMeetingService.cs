using System;
using System.Collections.Generic;
using System.Linq;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services
{
    public class MockedMeetingService : IMeetingService
    {
        private readonly List<Meeting> _meetings = new()
        {
            new Meeting { Id = 1, DateAndTime = new DateTime(2021, 10, 15, 10, 0, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 1 },
            new Meeting { Id = 3, DateAndTime = new DateTime(2021, 10, 15, 10, 30, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 1 },
            new Meeting { Id = 6, DateAndTime = new DateTime(2021, 10, 16, 11, 30, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 1 },
            new Meeting { Id = 7, DateAndTime = new DateTime(2021, 10, 15, 12, 0, 0), Duration = new TimeSpan(2, 0, 0), RoomNumber = 1 },
            new Meeting { Id = 2, DateAndTime = new DateTime(2021, 10, 15, 10, 0, 0), Duration = new TimeSpan(0, 30, 0), RoomNumber = 2 },
            new Meeting { Id = 4, DateAndTime = new DateTime(2021, 10, 15, 11, 0, 0), Duration = new TimeSpan(1, 0, 0), RoomNumber = 2 },
            new Meeting { Id = 5, DateAndTime = new DateTime(2021, 10, 15, 13, 0, 0), Duration = new TimeSpan(1, 30, 0), RoomNumber = 2 },
        };

        public IEnumerable<Meeting> GetAll(DateTime date)
            => _meetings.Where(meeting => meeting.DateAndTime == date);

        public IEnumerable<TimeSpan> GetAllAvailableHours(DateTime date, int room)
        {
            var meetings = _meetings.Where(x => x.DateAndTime.Date == date);
            var result = new List<TimeSpan>();

            for (int i = 0; i < 16; i++)
            {
                var time = new TimeSpan(10 + i / 2, i % 2 == 0 ? 0 : 30, 0);

                if (!_meetings.Where(meeting => CheckIfTimeIsInAMeeting(meeting, date, room, time)).Any())
                {
                    result.Add(time);
                }
            }

            return result;
        }

        public Meeting Find(long id)
            => _meetings.Find(x => x.Id == id);

        public bool Add(Meeting meet)
        {
            if (meet.DateAndTime.TimeOfDay < new TimeSpan(10, 0, 0) ||
                meet.DateAndTime.TimeOfDay >= new TimeSpan(18, 0, 0))
            {
                return false;
            }

            if (!_meetings.Where(meeting => CheckIfMeetingsIntersect(meeting, meet)).Any())
            {
                _meetings.Add(meet);
                meet.Id = _meetings.LongCount();
                return true;
            }

            return false;
        }

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

        private static bool CheckIfTimeIsInAMeeting(Meeting meeting, DateTime date, int room, TimeSpan time)
            => date == meeting.DateAndTime.Date &&
               meeting.RoomNumber == room &&
               meeting.DateAndTime.TimeOfDay <= time &&
               time < meeting.DateAndTime.TimeOfDay + meeting.Duration;

        private static bool CheckIfMeetingsIntersect(Meeting left, Meeting right)
            => left.DateAndTime.Date == right.DateAndTime.Date &&
               left.RoomNumber == right.RoomNumber &&
               (left.DateAndTime.TimeOfDay <= right.DateAndTime.TimeOfDay &&
               right.DateAndTime.TimeOfDay < left.DateAndTime.TimeOfDay + left.Duration ||
               right.DateAndTime.TimeOfDay <= left.DateAndTime.TimeOfDay &&
               left.DateAndTime.TimeOfDay < right.DateAndTime.TimeOfDay + right.Duration);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.Services
{
    public class MeetingService : IMeetingService
    {
        private readonly IRepository<Meeting> _repository;

        public MeetingService(IRepository<Meeting> repository)
            => _repository = repository;

        public IEnumerable<Meeting> GetAll(DateTime date, IEnumerable<string> inclusion)
            => _repository.GetAll(inclusion).Where(meeting => meeting.DateAndTime.Date == date).ToList();

        public IEnumerable<TimeSpan> GetAllAvailableHours(DateTime date, int room)
        {
            var meetings = GetAll(date, Array.Empty<string>());
            var result = new List<TimeSpan>();

            for (int i = 0; i < 16; i++)
            {
                var time = new TimeSpan(10 + i / 2, i % 2 == 0 ? 0 : 30, 0);

                if (!meetings.Where(meeting => CheckIfTimeIsInAMeeting(meeting, room, time)).Any())
                {
                    result.Add(time);
                }
            }

            return result;
        }

        public async Task<Meeting> Find(long id, IEnumerable<string> inclusion)
            => await _repository.Find(id, inclusion);

        public async Task<bool> Add(Meeting meet)
        {
            if (meet.DateAndTime.TimeOfDay < new TimeSpan(10, 0, 0) ||
                meet.DateAndTime.TimeOfDay >= new TimeSpan(18, 0, 0))
            {
                return false;
            }

            if (!GetAll(meet.DateAndTime.Date, Array.Empty<string>())
                    .Where(meeting => CheckIfMeetingsIntersect(meeting, meet))
                    .Any())
            {
                await _repository.Add(meet);
                await _repository.Commit();
                return true;
            }

            return false;
        }

        public async Task<Meeting> Update(Meeting meet)
        {
            var result = _repository.Update(meet);
            await _repository.Commit();

            return result;
        }

        public async Task Remove(long id)
        {
            await _repository.Remove(id);
            await _repository.Commit();
        }

        private static bool CheckIfTimeIsInAMeeting(Meeting meeting, int room, TimeSpan time)
            => meeting.RoomNumber == room &&
               meeting.DateAndTime.TimeOfDay <= time &&
               time < meeting.DateAndTime.TimeOfDay + meeting.Duration;

        private static bool CheckIfMeetingsIntersect(Meeting left, Meeting right)
            => left.RoomNumber == right.RoomNumber &&
               (left.DateAndTime.TimeOfDay <= right.DateAndTime.TimeOfDay &&
               right.DateAndTime.TimeOfDay < left.DateAndTime.TimeOfDay + left.Duration ||
               right.DateAndTime.TimeOfDay <= left.DateAndTime.TimeOfDay &&
               left.DateAndTime.TimeOfDay < right.DateAndTime.TimeOfDay + right.Duration);
    }
}

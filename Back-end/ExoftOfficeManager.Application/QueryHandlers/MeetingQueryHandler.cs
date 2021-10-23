using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.QueryHandlers
{
    public class MeetingQueryHandler : IMeetingQueryHandler
    {
        private readonly IRepository<Meeting> _repository;

        public MeetingQueryHandler(IRepository<Meeting> repo)
        {
            _repository = repo;
        }

        public IEnumerable<Meeting> GetAllQuery(DateTime date)
            => _repository.GetAll().Where(meeting => meeting.DateAndTime.Date == date).ToList();

        public IEnumerable<TimeSpan> GetAllAvailableHoursQuery(DateTime date, int room)
        {
            var meetings = GetAllQuery(date);
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

        public async Task<Meeting> FindQuery(long meetingId)
            => await _repository.Find(meetingId);

        private static bool CheckIfTimeIsInAMeeting(Meeting meeting, int room, TimeSpan time)
            => meeting.RoomNumber == room &&
               meeting.DateAndTime.TimeOfDay <= time &&
               time < meeting.DateAndTime.TimeOfDay + meeting.Duration;
    }
}

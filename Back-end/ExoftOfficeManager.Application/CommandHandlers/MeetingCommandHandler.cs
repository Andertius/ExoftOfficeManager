using System;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Domain.Entities;

namespace ExoftOfficeManager.Application.CommandHandlers
{
    public class MeetingCommandHandler : IMeetingCommandHandler
    {
        private readonly IRepository<Meeting> _repository;

        public async Task AddCommand(Meeting meet)
        {
            if (meet.DateAndTime.TimeOfDay < new TimeSpan(10, 0, 0) ||
                meet.DateAndTime.TimeOfDay >= new TimeSpan(18, 0, 0))
            {
                throw new ArgumentOutOfRangeException(nameof(meet), $"{meet.DateAndTime.TimeOfDay} is out of available time range (10 - 18)");
            }

            if (meet.DateAndTime.TimeOfDay.TotalMinutes % 30 != 0)
            {
                throw new ArgumentException($"Duration of the meeting '{meet.DateAndTime.TimeOfDay}' is not divisible by 30");
            }

            var meetings = _repository.GetAll().Where(meeting => meeting.DateAndTime.Date == meet.DateAndTime.Date).ToList();

            if (!meetings.Where(meeting => CheckIfMeetingsIntersect(meeting, meet)).Any())
            {
                await _repository.Add(meet);
                await _repository.Commit();
            }

            throw new ArgumentOutOfRangeException(nameof(meet), $"There is already a meeting at {meet.DateAndTime.TimeOfDay}");
        }

        public async Task UpdateCommand(Meeting meet)
        {
            _repository.Update(meet);
            await _repository.Commit();
        }

        public async Task RemoveCommand(long meetingId)
        {
            _repository.Remove(meetingId);
            await _repository.Commit();
        }

        private static bool CheckIfMeetingsIntersect(Meeting left, Meeting right)
            => left.RoomNumber == right.RoomNumber &&
               (left.DateAndTime.TimeOfDay <= right.DateAndTime.TimeOfDay &&
               right.DateAndTime.TimeOfDay < left.DateAndTime.TimeOfDay + left.Duration ||
               right.DateAndTime.TimeOfDay <= left.DateAndTime.TimeOfDay &&
               left.DateAndTime.TimeOfDay < right.DateAndTime.TimeOfDay + right.Duration);
    }
}

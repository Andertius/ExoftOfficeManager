using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours
{
    public class GetAvailableHoursQueryHandler : IRequestHandler<GetAvailableHoursQuery, GetAvailableHoursQueryResponse[]>
    {
        private readonly IMeetingRepository _repository;

        public GetAvailableHoursQueryHandler(IMeetingRepository repo)
        {
            _repository = repo;
        }

        public async Task<GetAvailableHoursQueryResponse[]> Handle(GetAvailableHoursQuery request, CancellationToken cancellationToken)
        {
            var meetings = await _repository.GetAllMeetings(request.Date);
            var availableHours = new List<TimeSpan>();

            for (int i = 0; i < 16; i++)
            {
                var time = new TimeSpan(10 + i / 2, i % 2 == 0 ? 0 : 30, 0);

                if (!meetings.Where(meeting => CheckIfTimeIsInAMeeting(meeting, request.RoomNumber, time)).Any())
                {
                    availableHours.Add(time);
                }
            }

            return availableHours.Select(x => new GetAvailableHoursQueryResponse(x)).ToArray();
        }

        private static bool CheckIfTimeIsInAMeeting(Meeting meeting, int room, TimeSpan time)
            => meeting.RoomNumber == room &&
               meeting.DateAndTime.TimeOfDay <= time &&
               time < meeting.DateAndTime.TimeOfDay + meeting.Duration;
    }
}

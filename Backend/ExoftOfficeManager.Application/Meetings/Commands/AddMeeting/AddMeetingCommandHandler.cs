using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Exceptions.Meetings;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.AddMeeting
{
    public class AddMeetingCommandHandler : IRequestHandler<AddMeetingCommand>
    {
        private readonly IMeetingRepository _repository;

        public AddMeetingCommandHandler(IMeetingRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(AddMeetingCommand request, CancellationToken cancellationToken)
        {
            var meetings = await _repository.GetAllMeetings(request.Meeting.DateAndTime.Date);

            if (!meetings.Where(meeting => CheckIfMeetingsIntersect(meeting, request.Meeting)).Any())
            {
                await _repository.AddMeeting(request.Meeting);
                await _repository.Commit();
            }
            else
            {
                throw new MeetingsIntersectException("Cannot book a meeting because it is intersecting with another one.");
            }

            return Unit.Value;
        }

        private static bool CheckIfMeetingsIntersect(Meeting left, Meeting right)
           => left.RoomNumber == right.RoomNumber &&
              (left.DateAndTime.TimeOfDay <= right.DateAndTime.TimeOfDay &&
              right.DateAndTime.TimeOfDay < left.DateAndTime.TimeOfDay + left.Duration ||
              right.DateAndTime.TimeOfDay <= left.DateAndTime.TimeOfDay &&
              left.DateAndTime.TimeOfDay < right.DateAndTime.TimeOfDay + right.Duration);
    }
}

using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting
{
    public class RemoveMeetingCommandHandler : IRequestHandler<RemoveMeetingCommand>
    {
        private readonly IMeetingRepository _repository;

        public RemoveMeetingCommandHandler(IMeetingRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(RemoveMeetingCommand request, CancellationToken cancellationToken)
        {
            await _repository.RemoveMeeting(request.MeetingId);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.UpdateMeeting
{
    public class UpdateMeetingCommandHandler : IRequestHandler<UpdateMeetingCommand>
    {
        private readonly IMeetingRepository _repository;

        public UpdateMeetingCommandHandler(IMeetingRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
        {
            _repository.UpdateMeeting(request.Meeting);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Queries.FindMeetingById
{
    public class FindMeetingByIdQueryHandler : IRequestHandler<FindMeetingByIdQuery, MeetingsQueryResponse>
    {
        private readonly IMeetingRepository _repository;

        public FindMeetingByIdQueryHandler(IMeetingRepository repo)
        {
            _repository = repo;
        }

        public async Task<MeetingsQueryResponse> Handle(FindMeetingByIdQuery request, CancellationToken cancellationToken)
        {
            var meeting = await _repository.FindMeetingById(request.MeetingId);
            return new MeetingsQueryResponse(MeetingMapper.MapIntoDto(meeting));
        }
    }
}

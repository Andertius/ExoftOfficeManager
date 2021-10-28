using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Queries.GetMeetings
{
    public class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, MeetingsQueryResponse[]>
    {
        private readonly IMeetingRepository _repository;

        public GetMeetingsQueryHandler(IMeetingRepository repo)
        {
            _repository = repo;
        }

        public async Task<MeetingsQueryResponse[]> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
        {
            var getMeetingsQueryDtos = await _repository.GetAllMeetings(request.MeetingDate);
            return getMeetingsQueryDtos.Select(x => new MeetingsQueryResponse(x)).ToArray();
        }
    }
}

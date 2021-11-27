using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
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
            var meetings = await _repository.GetAllMeetings(request.MeetingDate);
            return meetings
                .Select(x => new MeetingsQueryResponse(MeetingMapper.MapIntoDto(x)))
                .ToArray();
        }
    }
}

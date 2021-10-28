using ExoftOfficeManager.Domain.Dtos;

namespace ExoftOfficeManager.Application.Meetings.Queries
{
    public class MeetingsQueryResponse
    {
        public MeetingsQueryResponse(MeetingDto meet)
        {
            Meeting = meet;
        }

        public MeetingDto Meeting { get; private set; }
    }
}

using System;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Queries.GetMeetings
{
    public class GetMeetingsQuery : IRequest<MeetingsQueryResponse[]>
    {
        public GetMeetingsQuery(DateTime meetingDate)
        {
            MeetingDate = meetingDate;
        }

        public DateTime MeetingDate { get; private set; }
    }
}

using System;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Queries.FindMeetingById
{
    public class FindMeetingByIdQuery : IRequest<MeetingsQueryResponse>
    {
        public FindMeetingByIdQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; set; }
    }
}

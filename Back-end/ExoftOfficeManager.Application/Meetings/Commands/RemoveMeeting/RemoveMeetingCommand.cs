using System;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting
{
    public class RemoveMeetingCommand : IRequest
    {
        public RemoveMeetingCommand(Guid meetId)
        {
            MeetingId = meetId;
        }

        public Guid MeetingId { get; set; }
    }
}

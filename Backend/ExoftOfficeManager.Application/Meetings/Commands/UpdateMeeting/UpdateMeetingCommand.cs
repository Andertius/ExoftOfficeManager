using ExoftOfficeManager.Domain.Entities;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.UpdateMeeting
{
    public class UpdateMeetingCommand : IRequest
    {
        public UpdateMeetingCommand(Meeting meet)
        {
            Meeting = meet;
        }

        public Meeting Meeting { get; set; }
    }
}

using ExoftOfficeManager.Domain.Entities;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.AddMeeting
{
    public class AddMeetingCommand : IRequest
    {
        public AddMeetingCommand(Meeting meeting)
        {
            Meeting = meeting;
        }

        public Meeting Meeting { get; set; }
    }
}

using ExoftOfficeManager.Domain.Dtos;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.AddMeeting
{
    public class AddMeetingCommand : IRequest
    {
        public AddMeetingCommand(MeetingDto meeting)
        {
            Meeting = meeting;
        }

        public MeetingDto Meeting { get; set; }
    }
}

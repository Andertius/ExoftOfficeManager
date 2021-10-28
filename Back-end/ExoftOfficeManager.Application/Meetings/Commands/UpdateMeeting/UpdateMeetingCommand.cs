using ExoftOfficeManager.Domain.Dtos;

using MediatR;

namespace ExoftOfficeManager.Application.Meetings.Commands.UpdateMeeting
{
    public class UpdateMeetingCommand : IRequest
    {
        public UpdateMeetingCommand(MeetingDto meet)
        {
            Meeting = meet;
        }

        public MeetingDto Meeting { get; set; }
    }
}

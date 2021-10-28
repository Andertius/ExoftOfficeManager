using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Meetings.Commands.AddMeeting;
using ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting;
using ExoftOfficeManager.Application.Meetings.Queries.FindMeetingById;
using ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours;
using ExoftOfficeManager.Application.Meetings.Queries.GetMeetings;
using ExoftOfficeManager.Domain.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("meetings/get-all-meetings")]
        public async Task<IActionResult> GetAllMeetings(DateTime date)
        {
            var meetings = await _mediator.Send(new GetMeetingsQuery(date.Date));
            return Ok(meetings);
        }

        [HttpGet("meetings/get-all-available-hours")]
        public async Task<IActionResult> GetAllAvailableHours(DateTime date, int room)
        {
            var hours = await _mediator.Send(new GetAvailableHoursQuery(date.Date, room));
            return Ok(hours);
        }

        [HttpGet("meetings/{meetingId}/find")]
        public async Task<IActionResult> Find(Guid meetingId)
        {
            var meeting = await _mediator.Send(new FindMeetingByIdQuery(meetingId));
            return Ok(meeting);
        }

        [HttpGet("meetings/reserve-meeting")]
        public async Task<IActionResult> ReserveMeeting(DateTime dateAndTime, int durationMins, int room, string purpose)
        {
            var meet = new MeetingDto
            {
                DateAndTime = dateAndTime,
                Duration = new TimeSpan(0, durationMins, 0),
                RoomNumber = room,
                MeetingPurpose = purpose,
            };

            await _mediator.Send(new AddMeetingCommand(meet));
            return Ok();
        }

        [HttpGet("meetings/{meetingId}/cancel-meeting")]
        public async Task<IActionResult> CancelMeeting(Guid meetingId)
        {
            await _mediator.Send(new RemoveMeetingCommand(meetingId));
            return Ok();
        }
    }
}

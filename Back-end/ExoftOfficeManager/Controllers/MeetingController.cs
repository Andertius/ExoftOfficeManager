using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Meetings.Commands.AddMeeting;
using ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting;
using ExoftOfficeManager.Application.Meetings.Queries.FindMeetingById;
using ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours;
using ExoftOfficeManager.Application.Meetings.Queries.GetMeetings;
using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Requests;

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

        [HttpGet("meetings")]
        public async Task<IActionResult> GetAllMeetings([FromQuery] DateTime date)
        {
            var meetings = await _mediator.Send(new GetMeetingsQuery(date.Date));
            return Ok(meetings);
        }

        [HttpGet("meetings/available-hours")]
        public async Task<IActionResult> GetAllAvailableHours([FromQuery] DateTime date, [FromQuery] int room)
        {
            var hours = await _mediator.Send(new GetAvailableHoursQuery(date.Date, room));
            return Ok(hours);
        }

        [HttpGet("meetings/{meetingId}/meeting")]
        public async Task<IActionResult> Find([FromRoute] Guid meetingId)
        {
            var meeting = await _mediator.Send(new FindMeetingByIdQuery(meetingId));
            return Ok(meeting);
        }

        [HttpPost("meetings/{room}/reserve-meeting")]
        public async Task<IActionResult> ReserveMeeting([FromBody] ReserveMeetingRequest request)
        {
            var meet = new MeetingDto
            {
                DateAndTime = request.DateAndTime,
                Duration = new TimeSpan(0, request.DurationMinutes, 0),
                RoomNumber = request.RoomNumber,
                MeetingPurpose = request.MeetingPurpose,
            };

            await _mediator.Send(new AddMeetingCommand(meet));
            return NoContent();
        }

        [HttpDelete("meetings/{meetingId}/cancel-meeting")]
        public async Task<IActionResult> CancelMeeting([FromRoute] Guid meetingId)
        {
            await _mediator.Send(new RemoveMeetingCommand(meetingId));
            return NoContent();
        }
    }
}

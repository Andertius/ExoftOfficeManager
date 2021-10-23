using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        //private readonly IMeetingService _meetingService;

        private readonly IMeetingCommandHandler _meetingCommands;
        private readonly IMeetingQueryHandler _meetingQueries;

        public MeetingController(IMeetingCommandHandler commands, IMeetingQueryHandler queries)
        {
            _meetingCommands = commands;
            _meetingQueries = queries;
        }

        [HttpGet("get-all-meetings")]
        public async Task<IActionResult> GetAllMeetings(DateTime date)
            => await Task.Run(() => Ok(_meetingQueries.GetAllQuery(date)));

        [HttpGet("get-all-available-hours")]
        public async Task<IActionResult> GetAllAvailableHours(DateTime date, int room)
            => await Task.Run(() => Ok(_meetingQueries.GetAllAvailableHoursQuery(date, room)));

        [HttpGet("find")]
        public async Task<IActionResult> Find(long meetingId)
            => Ok(await _meetingQueries.FindQuery(meetingId));

        [HttpGet("reserve-meeting")]
        public async Task<IActionResult> ReserveMeeting(DateTime dateAndTime, int durationMins, int room)
        {
            var meet = new Meeting { DateAndTime = dateAndTime, Duration = new TimeSpan(0, durationMins, 0), RoomNumber = room };
            await _meetingCommands.AddCommand(meet);
            return Ok();
        }

        [HttpGet("cancel-meeting")]
        public async Task<IActionResult> CancelMeeting(long id)
        {
            await _meetingCommands.RemoveCommand(id);
            return Ok();
        }
    }
}

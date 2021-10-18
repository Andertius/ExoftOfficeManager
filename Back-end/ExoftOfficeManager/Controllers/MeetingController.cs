﻿using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meeting)
        {
            _meetingService = meeting;
        }

        [HttpGet("get-all-meetings")]
        public async Task<IActionResult> GetAllMeetings(DateTime date)
            => await Task.Run(() => Ok(_meetingService.GetAll(date)));

        [HttpGet("get-all-available-hours")]
        public async Task<IActionResult> GetAllAvailableHours(DateTime date, int room)
            => await Task.Run(() => Ok(_meetingService.GetAllAvailableHours(date, room)));

        [HttpGet("find")]
        public async Task<IActionResult> Find(long meetingId)
            => await Task.Run(() => Ok(_meetingService.Find(meetingId)));

        [HttpGet("reserve-meeting")]
        public async Task<IActionResult> ReserveMeeting(DateTime dateAndTime, int durationMins, int room)
        {
            var meet = new Meeting { DateAndTime = dateAndTime, Duration = new TimeSpan(0, durationMins, 0), RoomNumber = room };
            return await _meetingService.Add(meet) ? Ok() : BadRequest();
        }

        [HttpGet("cancel-meeting")]
        public async Task<IActionResult> CancelMeeting(long id)
        {
            await _meetingService.Remove(id);
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IWorkPlaceService _workPlaceService;
        private readonly IMeetingService _meetingService;

        public AdminController(
            IWorkPlaceService workPlace,
            IMeetingService meeting)
        {
            _workPlaceService = workPlace;
            _meetingService = meeting;
        }

        [HttpGet("cancel-meeting")]
        public async Task<IActionResult> CancelMeeting(long meetingId)
        {
            await _meetingService.Remove(meetingId);
            return Ok();
        }
    }
}

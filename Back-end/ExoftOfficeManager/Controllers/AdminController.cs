using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IWorkPlaceService _workPlaceService;
        private readonly IUserService _userService;
        private readonly IMeetingService _meetingService;

        public AdminController(
            ILogger<AdminController> logger,
            IWorkPlaceService workPlace,
            IUserService user,
            IMeetingService meeting)
        {
            _logger = logger;
            _workPlaceService = workPlace;
            _userService = user;
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

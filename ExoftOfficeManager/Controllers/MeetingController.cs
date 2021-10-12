using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IMeetingService _meetingService;

        public MeetingController(ILogger<MeetingController> logger, IMeetingService meeting)
        {
            _logger = logger;
            _meetingService = meeting;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_meetingService.GetAll()));

        [HttpGet("find")]
        public async Task<IActionResult> Find(long meetingId)
            => await Task.Run(() => Ok(_meetingService.Find(meetingId)));

        [HttpGet("remove")]
        public async Task<IActionResult> Remove(long meetingId)
        {
            if (await Task.Run(() => _meetingService.Remove(meetingId)))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

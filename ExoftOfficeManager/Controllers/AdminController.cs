using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;

        public AdminController(ILogger<AdminController> logger, IAdminService admin)
        {
            _logger = logger;
            _adminService = admin;
        }

        [HttpGet("removeMeeting")]
        public async Task<IActionResult> Remove(long meetingId)
        {
            if (await Task.Run(() => _adminService.RemoveMeeting(meetingId)))
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

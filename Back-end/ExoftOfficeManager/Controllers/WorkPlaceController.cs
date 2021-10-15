using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkPlaceController : ControllerBase
    {
        private readonly ILogger<WorkPlaceController> _logger;
        private readonly IWorkPlaceService _placeService;
        private readonly IUserService _developerService;

        public WorkPlaceController(ILogger<WorkPlaceController> logger, IWorkPlaceService work, IUserService developerService)
        {
            _logger = logger;
            _placeService = work;
            _developerService = developerService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_placeService.GetAll()));

        [HttpGet("get-all-booked")]
        public async Task<IActionResult> GetBooked([FromQuery] DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllBooked(date.Date)));

        [HttpGet("get-all-available")]
        public async Task<IActionResult> GetAllAvailable([FromQuery] DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllAvailable(date.Date)));

        [HttpGet("find-workplace")]
        public async Task<IActionResult> FindWorkPlace([FromQuery] long placeId)
            => await Task.Run(() => Ok(_placeService.Find(placeId)));

        [HttpGet("book")]
        public async Task<IActionResult> Book(
            [FromQuery] long placeId,
            [FromQuery] long devId,
            [FromQuery] WorkPlaceStatus status,
            [FromQuery] DateTime date,
            [FromQuery] int days)
        {
            await _placeService.Book(placeId, devId, status, date, days);
            return Ok();
        }

        [HttpGet("cancel-reservation")]
        public async Task<IActionResult> CancelReservation(
            [FromQuery] long placeId,
            [FromQuery] DateTime date,
            [FromQuery] long devId)
        {
            await _placeService.MakeAvailable(placeId, date, devId);
            return Ok();
        }

        [HttpGet("find-developer")]
        public async Task<IActionResult> FindDeveloper(long id)
        {
            return await Task.Run(() => Ok(_developerService.Find(id)));
        }
    }
}

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
    public class WorkPlaceController : ControllerBase
    {
        private readonly ILogger<WorkPlaceController> _logger;
        private readonly IWorkPlaceService _placeService;

        public WorkPlaceController(ILogger<WorkPlaceController> logger, IWorkPlaceService work)
        {
            _logger = logger;
            _placeService = work;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_placeService.GetAll()));

        [HttpGet("getalloccupied")]
        public async Task<IActionResult> GetAllOccupied()
            => await Task.Run(() => Ok(_placeService.GetAllOccupied()));

        [HttpGet("getallunoccupied")]
        public async Task<IActionResult> GetAllUnoccupied()
            => await Task.Run(() => Ok(_placeService.GetAllUnoccupied()));

        [HttpGet("find")]
        public async Task<IActionResult> Find(long placeId)
            => await Task.Run(() => Ok(_placeService.Find(placeId)));

        [HttpGet("changeoccupation")]
        public async Task<IActionResult> ChangeOccupation(long placeId, long devId)
        {
            if (await Task.Run(() => _placeService.ChangeOccupation(placeId, devId)))
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

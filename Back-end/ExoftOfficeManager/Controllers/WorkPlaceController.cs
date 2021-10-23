using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Domain;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkPlaceController : ControllerBase
    {
        //private readonly IWorkPlaceService _placeService;

        private readonly IWorkPlaceCommandHandler _placeCommands;
        private readonly IWorkPlaceQueryHandler _placeQueries;

        public WorkPlaceController(IWorkPlaceCommandHandler commands, IWorkPlaceQueryHandler queries)
        {
            _placeCommands = commands;
            _placeQueries = queries;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_placeQueries.GetAllQuery()));

        [HttpGet("get-all-booked")]
        public async Task<IActionResult> GetBooked(DateTime date)
            => await Task.Run(() => Ok(_placeQueries.GetAllBookedQuery(date.Date)));

        [HttpGet("get-all-available")]
        public async Task<IActionResult> GetAllAvailable(DateTime date)
            => await Task.Run(() => Ok(_placeQueries.GetAllAvailableQuery(date.Date)));

        [HttpGet("find-workplace")]
        public async Task<IActionResult> FindWorkPlace(long placeId)
            => Ok(await _placeQueries.FindQuery(placeId));

        [HttpGet("book")]
        public async Task<IActionResult> Book(long placeId, long devId, BookingType bookingType, DateTime date, int days)
        {
            if (days > 1)
            {
                await _placeCommands.BookCommand(placeId, devId, bookingType, date, days);
            }
            else
            {
                await _placeCommands.BookCommand(placeId, devId, bookingType, date, days);
            }

            return Ok();
        }
    }
}

using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkPlaceController : ControllerBase
    {
        private readonly IWorkPlaceService _placeService;
        private readonly IUserService _userService;

        public WorkPlaceController(IWorkPlaceService work, IUserService userService)
        {
            _placeService = work;
            _userService = userService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_placeService.GetAll(new[] { nameof(WorkPlace.Bookings) })));

        [HttpGet("get-all-booked")]
        public async Task<IActionResult> GetBooked([FromQuery] DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllBooked(date.Date, new[] { nameof(WorkPlace.Bookings) })));

        [HttpGet("get-all-available")]
        public async Task<IActionResult> GetAllAvailable([FromQuery] DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllAvailable(date.Date, new[] { nameof(WorkPlace.Bookings) })));

        [HttpGet("find-workplace")]
        public async Task<IActionResult> FindWorkPlace([FromQuery] long placeId)
            => Ok(await _placeService.Find(placeId, new[] { nameof(WorkPlace.Bookings) }));

        [HttpGet("book")]
        public async Task<IActionResult> Book(
            [FromQuery] long placeId,
            [FromQuery] long devId,
            [FromQuery] WorkPlaceStatus status,
            [FromQuery] DateTime date,
            [FromQuery] int days)
        {
            if (days > 1)
            {
                // TODO figure out what this is supposed to be
            }
            else
            {
                await _placeService.Book(placeId, devId, status, date, days);
            }

            return Ok();
        }

        [HttpGet("get-all-users-bookings")]
        public async Task<IActionResult> GetAllUsersBookings([FromQuery] long devId)
            => await Task.Run(() => Ok(_placeService.GetAllUserBooked(devId, new[] { nameof(Booking.User), nameof(Booking.WorkPlace) })));

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
            return Ok(await _userService.Find(id, Array.Empty<string>()));
        }
    }
}

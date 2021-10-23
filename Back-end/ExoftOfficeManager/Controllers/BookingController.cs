using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.QueryHandlers.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        //private readonly IBookingService _bookingService;

        private readonly IBookingCommandHandler _bookingCommands;
        private readonly IBookingQueryHandler _bookingQueries;

        public BookingController(IBookingCommandHandler commands, IBookingQueryHandler queries)
        {
            _bookingCommands = commands;
            _bookingQueries = queries;
        }

        [HttpGet("get-all-users-bookings")]
        public async Task<IActionResult> GetAllUsersBookings(long devId)
            => await Task.Run(() => Ok(_bookingQueries.GetAllUserBookedQuery(devId)));

        [HttpGet("cancel-booking")]
        public async Task<IActionResult> RemoveBooking(long placeId, DateTime date, long devId)
        {
            var booking = await _bookingQueries.FindQuery(placeId, date, devId);
            await _bookingCommands.RemoveCommand(booking.Id);
            return Ok();
        }

        [HttpGet("find-booking-by-id")]
        public async Task<IActionResult> FindBooking(long bookingId)
            => Ok(await _bookingQueries.FindQuery(bookingId));

        [HttpGet("find-booking")]
        public async Task<IActionResult> FindBooking(long placeId, DateTime date, long userId)
            => Ok(await _bookingQueries.FindQuery(placeId, date, userId));
    }
}

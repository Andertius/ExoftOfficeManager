using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.QueryHandlers;
using ExoftOfficeManager.Application.QueryHandlers.Interfaces;

using MediatR;

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
        private readonly IMediator _mediator;

        public BookingController(IBookingCommandHandler commands, IBookingQueryHandler queries, IMediator mediator)
        {
            _bookingCommands = commands;
            _bookingQueries = queries;
            _mediator = mediator;
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings([FromQuery] DateTime bookingDate)
        {
            var bookings = await _mediator.Send(new GetBookingsQuery(bookingDate));

            return Ok(bookings);
        }

        [HttpGet("users/{userId}/bookings")]
        public async Task<IActionResult> GetAllUsersBookings(long userId)
            => await Task.Run(() => Ok(_bookingQueries.GetAllUserBookedQuery(userId)));

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

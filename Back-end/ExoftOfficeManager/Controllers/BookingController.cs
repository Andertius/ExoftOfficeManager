using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.RemoveBooking;
using ExoftOfficeManager.Application.Bookings.Queries.FindBooking;
using ExoftOfficeManager.Application.Bookings.Queries.GetBookings;
using ExoftOfficeManager.Application.Bookings.Queries.GetBookingsByUser;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings([FromQuery] DateTime bookingDate)
        {
            var bookings = await _mediator.Send(new GetBookingsQuery(bookingDate));
            return Ok(bookings);
        }

        [HttpGet("users/{userId}/bookings")]
        public async Task<IActionResult> GetBookingsByUser([FromRoute] Guid userId)
        {
            var bookings = await _mediator.Send(new GetBookingsByUserQuery(userId));
            return Ok(bookings);
        }

        [HttpGet("bookings/cancel-booking")]
        public async Task<IActionResult> RemoveBooking(Guid placeId, DateTime date, Guid userId)
        {
            await _mediator.Send(new RemoveBookingCommand(placeId, date, userId));
            return Ok();
        }

        [HttpGet("bookings/find-booking-by-id")]
        public async Task<IActionResult> FindBooking(Guid bookingId)
        {
            var booking = await _mediator.Send(new FindBookingQuery(bookingId));
            return Ok(booking);
        }

        [HttpGet("bookings/find-booking")]
        public async Task<IActionResult> FindBooking(Guid placeId, DateTime date, Guid userId)
        {
            var booking = await _mediator.Send(new FindBookingQuery(placeId, date, userId));
            return Ok(booking);
        }
    }
}

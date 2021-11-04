using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingByWorkplace;
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

        [HttpDelete("bookings/cancel-booking")]
        public async Task<IActionResult> RemoveBooking(
            [FromQuery] Guid placeId,
            [FromQuery] DateTime date,
            [FromQuery] Guid userId)
        {
            await _mediator.Send(new RemoveBookingByWorkplaceCommand(placeId, date, userId));
            return NoContent();
        }

        [HttpGet("bookings/{bookingId}/find-booking-by-id")]
        public async Task<IActionResult> FindBooking([FromRoute] Guid bookingId)
        {
            var booking = await _mediator.Send(new FindBookingQuery(bookingId));
            return Ok(booking);
        }

        [HttpGet("bookings/find-booking")]
        public async Task<IActionResult> FindBooking(
            [FromQuery] Guid placeId,
            [FromQuery] DateTime date,
            [FromQuery] Guid userId)
        {
            var booking = await _mediator.Send(new FindBookingQuery(placeId, date, userId));
            return Ok(booking);
        }
    }
}

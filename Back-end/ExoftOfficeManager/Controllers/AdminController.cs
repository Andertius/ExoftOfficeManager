using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.UpdateBooking;
using ExoftOfficeManager.Application.Bookings.Queries.FindBooking;
using ExoftOfficeManager.Application.Bookings.Queries.GetPendingBookings;
using ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting;
using ExoftOfficeManager.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("meetings/{meetingId}/cancel-meeting")]
        public async Task<IActionResult> CancelMeeting([FromRoute] Guid meetingId)
        {
            await _mediator.Send(new RemoveMeetingCommand(meetingId));
            return Ok();
        }

        [HttpGet("bookings/get-all-pending-bookings")]
        public async Task<IActionResult> GetAllPendingBooking()
        {
            await _mediator.Send(new GetPendingBookingsQuery());
            return Ok();
        }

        [HttpGet("bookings/{bookingId}/approve-booking")]
        public async Task<IActionResult> ApproveBooking([FromRoute] Guid bookingId)
        {
            var booking = await _mediator.Send(new FindBookingQuery(bookingId));
            booking.Booking.Status = BookingStatus.Approved;
            await _mediator.Send(new UpdateBookingCommand(booking.Booking));
            return Ok();
        }

        [HttpGet("bookings/{bookingId}/decline-booking")]
        public async Task<IActionResult> DeclineBooking([FromRoute] Guid bookingId)
        {
            var booking = await _mediator.Send(new FindBookingQuery(bookingId));
            booking.Booking.Status = BookingStatus.Declined;
            await _mediator.Send(new UpdateBookingCommand(booking.Booking));
            return Ok();
        }
    }
}

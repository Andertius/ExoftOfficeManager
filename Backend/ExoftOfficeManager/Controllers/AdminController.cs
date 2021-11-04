using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.ChangeBookingStatus;
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

        [HttpDelete("meetings/{meetingId}/cancel-meeting")]
        public async Task<IActionResult> CancelMeeting([FromRoute] Guid meetingId)
        {
            await _mediator.Send(new RemoveMeetingCommand(meetingId));
            return NoContent();
        }

        [HttpGet("bookings/pending-bookings")]
        public async Task<IActionResult> GetAllPendingBooking()
        {
            var pendingBookings = await _mediator.Send(new GetPendingBookingsQuery());
            return Ok(pendingBookings);
        }

        [HttpPut("bookings/{bookingId}/approve-booking")]
        public async Task<IActionResult> ApproveBooking([FromRoute] Guid bookingId)
        {
            await _mediator.Send(new ChangeBookingStatusCommand(bookingId, BookingStatus.Approved));
            return NoContent();
        }

        [HttpPut("bookings/{bookingId}/decline-booking")]
        public async Task<IActionResult> DeclineBooking([FromRoute] Guid bookingId)
        {
            await _mediator.Send(new ChangeBookingStatusCommand(bookingId, BookingStatus.Declined));
            return NoContent();
        }
    }
}

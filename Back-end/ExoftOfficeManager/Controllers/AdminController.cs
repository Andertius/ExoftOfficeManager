using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.CommandHandlers.Interfaces;
using ExoftOfficeManager.Application.QueryHandlers.Interfaces;
using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        //private readonly IMeetingService _meetingService;
        //private readonly IBookingService _bookingService;

        private readonly IMeetingCommandHandler _meetingCommands;
        private readonly IMeetingQueryHandler _meetingQueries;

        private readonly IBookingCommandHandler _bookingCommands;
        private readonly IBookingQueryHandler _bookingQueries;

        public AdminController(
            IMeetingCommandHandler meetingCommands,
            IMeetingQueryHandler meetingQueries,
            IBookingCommandHandler bookingCommands,
            IBookingQueryHandler bookingQueries)
        {
            _meetingCommands = meetingCommands;
            _meetingQueries = meetingQueries;
            
            _bookingCommands = bookingCommands;
            _bookingQueries = bookingQueries;
        }

        [HttpGet("cancel-meeting")]
        public async Task<IActionResult> CancelMeeting(long meetingId)
        {
            await _meetingCommands.RemoveCommand(meetingId);
            return Ok();
        }

        [HttpGet("get-all-pending-bookings")]
        public async Task<IActionResult> GetAllPendingBooking()
            => await Task.Run(() => Ok(_bookingQueries.GetAllPendingBookingsQuery()));

        [HttpGet("approve-booking")]
        public async Task<IActionResult> ApproveBooking(long id)
        {
            await _bookingCommands.UpdateCommand(id, BookingStatus.Approved);
            return Ok();
        }

        [HttpGet("decline-booking")]
        public async Task<IActionResult> DeclineBooking(long id)
        {
            await _bookingCommands.UpdateCommand(id, BookingStatus.Declined);
            return Ok();
        }
    }
}

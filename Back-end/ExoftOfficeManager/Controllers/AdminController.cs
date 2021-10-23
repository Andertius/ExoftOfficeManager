using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IWorkPlaceService _workPlaceService;
        private readonly IMeetingService _meetingService;
        private readonly IBookingService _bookingService;

        public AdminController(
            IWorkPlaceService workPlace,
            IMeetingService meeting,
            IBookingService booking)
        {
            _workPlaceService = workPlace;
            _meetingService = meeting;
            _bookingService = booking;
        }

        [HttpGet("cancel-meeting")]
        public async Task<IActionResult> CancelMeeting(long meetingId)
        {
            await _meetingService.Remove(meetingId);
            return Ok();
        }

        [HttpGet("get-all-pending-bookings")]
        public async Task<IActionResult> GetAllPendingBooking()
            => await Task.Run(() => Ok(_bookingService.GetAllPendingBookings()));

        [HttpGet("approve-booking")]
        public async Task<IActionResult> ApproveBooking(long id)
        {
            await _bookingService.Update(id, Domain.BookingStatus.Approved);
            return Ok();
        }

        [HttpGet("decline-booking")]
        public async Task<IActionResult> DeclineBooking(long id)
        {
            await _bookingService.Update(id, Domain.BookingStatus.Declined);
            return Ok();
        }
    }
}

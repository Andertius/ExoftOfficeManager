using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Interfaces;
using ExoftOfficeManager.Domain;
using ExoftOfficeManager.Domain.Entities;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("get-all-users-bookings")]
        public async Task<IActionResult> GetAllUsersBookings(long devId)
            => await Task.Run(() => Ok(_bookingService.GetAllUserBooked(devId)));

        [HttpGet("cancel-booking")]
        public async Task<IActionResult> RemoveBooking(long placeId, DateTime date, long devId)
        {
            var booking = await _bookingService.Find(placeId, date, devId);
            await _bookingService.Remove(booking.Id);
            return Ok();
        }

        [HttpGet("find-booking-by-id")]
        public async Task<IActionResult> FindBooking(long bookingId)
            => Ok(await _bookingService.Find(bookingId));

        [HttpGet("find-booking")]
        public async Task<IActionResult> FindBooking(long placeId, DateTime date, long userId)
            => Ok(await _bookingService.Find(placeId, date, userId));
    }
}

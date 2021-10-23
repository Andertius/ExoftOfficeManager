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

        public WorkPlaceController(IWorkPlaceService work)
        {
            _placeService = work;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_placeService.GetAll()));

        [HttpGet("get-all-booked")]
        public async Task<IActionResult> GetBooked(DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllBooked(date.Date)));

        [HttpGet("get-all-available")]
        public async Task<IActionResult> GetAllAvailable(DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllAvailable(date.Date)));

        [HttpGet("find-workplace")]
        public async Task<IActionResult> FindWorkPlace(long placeId)
            => Ok(await _placeService.Find(placeId));

        [HttpGet("book")]
        public async Task<IActionResult> Book(long placeId, long devId, BookingType bookingType, DateTime date, int days)
        {
            if (days > 1)
            {
                await _placeService.Book(placeId, devId, bookingType, date, days);
            }
            else
            {
                await _placeService.Book(placeId, devId, bookingType, date, days);
            }

            return Ok();
        }
    }
}

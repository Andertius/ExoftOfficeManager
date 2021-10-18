﻿using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;
using ExoftOfficeManager.DataAccess;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkPlaceController : ControllerBase
    {
        private readonly IWorkPlaceService _placeService;
        private readonly IUserService _userService;

        private readonly AppDbContext _context;

        public WorkPlaceController(IWorkPlaceService work, AppDbContext context)
        {
            _placeService = work;
            _context = context;
            //_userService = developerService;
        }

        [HttpGet("AAA")]
        public async Task<IActionResult> Afkjsdfmz()
        {
            var result = await _context.Users.ToListAsync();
            return Ok(result);
        }

        [HttpPost("ASJAKNC")]
        public async Task<IActionResult> Akafjefmefllllllllll()
        {
            var user = new User { FullName = "John Doe", Role = UserRole.Developer };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
            => await Task.Run(() => Ok(_placeService.GetAll()));

        [HttpGet("get-all-booked")]
        public async Task<IActionResult> GetBooked([FromQuery] DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllBooked(date.Date)));

        [HttpGet("get-all-available")]
        public async Task<IActionResult> GetAllAvailable([FromQuery] DateTime date)
            => await Task.Run(() => Ok(_placeService.GetAllAvailable(date.Date)));

        [HttpGet("find-workplace")]
        public async Task<IActionResult> FindWorkPlace([FromQuery] long placeId)
            => await Task.Run(() => Ok(_placeService.Find(placeId)));

        [HttpGet("book")]
        public async Task<IActionResult> Book(
            [FromQuery] long placeId,
            [FromQuery] long devId,
            [FromQuery] WorkPlaceStatus status,
            [FromQuery] DateTime date,
            [FromQuery] int days)
        {
            if (days > 1)
            {

                // TODO figure out what this is supposed to be
                // ???
            }
            else
            {
                await _placeService.Book(placeId, devId, status, date, days);
                //_userService.AddRequest(new Request { PlaceId = placeId, DeveloperId = devId, Status = status, Date = date, Days = days });

            }

            return Ok();
        }

        [HttpGet("get-all-users-bookings")]
        public async Task<IActionResult> GetAllUsersBookings([FromQuery] long devId)
            => await Task.Run(() => Ok(_placeService.GetAllUserBooked(devId)));

        [HttpGet("cancel-reservation")]
        public async Task<IActionResult> CancelReservation(
            [FromQuery] long placeId,
            [FromQuery] DateTime date,
            [FromQuery] long devId)
        {
            await _placeService.MakeAvailable(placeId, date, devId);
            return Ok();
        }

        [HttpGet("find-developer")]
        public async Task<IActionResult> FindDeveloper(long id)
        {
            return await Task.Run(() => Ok(_userService.Find(id)));
        }
    }
}
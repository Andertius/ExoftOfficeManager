using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.AddBooking;
using ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceById;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetAvailableWorkPlaces;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetBookedWorkPlaces;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetWorkPlaces;
using ExoftOfficeManager.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace ExoftOfficeManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkPlaceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkPlaceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("work-places/get-all")]
        public async Task<IActionResult> GetAll()
        {
            var places = await _mediator.Send(new GetWorkPlacesQuery());
            return Ok(places);
        }

        [HttpGet("work-places/get-all-booked")]
        public async Task<IActionResult> GetBooked(DateTime date)
        {
            var places = await _mediator.Send(new GetBookedWorkPlacesQuery(date.Date));
            return Ok(places);
        }

        [HttpGet("work-places/get-all-available")]
        public async Task<IActionResult> GetAllAvailable(DateTime date)
        {
            var places = await _mediator.Send(new GetAvailableWorkPlacesQuery(date.Date));
            return Ok(places);
        }

        [HttpGet("work-places/{placeId}/find-workplace")]
        public async Task<IActionResult> FindWorkPlace(Guid placeId)
        {
            var workPlace = await _mediator.Send(new FindWorkPlaceByIdQuery(placeId));
            return Ok(workPlace);
        }

        [HttpGet("work-places/book")]
        public async Task<IActionResult> Book(Guid placeId, Guid devId, BookingType bookingType, DateTime date, int days)
        {
            await _mediator.Send(new AddBookingCommand(placeId, devId, bookingType, date, days));
            return Ok();
        }
    }
}

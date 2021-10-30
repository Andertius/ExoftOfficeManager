using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.AddBooking;
using ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceById;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetAvailableWorkPlaces;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetBookedWorkPlaces;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetWorkPlaces;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Requests;

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

        [HttpGet("work-places")]
        public async Task<IActionResult> GetAll()
        {
            var places = await _mediator.Send(new GetWorkPlacesQuery());
            return Ok(places);
        }

        [HttpGet("workplaces/booked-work-places")]
        public async Task<IActionResult> GetBooked(DateTime date)
        {
            var places = await _mediator.Send(new GetBookedWorkPlacesQuery(date.Date));
            return Ok(places);
        }

        [HttpGet("workplaces/available-workplaces")]
        public async Task<IActionResult> GetAllAvailable(DateTime date)
        {
            var places = await _mediator.Send(new GetAvailableWorkPlacesQuery(date.Date));
            return Ok(places);
        }

        [HttpGet("workplaces/{placeId}/workplace")]
        public async Task<IActionResult> FindWorkPlace([FromRoute] Guid placeId)
        {
            var workPlace = await _mediator.Send(new FindWorkPlaceByIdQuery(placeId));
            return Ok(workPlace);
        }

        [HttpPost("workplaces/{placeId}/book")]
        public async Task<IActionResult> Book(
            [FromRoute] Guid placeId,
            [FromBody] BookWorkPlaceRequest request)
        {
            await _mediator.Send(new AddBookingCommand(
                placeId,
                request.UserId,
                request.BookingType,
                request.BookingDate,
                request.Days));

            return NoContent();
        }
    }
}

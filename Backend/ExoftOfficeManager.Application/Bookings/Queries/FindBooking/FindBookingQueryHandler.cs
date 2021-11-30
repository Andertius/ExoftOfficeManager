using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.FindBooking
{
    public class FindBookingQueryHandler : IRequestHandler<FindBookingQuery, BookingsQueryResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IWorkPlaceRepository _placeRepository;

        public FindBookingQueryHandler(
            IBookingRepository bookingRepo,
            IWorkPlaceRepository placeRepo)
        {
            _bookingRepository = bookingRepo;
            _placeRepository = placeRepo;
        }

        public async Task<BookingsQueryResponse> Handle(FindBookingQuery request, CancellationToken cancellationToken)
        {
            if (request.BookingId == Guid.Empty)
            {
                var placeResponse = await _placeRepository.FindWorkPlaceById(request.PlaceId);
                var booking = placeResponse.Bookings.FirstOrDefault(x => x.Date == request.Date && x.User.Id == request.UserId);
                request.BookingId = booking.Id;
            }

            var getBookings = await _bookingRepository.FindById(request.BookingId);
            return new BookingsQueryResponse(BookingMapper.MapIntoDto(getBookings));
        }
    }
}

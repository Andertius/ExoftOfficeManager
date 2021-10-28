using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.FindBooking
{
    public class FindBookingQueryHandler : IRequestHandler<FindBookingQuery, BookingsQueryResponse>
    {
        private readonly IBookingRepository _repository;
        private readonly IWorkPlaceRepository _placeRepository;

        public FindBookingQueryHandler(IBookingRepository repo, IWorkPlaceRepository placeRepo)
        {
            _repository = repo;
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

            var getPendingBookingsDtos = await _repository.FindById(request.BookingId);
            return new BookingsQueryResponse(getPendingBookingsDtos);
        }
    }
}

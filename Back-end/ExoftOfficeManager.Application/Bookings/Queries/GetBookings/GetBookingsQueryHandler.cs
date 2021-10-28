using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetBookings
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, BookingsQueryResponse[]>
    {
        private readonly IBookingRepository _repository;

        public GetBookingsQueryHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<BookingsQueryResponse[]> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var getBookingsQueryDtos = await _repository.GetAllBookings(request.BookingDate);
            return getBookingsQueryDtos.Select(b => new BookingsQueryResponse(b)).ToArray();
        }
    }
}

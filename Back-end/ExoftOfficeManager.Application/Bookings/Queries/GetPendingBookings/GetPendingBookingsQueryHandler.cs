using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetPendingBookings
{
    class GetPendingBookingsQueryHandler : IRequestHandler<GetPendingBookingsQuery, BookingsQueryResponse[]>
    {
        private readonly IBookingRepository _repository;

        public GetPendingBookingsQueryHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<BookingsQueryResponse[]> Handle(GetPendingBookingsQuery request, CancellationToken cancellationToken)
        {
            var getPendingBookingsDtos = await _repository.GetAllPendingBookings();
            return getPendingBookingsDtos.Select(b => new BookingsQueryResponse(b)).ToArray();
        }
    }
}

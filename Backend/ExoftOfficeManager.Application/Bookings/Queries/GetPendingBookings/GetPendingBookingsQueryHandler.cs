using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetPendingBookings
{
    public class GetPendingBookingsQueryHandler : IRequestHandler<GetPendingBookingsQuery, BookingsQueryResponse[]>
    {
        private readonly IBookingRepository _repository;

        public GetPendingBookingsQueryHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<BookingsQueryResponse[]> Handle(GetPendingBookingsQuery request, CancellationToken cancellationToken)
        {
            var pendingBookings = await _repository.GetAllPendingBookings();
            return pendingBookings
                .Select(b => new BookingsQueryResponse(BookingMapper.MapIntoDto(b)))
                .ToArray();
        }
    }
}

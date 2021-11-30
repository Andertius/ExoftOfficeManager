using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetBookingsByUser
{
    public class GetBookingsByUserQueryHandler : IRequestHandler<GetBookingsByUserQuery, BookingsQueryResponse[]>
    {
        private readonly IBookingRepository _repository;

        public GetBookingsByUserQueryHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<BookingsQueryResponse[]> Handle(GetBookingsByUserQuery request, CancellationToken cancellationToken)
        {
            var pendingBookings = await _repository.GetBookingsByUser(request.UserId);
            var a = pendingBookings
                .Select(b => new BookingsQueryResponse(BookingMapper.MapIntoDto(b)))
                .ToArray();
            return a;
        }
    }
}

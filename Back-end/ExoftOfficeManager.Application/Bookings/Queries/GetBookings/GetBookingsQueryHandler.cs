using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services;

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

        public async Task<BookingsQueryResponse[]> Handle(GetBookingsQuery query, CancellationToken cancellationToken)
        {
            var getBookingsQueryDtos = await _repository.GetAllBookings(query.Date);
            return getBookingsQueryDtos.Select(b => new BookingsQueryResponse(b)).ToArray();
        }
    }
}

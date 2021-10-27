using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.FindById
{
    public class FindByIdQueryHandler : IRequestHandler<FindByIdQuery, BookingsQueryResponse>
    {
        private readonly IBookingRepository _repository;

        public FindByIdQueryHandler(IBookingRepository repo)
        {
            _repository = repo;
        }

        public async Task<BookingsQueryResponse> Handle(FindByIdQuery request, CancellationToken cancellationToken)
        {
            var getPendingBookingsDtos = await _repository.FindById(request.Id);
            return new BookingsQueryResponse(getPendingBookingsDtos);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var getPendingBookingsDtos = await _repository.GetBookingsByUser(request.UserId);
            return getPendingBookingsDtos.Select(b => new BookingsQueryResponse(b)).ToArray();
        }
    }
}

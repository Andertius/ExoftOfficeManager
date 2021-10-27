using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetBookingsByUser
{
    public class GetBookingsByUserQuery : IRequest<BookingsQueryResponse[]>
    {
        public GetBookingsByUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}

using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<BookingsQueryResponse[]>
    {
        public GetBookingsQuery(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; private set; }
    }
}

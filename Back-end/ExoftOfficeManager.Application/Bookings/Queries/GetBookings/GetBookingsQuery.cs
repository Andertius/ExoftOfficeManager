using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.GetBookings
{
    public class GetBookingsQuery : IRequest<BookingsQueryResponse[]>
    {
        public GetBookingsQuery(DateTime meetingDate)
        {
            BookingDate = meetingDate;
        }

        public DateTime BookingDate { get; private set; }
    }
}

using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Queries.FindBooking
{
    public class FindBookingQuery : IRequest<BookingsQueryResponse>
    {
        public FindBookingQuery(Guid bookingId)
        {
            BookingId = bookingId;
        }

        public FindBookingQuery(
            Guid placeId,
            DateTime date,
            Guid userId)
        {
            PlaceId = placeId;
            Date = date;
            UserId = userId;
        }

        public Guid BookingId { get; set; }

        public Guid PlaceId { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}

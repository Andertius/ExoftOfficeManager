using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBooking
{
    public class RemoveBookingCommand : IRequest
    {
        public RemoveBookingCommand(Guid id)
        {
            BookingId = id;
        }

        public RemoveBookingCommand(Guid placeId, DateTime date, Guid userId)
        {
            PlaceId = placeId;
            Date = date;
            UserId = userId;
        }

        public Guid? BookingId { get; set; }

        public Guid PlaceId { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
    }
}

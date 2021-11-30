using System;

using ExoftOfficeManager.Domain.Enums;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.AddBooking
{
    public class AddBookingCommand : IRequest
    {
        public AddBookingCommand(
            Guid placeId,
            Guid developerId,
            BookingType type,
            DateTime date,
            int? days)
        {
            PlaceId = placeId;
            UserId = developerId;
            BookingType = type;
            BookingDate = date;
            DayNumber = days;
        }

        public Guid PlaceId { get; set; }

        public Guid UserId { get; set; }

        public BookingType BookingType { get; set; }

        public DateTime BookingDate { get; set; }

        public int? DayNumber { get; set; }
    }
}

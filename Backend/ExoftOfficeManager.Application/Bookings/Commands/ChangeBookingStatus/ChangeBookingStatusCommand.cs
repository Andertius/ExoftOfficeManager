using System;

using ExoftOfficeManager.Domain.Enums;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.ChangeBookingStatus
{
    public class ChangeBookingStatusCommand : IRequest
    {
        public ChangeBookingStatusCommand(Guid bookingId, BookingStatus status)
        {
            BookingId = bookingId;
            BookingStatus = status;
        }

        public Guid BookingId { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }
}

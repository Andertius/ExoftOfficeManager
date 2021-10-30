using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingById
{
    public class RemoveBookingByIdCommand : IRequest
    {
        public RemoveBookingByIdCommand(Guid id)
        {
            BookingId = id;
        }

        public Guid BookingId { get; set; }
    }
}

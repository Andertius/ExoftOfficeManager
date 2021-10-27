using System;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.RemoveBooking
{
    public class RemoveBookingCommand : IRequest
    {
        public RemoveBookingCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

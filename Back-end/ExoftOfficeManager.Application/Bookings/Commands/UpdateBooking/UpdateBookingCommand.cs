
using ExoftOfficeManager.Domain.Dtos;

using MediatR;

namespace ExoftOfficeManager.Application.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommand : IRequest
    {
        public UpdateBookingCommand(BookingDto booking)
        {
            Booking = booking;
        }

        public BookingDto Booking { get; set; }
    }
}

using ExoftOfficeManager.Application.Bookings.Commands.AddBooking;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators.Commands.Bookings
{
    public sealed class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingCommandValidator()
        {
            RuleFor(x => x.BookingType)
                .IsInEnum();

            RuleFor(x => x.DayNumber)
                .GreaterThan(0);
        }
    }
}

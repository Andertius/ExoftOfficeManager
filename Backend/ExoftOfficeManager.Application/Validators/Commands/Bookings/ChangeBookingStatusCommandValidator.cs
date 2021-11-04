using ExoftOfficeManager.Application.Bookings.Commands.ChangeBookingStatus;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators.Commands.Bookings
{
    public sealed class ChangeBookingStatusCommandValidator : AbstractValidator<ChangeBookingStatusCommand>
    {
        public ChangeBookingStatusCommandValidator()
        {
            RuleFor(x => x.BookingStatus)
                .IsInEnum();
        }
    }
}

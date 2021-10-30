using ExoftOfficeManager.Application.Bookings.Commands.UpdateBooking;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators.Commands.Bookings
{
    public sealed class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.Booking)
                .ChildRules(x =>
                    x.RuleFor(x => x.DayNumber)
                        .GreaterThan(0));

            RuleFor(x => x.Booking)
                .ChildRules(x =>
                    x.RuleFor(x => x.Type)
                        .IsInEnum());

            RuleFor(x => x.Booking)
                .ChildRules(x =>
                    x.RuleFor(x => x.Status)
                        .IsInEnum());
        }
    }
}

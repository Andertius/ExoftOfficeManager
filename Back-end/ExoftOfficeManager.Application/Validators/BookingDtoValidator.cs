using ExoftOfficeManager.Domain.Dtos;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty();
        }
    }
}

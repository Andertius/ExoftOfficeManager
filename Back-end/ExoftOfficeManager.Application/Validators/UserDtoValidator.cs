using ExoftOfficeManager.Domain.Dtos;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .Must(x => x.Split(' ').Length > 1);
        }
    }
}

using ExoftOfficeManager.Application.Users.Commands.AddUser;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators.Commands.Users
{
    public sealed class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.User)
                .ChildRules(x =>
                    x.RuleFor(x => x.FullName)
                        .NotEmpty());

            RuleFor(x => x.User)
                .ChildRules(x =>
                    x.RuleFor(x => x.Role)
                        .IsInEnum());
        }
    }
}

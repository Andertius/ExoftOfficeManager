using ExoftOfficeManager.Application.WorkPlaces.Commands.UpdateWorkPlace;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators.Commands.WorkPlaces
{
    public sealed class UpdateWorkPlaceCommandValidator : AbstractValidator<UpdateWorkPlaceCommand>
    {
        public UpdateWorkPlaceCommandValidator()
        {
            RuleFor(x => x.WorkPlace)
                .ChildRules(x =>
                    x.RuleFor(x => x.PlaceNumber)
                        .GreaterThan(0)
                        .LessThanOrEqualTo(5));

            RuleFor(x => x.WorkPlace)
                .ChildRules(x =>
                    x.RuleFor(x => x.FloorNumber)
                        .GreaterThan(0)
                        .LessThanOrEqualTo(2));
        }
    }
}

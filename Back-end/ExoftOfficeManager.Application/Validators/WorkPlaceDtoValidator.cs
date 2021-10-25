using ExoftOfficeManager.Domain.Dtos;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators
{
    public class WorkPlaceDtoValidator : AbstractValidator<WorkPlaceDto>
    {
        public WorkPlaceDtoValidator()
        {
        }
    }
}

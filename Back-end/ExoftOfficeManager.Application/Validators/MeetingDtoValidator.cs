using System;

using ExoftOfficeManager.Domain.Dtos;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators
{
    public class MeetingDtoValidator : AbstractValidator<MeetingDto>
    {
        public MeetingDtoValidator()
        {
            RuleFor(x => x.DateAndTime)
                .NotEmpty()
                .Must(x => x.TimeOfDay >= new TimeSpan(10, 0, 0) && x.TimeOfDay < new TimeSpan(18, 0, 0));

            RuleFor(x => x.Duration)
                .NotEmpty()
                .Must(x => x.TotalMinutes % 30 == 0);

            RuleFor(x => x.MeetingPurpose)
                .NotEmpty();
        }
    }
}

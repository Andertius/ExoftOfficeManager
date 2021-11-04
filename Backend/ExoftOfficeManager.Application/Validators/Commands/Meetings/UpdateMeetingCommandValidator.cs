using System;

using ExoftOfficeManager.Application.Meetings.Commands.UpdateMeeting;

using FluentValidation;

namespace ExoftOfficeManager.Application.Validators.Commands.Meetings
{
    public sealed class UpdateMeetingCommandValidator : AbstractValidator<UpdateMeetingCommand>
    {
        public UpdateMeetingCommandValidator()
        {
            RuleFor(x => x.Meeting)
                .ChildRules(x =>
                    x.RuleFor(x => x.MeetingPurpose)
                        .NotEmpty());

            RuleFor(x => x.Meeting)
                .ChildRules(x =>
                    x.RuleFor(x => x.DateAndTime)
                        .NotEmpty()
                        .Must(x => x.TimeOfDay >= new TimeSpan(10, 0, 0) && x.TimeOfDay < new TimeSpan(18, 0, 0)));

            RuleFor(x => x.Meeting)
                .ChildRules(x =>
                    x.RuleFor(x => x.Duration)
                        .NotEmpty()
                        .Must(x => x.TotalMinutes % 30 == 0));

            RuleFor(x => x.Meeting)
                .ChildRules(x =>
                    x.RuleFor(x => x.Owner)
                        .NotEmpty());
        }
    }
}

using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchServiceSessions.Commands.ScheduleSession;

public class ScheduleSessionCommandValidator : AbstractValidator<ScheduleSessionCommand>
{
      public ScheduleSessionCommandValidator()
      {
            RuleFor(x => x.ChurchServiceId).NotEmpty();
            RuleFor(x => x.StartTime).MustBeUtc();
            RuleFor(x => x.EndTime).MustBeUtc().GreaterThan(x => x.StartTime);
      }
}

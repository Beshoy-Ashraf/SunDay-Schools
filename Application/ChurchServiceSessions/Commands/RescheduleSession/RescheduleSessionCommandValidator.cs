using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchServiceSessions.Commands.RescheduleSession;

public class RescheduleSessionCommandValidator : AbstractValidator<RescheduleSessionCommand>
{
      public RescheduleSessionCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.StartTime).MustBeUtc();
            RuleFor(x => x.EndTime).MustBeUtc().GreaterThan(x => x.StartTime);
      }
}

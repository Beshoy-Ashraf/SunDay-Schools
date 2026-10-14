using FluentValidation;

namespace Application.ActivityTypes.Commands.SetActivityTypePoints;

public class SetActivityTypePointsCommandValidator : AbstractValidator<SetActivityTypePointsCommand>
{
      public SetActivityTypePointsCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Points).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MaxPerMonth).GreaterThanOrEqualTo(1).When(x => x.MaxPerMonth.HasValue);
      }
}

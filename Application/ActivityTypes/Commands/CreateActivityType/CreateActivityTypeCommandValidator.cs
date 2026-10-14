using FluentValidation;

namespace Application.ActivityTypes.Commands.CreateActivityType;

public class CreateActivityTypeCommandValidator : AbstractValidator<CreateActivityTypeCommand>
{
      public CreateActivityTypeCommandValidator()
      {
            RuleFor(x => x.ChurchServiceId).NotEmpty();
            RuleFor(x => x.Code).IsInEnum();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.AppliesTo).IsInEnum();
            RuleFor(x => x.Points).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MaxPerMonth).GreaterThanOrEqualTo(1).When(x => x.MaxPerMonth.HasValue);
      }
}

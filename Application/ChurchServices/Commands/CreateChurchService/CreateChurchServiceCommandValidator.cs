using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchServices.Commands.CreateChurchService;

public class CreateChurchServiceCommandValidator : AbstractValidator<CreateChurchServiceCommand>
{
      public CreateChurchServiceCommandValidator()
      {
            RuleFor(x => x.ChurchId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.StartTime).MustBeUtc();
            RuleFor(x => x.EndTime).MustBeUtc().GreaterThan(x => x.StartTime);
      }
}

using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchServices.Commands.UpdateChurchService;

public class UpdateChurchServiceCommandValidator : AbstractValidator<UpdateChurchServiceCommand>
{
      public UpdateChurchServiceCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(1000);
            RuleFor(x => x.StartTime).MustBeUtc();
            RuleFor(x => x.EndTime).MustBeUtc().GreaterThan(x => x.StartTime);
      }
}

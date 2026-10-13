using FluentValidation;

namespace Application.Eparchies.Commands.CreateEparchy;

public class CreateEparchyCommandValidator : AbstractValidator<CreateEparchyCommand>
{
      public CreateEparchyCommandValidator()
      {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
      }
}

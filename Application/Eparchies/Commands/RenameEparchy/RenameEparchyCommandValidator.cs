using FluentValidation;

namespace Application.Eparchies.Commands.RenameEparchy;

public class RenameEparchyCommandValidator : AbstractValidator<RenameEparchyCommand>
{
      public RenameEparchyCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
      }
}

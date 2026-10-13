using FluentValidation;

namespace Application.ChurchMembers.Commands.ChangeMemberRole;

public class ChangeMemberRoleCommandValidator : AbstractValidator<ChangeMemberRoleCommand>
{
      public ChangeMemberRoleCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
      }
}

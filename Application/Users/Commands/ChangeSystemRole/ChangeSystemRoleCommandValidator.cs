using FluentValidation;

namespace Application.Users.Commands.ChangeSystemRole;

public class ChangeSystemRoleCommandValidator : AbstractValidator<ChangeSystemRoleCommand>
{
      public ChangeSystemRoleCommandValidator()
      {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
      }
}

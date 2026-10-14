using FluentValidation;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
      public UpdateUserCommandValidator()
      {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(100);
      }
}

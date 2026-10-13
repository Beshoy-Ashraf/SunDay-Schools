using FluentValidation;

namespace Application.Churches.Commands.UpdateChurch;

public class UpdateChurchCommandValidator : AbstractValidator<UpdateChurchCommand>
{
      public UpdateChurchCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(500);
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
      }
}

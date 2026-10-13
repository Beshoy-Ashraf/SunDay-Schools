using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchMembers.Commands.LeaveChurch;

public class LeaveChurchCommandValidator : AbstractValidator<LeaveChurchCommand>
{
      public LeaveChurchCommandValidator()
      {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.DateLeft).MustBeUtc();
      }
}

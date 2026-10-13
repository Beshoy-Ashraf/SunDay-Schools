using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchMembers.Commands.AddChurchMember;

public class AddChurchMemberCommandValidator : AbstractValidator<AddChurchMemberCommand>
{
      public AddChurchMemberCommandValidator()
      {
            RuleFor(x => x.ChurchId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
            RuleFor(x => x.DateJoined).MustBeUtc();
            RuleFor(x => x.Notes).MaximumLength(1000);
      }
}

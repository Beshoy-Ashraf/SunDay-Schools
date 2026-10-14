using FluentValidation;

namespace Application.ChurchServiceMembers.Commands.AssignServiceMember;

public class AssignServiceMemberCommandValidator : AbstractValidator<AssignServiceMemberCommand>
{
      public AssignServiceMemberCommandValidator()
      {
            RuleFor(x => x.ChurchServiceId).NotEmpty();
            RuleFor(x => x.ChurchMemberId).NotEmpty();
            RuleFor(x => x.Role).IsInEnum();
      }
}

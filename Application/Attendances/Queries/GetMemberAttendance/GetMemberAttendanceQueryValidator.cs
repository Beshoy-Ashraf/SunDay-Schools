using Application.Common.Validation;
using FluentValidation;

namespace Application.Attendances.Queries.GetMemberAttendance;

public class GetMemberAttendanceQueryValidator : AbstractValidator<GetMemberAttendanceQuery>
{
      public GetMemberAttendanceQueryValidator()
      {
            RuleFor(x => x.From).MustBeUtc();
            RuleFor(x => x.To).MustBeUtc();
            RuleFor(x => x.To).GreaterThan(x => x.From).When(x => x.From is not null && x.To is not null);
      }
}

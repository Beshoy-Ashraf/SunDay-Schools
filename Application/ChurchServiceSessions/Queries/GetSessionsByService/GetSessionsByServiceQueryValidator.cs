using Application.Common.Validation;
using FluentValidation;

namespace Application.ChurchServiceSessions.Queries.GetSessionsByService;

public class GetSessionsByServiceQueryValidator : AbstractValidator<GetSessionsByServiceQuery>
{
      public GetSessionsByServiceQueryValidator()
      {
            RuleFor(x => x.From).MustBeUtc();
            RuleFor(x => x.To).MustBeUtc();
            RuleFor(x => x.To).GreaterThan(x => x.From).When(x => x.From is not null && x.To is not null);
      }
}

using Application.Common.Validation;
using FluentValidation;

namespace Application.Attendances.Commands.RecordAttendance;

public class RecordAttendanceCommandValidator : AbstractValidator<RecordAttendanceCommand>
{
      public RecordAttendanceCommandValidator()
      {
            RuleFor(x => x.SessionId).NotEmpty();
            RuleFor(x => x.ChurchMemberId).NotEmpty();
            RuleFor(x => x.AttendedAt).MustBeUtc();

            RuleForEach(x => x.ActivityAnswers)
                  .ChildRules(answer =>
                  {
                        answer.RuleFor(a => a.ActivityTypeId).NotEmpty();
                  })
                  .When(x => x.ActivityAnswers is not null);

            // No duplicate activity type IDs in one request.
            RuleFor(x => x.ActivityAnswers)
                  .Must(answers => answers!.Select(a => a.ActivityTypeId).Distinct().Count() == answers!.Count)
                  .WithMessage("Activity answers must not contain duplicate activity type IDs.")
                  .When(x => x.ActivityAnswers is { Count: > 1 });
      }
}

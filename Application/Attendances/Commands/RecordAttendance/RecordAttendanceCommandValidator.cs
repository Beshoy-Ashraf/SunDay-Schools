using Application.Common.Validation;
using FluentValidation;

namespace Application.Attendances.Commands.RecordAttendance;

public class RecordAttendanceCommandValidator : AbstractValidator<RecordAttendanceCommand>
{
      public RecordAttendanceCommandValidator()
      {
            RuleFor(x => x.ChurchServiceId).NotEmpty();
            RuleFor(x => x.ChurchMemberId).NotEmpty();
            RuleFor(x => x.AttendedAt).MustBeUtc();
      }
}

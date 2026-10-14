using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Commands.RecordAttendance;

/// <summary>Yes/No answer for one servant activity type during a session.</summary>
public record ActivityAnswer(Guid ActivityTypeId, bool Answered);

/// <summary>
/// Records a member's attendance for a session.
/// When the member is a servant, the caller may include activity answers (yes/no per activity type).
/// Each "yes" earns the servant that activity type's configured points.
/// </summary>
public record RecordAttendanceCommand(
    Guid SessionId,
    Guid ChurchMemberId,
    DateTime? AttendedAt,
    IReadOnlyList<ActivityAnswer>? ActivityAnswers = null) : IRequest<AttendanceDto>;

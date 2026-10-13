using Domain.Entities;

namespace Application.Attendances.Dtos;

public record AttendanceDto(
    Guid Id,
    Guid ChurchServiceSessionId,
    Guid ChurchMemberId,
    string? MemberDisplayName,
    Guid RecordedById,
    DateTime AttendedAt)
{
      public static AttendanceDto FromEntity(ChurchServiceAttendance attendance) => new(
            attendance.Id,
            attendance.ChurchServiceSessionId,
            attendance.ChurchMemberId,
            attendance.ChurchMember?.User?.DisplayName,
            attendance.RecordedById,
            attendance.AttendedAt);
}

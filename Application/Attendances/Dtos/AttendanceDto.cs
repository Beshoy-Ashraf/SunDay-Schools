using Domain.Entities;

namespace Application.Attendances.Dtos;

public record AttendanceDto(
    Guid Id,
    Guid ChurchServiceId,
    string ChurchServiceName,
    Guid ChurchMemberId,
    string? MemberDisplayName,
    Guid RecordedById,
    DateTime AttendedAt)
{
      public static AttendanceDto FromEntity(ChurchServiceAttendance attendance) => new(
            attendance.Id,
            attendance.ChurchServiceId,
            attendance.ChurchService?.Name ?? string.Empty,
            attendance.ChurchMemberId,
            attendance.ChurchMember?.User?.DisplayName,
            attendance.RecordedById,
            attendance.AttendedAt);
}

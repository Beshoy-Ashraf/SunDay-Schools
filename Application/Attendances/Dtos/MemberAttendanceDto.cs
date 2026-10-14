using Domain.Entities;

namespace Application.Attendances.Dtos;

/// <summary>One entry of a member's attendance history.</summary>
public record MemberAttendanceDto(
    Guid Id,
    Guid ChurchServiceId,
    string ChurchServiceName,
    DateTime AttendedAt)
{
      public static MemberAttendanceDto FromEntity(ChurchServiceAttendance attendance) => new(
            attendance.Id,
            attendance.ChurchServiceId,
            attendance.ChurchService.Name,
            attendance.AttendedAt);
}

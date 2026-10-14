using Domain.Entities;

namespace Application.Attendances.Dtos;

/// <summary>One entry of a member's full attendance history.</summary>
public record MemberAttendanceDto(
    Guid Id,
    Guid ChurchServiceSessionId,
    Guid ChurchServiceId,
    string ChurchServiceName,
    DateTime SessionStartTime,
    DateTime AttendedAt,
    int TotalPoints,
    IReadOnlyList<ServantActivityRecordDto> ActivityRecords)
{
      public static MemberAttendanceDto FromEntity(ChurchServiceAttendance attendance) => new(
            attendance.Id,
            attendance.ChurchServiceSessionId,
            attendance.ChurchServiceSession.ChurchServiceId,
            attendance.ChurchServiceSession.ChurchService.Name,
            attendance.ChurchServiceSession.StartTime,
            attendance.AttendedAt,
            attendance.ActivityRecords.Sum(r => r.PointsEarned),
            attendance.ActivityRecords.Select(ServantActivityRecordDto.FromEntity).ToList());
}

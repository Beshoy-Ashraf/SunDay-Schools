using Domain.Entities;

namespace Application.Attendances.Dtos;

public record AttendanceDto(
    Guid Id,
    Guid ChurchServiceSessionId,
    Guid ChurchMemberId,
    string? MemberDisplayName,
    Guid RecordedById,
    DateTime AttendedAt,
    int TotalPoints,
    IReadOnlyList<ServantActivityRecordDto> ActivityRecords)
{
      public static AttendanceDto FromEntity(ChurchServiceAttendance attendance) => new(
            attendance.Id,
            attendance.ChurchServiceSessionId,
            attendance.ChurchMemberId,
            attendance.ChurchMember?.User?.DisplayName,
            attendance.RecordedById,
            attendance.AttendedAt,
            attendance.ActivityRecords.Sum(r => r.PointsEarned),
            attendance.ActivityRecords.Select(ServantActivityRecordDto.FromEntity).ToList());
}

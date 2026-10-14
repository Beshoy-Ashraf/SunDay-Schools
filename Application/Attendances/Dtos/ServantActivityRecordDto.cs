using Domain.Entities;

namespace Application.Attendances.Dtos;

public record ServantActivityRecordDto(
    Guid ActivityTypeId,
    string ActivityTypeName,
    bool Answered,
    int PointsEarned)
{
      public static ServantActivityRecordDto FromEntity(ServantActivityRecord record) => new(
            record.ActivityTypeId,
            record.ActivityType.Name,
            record.Answered,
            record.PointsEarned);
}

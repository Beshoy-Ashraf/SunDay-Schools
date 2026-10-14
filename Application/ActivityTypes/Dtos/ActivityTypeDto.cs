using Domain.Entities;
using Domain.Enums;

namespace Application.ActivityTypes.Dtos;

public record ActivityTypeDto(
    Guid Id,
    Guid ChurchServiceId,
    ActivityTypeCode Code,
    string Name,
    ActivityAppliesTo AppliesTo,
    int Points,
    int? MaxPerMonth,
    bool IsActive)
{
      public static ActivityTypeDto FromEntity(ActivityType a) => new(
            a.Id, a.ChurchServiceId, a.Code, a.Name, a.AppliesTo, a.Points, a.MaxPerMonth, a.IsActive);
}

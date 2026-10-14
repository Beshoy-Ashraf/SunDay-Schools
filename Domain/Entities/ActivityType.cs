using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class ActivityType : BaseEntity
{
      public Guid ChurchServiceId { get; private set; }
      public ChurchService ChurchService { get; private set; } = null!;

      public ActivityTypeCode Code { get; private set; }
      public string Name { get; private set; } = null!;
      public ActivityAppliesTo AppliesTo { get; private set; }

      public int Points { get; private set; }
      public int? MaxPerMonth { get; private set; }

      public bool IsActive { get; private set; }

      private ActivityType() { }

      public ActivityType(Guid churchServiceId, ActivityTypeCode code, string name,
                          ActivityAppliesTo appliesTo, int points, int? maxPerMonth)
      {
            if (churchServiceId == Guid.Empty) throw new ArgumentException("Service is required.", nameof(churchServiceId));

            ChurchServiceId = churchServiceId;
            Code = code;
            AppliesTo = appliesTo;
            IsActive = true;
            SetPoints(points, maxPerMonth);
            SetName(name);
      }

      public void SetPoints(int points, int? maxPerMonth)
      {
            if (points < 0) throw new BadRequestException("Points cannot be negative.");
            if (maxPerMonth.HasValue && maxPerMonth.Value < 1)
                  throw new BadRequestException("MaxPerMonth must be at least 1 when set.");

            Points = points;
            MaxPerMonth = maxPerMonth;
            MarkUpdated();
      }

      public void SetName(string name)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
            MarkUpdated();
      }

      public void Activate()
      {
            IsActive = true;
            MarkUpdated();
      }

      public void Deactivate()
      {
            IsActive = false;
            MarkUpdated();
      }
}

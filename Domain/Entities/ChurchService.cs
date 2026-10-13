using Domain.Common;

namespace Domain.Entities;

public class ChurchService : BaseEntity
{
      private readonly List<ChurchServiceSession> _sessions = [];

      public string Name { get; private set; } = null!;
      public string? Description { get; private set; }
      public DateTime StartTime { get; private set; }
      public DateTime EndTime { get; private set; }

      public Guid ChurchId { get; private set; }
      public Church Church { get; private set; } = null!;

      public IReadOnlyCollection<ChurchServiceSession> Sessions => _sessions.AsReadOnly();

      private ChurchService() { }

      public ChurchService(Guid churchId, string name, string? description,
                           DateTime startTime, DateTime endTime)
      {
            if (churchId == Guid.Empty) throw new ArgumentException("Church is required.", nameof(churchId));

            ChurchId = churchId;
            Update(name, description, startTime, endTime);
      }

      public void Update(string name, string? description, DateTime startTime, DateTime endTime)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            if (endTime <= startTime)
                  throw new ArgumentException("End time must be after start time.");

            Name = name.Trim();
            Description = description?.Trim();
            StartTime = startTime;
            EndTime = endTime;
            MarkUpdated();
      }

      public ChurchServiceSession ScheduleSession(DateTime startTime, DateTime endTime)
      {
            var session = new ChurchServiceSession(this, startTime, endTime);
            _sessions.Add(session);
            return session;
      }
}
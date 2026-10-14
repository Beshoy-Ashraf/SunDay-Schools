using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;


namespace Domain.Entities;

public class ChurchService : BaseEntity
{
      private readonly List<ChurchServiceMember> _serviceMembers = [];

      public string Name { get; private set; } = null!;
      public string? Description { get; private set; }
      public DateTime StartTime { get; private set; }
      public DateTime EndTime { get; private set; }

      public Guid ChurchId { get; private set; }
      public Church Church { get; private set; } = null!;

      public IReadOnlyCollection<ChurchServiceMember> ServiceMembers => _serviceMembers.AsReadOnly();

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

      public ChurchServiceMember AssignMember(ChurchMember member, ChurchServiceRole role)
      {
            if (!member.IsActive)
                  throw new BadRequestException("Cannot assign an inactive church member to a service.");
            if (member.ChurchId != ChurchId)
                  throw new BadRequestException("Member does not belong to this service's church.");

            var assignment = new ChurchServiceMember(this, member, role);
            _serviceMembers.Add(assignment);
            return assignment;
      }

      /// <summary>
      /// Creates an attendance record. Enforces that both parties are active members of this service's church
      /// and that the recorder is a servant role.
      /// Duplicate check (same member + same day) must be done by the caller before invoking this.
      /// </summary>
      public ChurchServiceAttendance RecordAttendance(ChurchMember member, ChurchMember recordedBy, DateTime? attendedAt = null)
      {
            ArgumentNullException.ThrowIfNull(member);
            ArgumentNullException.ThrowIfNull(recordedBy);

            if (!member.IsActive || member.ChurchId != ChurchId)
                  throw new BadRequestException("Member is not an active member of this service's church.");

            if (!recordedBy.IsActive || recordedBy.ChurchId != ChurchId)
                  throw new ForbiddenException("Recorder is not an active member of this service's church.");

            if (recordedBy.Role == ChurchMemberRole.Member)
                  throw new ForbiddenException("Only servants can record attendance.");

            return new ChurchServiceAttendance(this, member, recordedBy, attendedAt ?? DateTime.UtcNow);
      }
}

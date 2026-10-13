using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class ChurchServiceSession : BaseEntity
{
      private readonly List<ChurchServiceAttendance> _attendances = [];

      public Guid ChurchServiceId { get; private set; }
      public ChurchService ChurchService { get; private set; } = null!;

      public DateTime StartTime { get; private set; }
      public DateTime EndTime { get; private set; }

      public IReadOnlyCollection<ChurchServiceAttendance> Attendances => _attendances.AsReadOnly();

      private ChurchServiceSession() { }

      internal ChurchServiceSession(ChurchService churchService, DateTime startTime, DateTime endTime)
      {
            ChurchService = churchService;
            ChurchServiceId = churchService.Id;
            Reschedule(startTime, endTime);
      }

      public void Reschedule(DateTime startTime, DateTime endTime)
      {
            if (endTime <= startTime)
                  throw new ArgumentException("End time must be after start time.");

            StartTime = startTime;
            EndTime = endTime;
            MarkUpdated();
      }

      /// <remarks>
      /// Load the session with <see cref="ChurchService"/> and <see cref="Attendances"/> before calling this.
      /// </remarks>
      public ChurchServiceAttendance RecordAttendance(ChurchMember member, ChurchMember recordedBy, DateTime? attendedAt = null)
      {
            ArgumentNullException.ThrowIfNull(member);
            ArgumentNullException.ThrowIfNull(recordedBy);

            if (ChurchService is null)
                  throw new InvalidOperationException("Session must be loaded with its ChurchService to record attendance.");

            var churchId = ChurchService.ChurchId;

            if (!member.IsActive || member.ChurchId != churchId)
                  throw new BadRequestException("Member is not an active member of this service's church.");

            if (!recordedBy.IsActive || recordedBy.ChurchId != churchId)
                  throw new ForbiddenException("Recorder is not an active member of this service's church.");

            if (recordedBy.Role == ChurchMemberRole.Member)
                  throw new ForbiddenException("Only servants can record attendance.");

            if (_attendances.Any(a => a.ChurchMemberId == member.Id && !a.IsDeleted))
                  throw new ConflictException("Attendance is already recorded for this member in this session.");

            var attendance = new ChurchServiceAttendance(this, member, recordedBy, attendedAt ?? DateTime.UtcNow);
            _attendances.Add(attendance);
            return attendance;
      }
}

using Domain.Common;

namespace Domain.Entities;

public class ChurchServiceAttendance : BaseEntity
{
      private readonly List<ServantActivityRecord> _activityRecords = [];

      public Guid ChurchServiceSessionId { get; private set; }
      public ChurchServiceSession ChurchServiceSession { get; private set; } = null!;

      public Guid ChurchMemberId { get; private set; }
      public ChurchMember ChurchMember { get; private set; } = null!;

      public Guid RecordedById { get; private set; }
      public ChurchMember RecordedBy { get; private set; } = null!;

      public DateTime AttendedAt { get; private set; }

      public IReadOnlyCollection<ServantActivityRecord> ActivityRecords => _activityRecords.AsReadOnly();

      private ChurchServiceAttendance() { }

      // Created only through ChurchServiceSession.RecordAttendance, which enforces the rules.
      internal ChurchServiceAttendance(ChurchServiceSession session, ChurchMember member,
                                       ChurchMember recordedBy, DateTime attendedAt)
      {
            ChurchServiceSession = session;
            ChurchServiceSessionId = session.Id;
            ChurchMember = member;
            ChurchMemberId = member.Id;
            RecordedBy = recordedBy;
            RecordedById = recordedBy.Id;
            AttendedAt = attendedAt;
      }

      /// <summary>Records a yes/no answer for a servant activity type, awarding points if answered true.</summary>
      public ServantActivityRecord RecordActivity(ActivityType activityType, bool answered)
      {
            var record = new ServantActivityRecord(this, activityType, answered);
            _activityRecords.Add(record);
            return record;
      }
}

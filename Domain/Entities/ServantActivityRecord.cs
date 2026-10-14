using Domain.Common;

namespace Domain.Entities;

public class ServantActivityRecord : BaseEntity
{
      public Guid ChurchServiceAttendanceId { get; private set; }
      public ChurchServiceAttendance ChurchServiceAttendance { get; private set; } = null!;

      public Guid ActivityTypeId { get; private set; }
      public ActivityType ActivityType { get; private set; } = null!;

      /// <summary>Whether the servant performed this activity during the session.</summary>
      public bool Answered { get; private set; }

      /// <summary>Points awarded: ActivityType.Points when Answered is true, 0 otherwise.</summary>
      public int PointsEarned { get; private set; }

      private ServantActivityRecord() { }

      internal ServantActivityRecord(ChurchServiceAttendance attendance, ActivityType activityType, bool answered)
      {
            ChurchServiceAttendance = attendance;
            ChurchServiceAttendanceId = attendance.Id;
            ActivityType = activityType;
            ActivityTypeId = activityType.Id;
            Answered = answered;
            PointsEarned = answered ? activityType.Points : 0;
      }
}

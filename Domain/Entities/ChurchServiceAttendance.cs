using Domain.Common;

namespace Domain.Entities;

public class ChurchServiceAttendance : BaseEntity
{
      public Guid ChurchServiceSessionId { get; private set; }
      public ChurchServiceSession ChurchServiceSession { get; private set; } = null!;

      public Guid ChurchMemberId { get; private set; }
      public ChurchMember ChurchMember { get; private set; } = null!;

      public Guid RecordedById { get; private set; }
      public ChurchMember RecordedBy { get; private set; } = null!;

      public DateTime AttendedAt { get; private set; }

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
}

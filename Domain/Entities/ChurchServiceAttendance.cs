using Domain.Common;

namespace Domain.Entities;

public class ChurchServiceAttendance : BaseEntity
{
      public Guid ChurchServiceId { get; private set; }
      public ChurchService ChurchService { get; private set; } = null!;

      public Guid ChurchMemberId { get; private set; }
      public ChurchMember ChurchMember { get; private set; } = null!;

      public Guid RecordedById { get; private set; }
      public ChurchMember RecordedBy { get; private set; } = null!;

      public DateTime AttendedAt { get; private set; }

      private ChurchServiceAttendance() { }

      public ChurchServiceAttendance(ChurchService service, ChurchMember member,
                                     ChurchMember recordedBy, DateTime attendedAt)
      {
            ChurchService = service;
            ChurchServiceId = service.Id;
            ChurchMember = member;
            ChurchMemberId = member.Id;
            RecordedBy = recordedBy;
            RecordedById = recordedBy.Id;
            AttendedAt = attendedAt;
      }
}

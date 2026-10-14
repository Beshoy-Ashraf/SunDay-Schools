using Domain.Enums;

namespace Application.Common.Authorization;

/// <summary>Which church roles may do what. Change permissions here, not in the handlers.</summary>
public static class ChurchRoles
{
      /// <summary>Manage the church's details, members and roles.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> Managers =
            [ChurchMemberRole.Admin, ChurchMemberRole.Priest];

      /// <summary>Create, update and delete the church's services.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> ServiceManagers =
            [ChurchMemberRole.Admin, ChurchMemberRole.Priest, ChurchMemberRole.ChurchServantHead];

      /// <summary>Schedule, reschedule and delete sessions.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> SessionManagers =
            [ChurchMemberRole.Admin, ChurchMemberRole.Priest, ChurchMemberRole.ChurchServantHead, ChurchMemberRole.ServiceHead];

      /// <summary>Take and view attendance. Matches the rule in ChurchServiceSession.RecordAttendance.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> Servants =
            Enum.GetValues<ChurchMemberRole>().Where(r => r != ChurchMemberRole.Member).ToArray();

      /// <summary>See the church's members, services and sessions.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> AnyMember = Enum.GetValues<ChurchMemberRole>();

      /// <summary>View servant attendance. Restricted to leadership only.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> ServantAttendanceViewers =
            [ChurchMemberRole.Admin, ChurchMemberRole.Priest, ChurchMemberRole.ChurchServantHead];

      /// <summary>Granting, changing or removing the church Admin role.</summary>
      public static readonly IReadOnlyCollection<ChurchMemberRole> AdminOnly = [ChurchMemberRole.Admin];
}

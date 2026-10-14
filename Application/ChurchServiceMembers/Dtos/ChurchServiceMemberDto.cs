using Domain.Entities;
using Domain.Enums;

namespace Application.ChurchServiceMembers.Dtos;

public record ChurchServiceMemberDto(
    Guid Id,
    Guid ChurchServiceId,
    Guid ChurchMemberId,
    Guid UserId,
    string? UserDisplayName,
    ChurchServiceRole Role)
{
      public static ChurchServiceMemberDto FromEntity(ChurchServiceMember sm) => new(
            sm.Id,
            sm.ChurchServiceId,
            sm.ChurchMemberId,
            sm.ChurchMember?.UserId ?? Guid.Empty,
            sm.ChurchMember?.User?.DisplayName,
            sm.Role);
}

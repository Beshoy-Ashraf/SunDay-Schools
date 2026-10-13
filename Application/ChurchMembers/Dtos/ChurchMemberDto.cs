using Domain.Entities;
using Domain.Enums;

namespace Application.ChurchMembers.Dtos;

public record ChurchMemberDto(
    Guid Id,
    Guid UserId,
    string? UserDisplayName,
    Guid ChurchId,
    string? ChurchName,
    ChurchMemberRole Role,
    DateTime DateJoined,
    DateTime? DateLeft,
    string? Notes,
    bool IsActive)
{
      // User / Church are filled only when the query loaded them.
      public static ChurchMemberDto FromEntity(ChurchMember member) => new(
            member.Id,
            member.UserId,
            member.User?.DisplayName,
            member.ChurchId,
            member.Church?.Name,
            member.Role,
            member.DateJoined,
            member.DateLeft,
            member.Notes,
            member.IsActive);
}

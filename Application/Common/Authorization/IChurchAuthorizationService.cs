using Domain.Enums;

namespace Application.Common.Authorization;

/// <summary>
/// Permission checks for the current user. System admins pass every church check.
/// Ensure* methods throw ForbiddenException when the check fails.
/// </summary>
public interface IChurchAuthorizationService
{
      Guid CurrentUserId { get; }
      Task<bool> IsSystemAdminAsync(CancellationToken cancellationToken);
      Task EnsureSystemAdminAsync(CancellationToken cancellationToken);
      Task<bool> HasChurchRoleAsync(Guid churchId, IReadOnlyCollection<ChurchMemberRole> roles, CancellationToken cancellationToken);
      Task EnsureChurchRoleAsync(Guid churchId, IReadOnlyCollection<ChurchMemberRole> roles, CancellationToken cancellationToken);
}

using Application.Common.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Common.Authorization;

// Roles are read from the database on each request, so a demotion takes effect immediately
// rather than when the user's access token expires.
public class ChurchAuthorizationService(IUnitOfWork context, ICurrentUserService currentUser) : IChurchAuthorizationService
{
      private bool? _isSystemAdmin;

      public Guid CurrentUserId => currentUser.UserId ?? throw new UnauthorizedException("User is not authenticated.");

      public async Task<bool> IsSystemAdminAsync(CancellationToken cancellationToken)
      {
            if (_isSystemAdmin is null)
            {
                  var userId = CurrentUserId;
                  var user = (await context.UserRepository.GetAllAsync(u => u.Id == userId, cancellationToken)).FirstOrDefault()
                      ?? throw new UnauthorizedException("User not found.");
                  _isSystemAdmin = user.Role == SystemRole.Admin;
            }

            return _isSystemAdmin.Value;
      }

      public async Task EnsureSystemAdminAsync(CancellationToken cancellationToken)
      {
            if (!await IsSystemAdminAsync(cancellationToken))
                  throw new ForbiddenException("This action requires a system administrator.");
      }

      public async Task<bool> HasChurchRoleAsync(Guid churchId, IReadOnlyCollection<ChurchMemberRole> roles, CancellationToken cancellationToken)
      {
            if (await IsSystemAdminAsync(cancellationToken))
                  return true;

            var membership = await context.ChurchMemberRepository.GetActiveMembershipAsync(CurrentUserId, churchId, cancellationToken);
            return membership is not null && roles.Contains(membership.Role);
      }

      public async Task EnsureChurchRoleAsync(Guid churchId, IReadOnlyCollection<ChurchMemberRole> roles, CancellationToken cancellationToken)
      {
            if (!await HasChurchRoleAsync(churchId, roles, cancellationToken))
                  throw new ForbiddenException("You do not have permission to perform this action in this church.");
      }
}

using Application.Common.Authorization;
using Application.Users.Dtos;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.ChangeSystemRole;

public class ChangeSystemRoleCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<ChangeSystemRoleCommand, UserDto>
{
      public async Task<UserDto> Handle(ChangeSystemRoleCommand request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            if (request.UserId == authorization.CurrentUserId && request.Role != SystemRole.Admin)
                  throw new BadRequestException("You cannot remove your own system admin role.");

            var user = await context.UserRepository.GetByIdAsync(request.UserId, cancellationToken);
            user.ChangeSystemRole(request.Role);
            await context.Complete(cancellationToken);

            return new UserDto(user.Id, user.Username, user.Email, user.DisplayName, user.NationalId, user.ParentId, user.Role.ToString());
      }
}

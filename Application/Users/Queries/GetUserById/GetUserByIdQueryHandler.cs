using Application.Common.Authorization;
using Application.Users.Dtos;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
      public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
      {
            var isAdmin = await authorization.IsSystemAdminAsync(cancellationToken);
            if (!isAdmin && request.UserId != authorization.CurrentUserId)
                  throw new ForbiddenException("You can only view your own profile.");

            var user = await context.UserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsDeleted)
                  throw new NotFoundException(nameof(user), request.UserId);

            return new UserDto(user.Id, user.Username, user.Email, user.DisplayName, user.NationalId, user.ParentId, user.Role.ToString());
      }
}

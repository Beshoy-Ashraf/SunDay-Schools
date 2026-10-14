using Application.Common.Authorization;
using Application.Users.Dtos;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<UpdateUserCommand, UserDto>
{
      public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            var user = await context.UserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsDeleted)
                  throw new NotFoundException(nameof(user), request.UserId);

            user.UpdateProfile(request.Username, request.Email, request.DisplayName, request.NationalId, request.ParentId);
            await context.Complete(cancellationToken);

            return new UserDto(user.Id, user.Username, user.Email, user.DisplayName, user.NationalId, user.ParentId, user.Role.ToString());
      }
}

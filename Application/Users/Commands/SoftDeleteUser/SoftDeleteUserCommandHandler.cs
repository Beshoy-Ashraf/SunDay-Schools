using Application.Common.Authorization;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Commands.SoftDeleteUser;

public class SoftDeleteUserCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<SoftDeleteUserCommand>
{
      public async Task Handle(SoftDeleteUserCommand request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            if (request.UserId == authorization.CurrentUserId)
                  throw new BadRequestException("You cannot delete your own account.");

            var user = await context.UserRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user.IsDeleted)
                  throw new NotFoundException(nameof(user), request.UserId);

            user.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

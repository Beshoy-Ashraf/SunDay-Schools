using Application.Common.Authorization;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Churches.Commands.DeleteChurch;

public class DeleteChurchCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<DeleteChurchCommand>
{
      public async Task Handle(DeleteChurchCommand request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            var church = await context.ChurchRepository.GetByIdAsync(request.Id, cancellationToken);

            if ((await context.ChurchMemberRepository.GetActiveMembersAsync(church.Id, cancellationToken)).Count != 0)
                  throw new ConflictException("Cannot delete a church that still has active members.");

            if ((await context.ChurchServiceRepository.GetByChurchAsync(church.Id, cancellationToken)).Count != 0)
                  throw new ConflictException("Cannot delete a church that still has services.");

            church.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

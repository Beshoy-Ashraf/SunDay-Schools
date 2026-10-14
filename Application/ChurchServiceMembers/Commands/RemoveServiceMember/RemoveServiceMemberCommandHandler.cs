using Application.Common.Authorization;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceMembers.Commands.RemoveServiceMember;

public class RemoveServiceMemberCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<RemoveServiceMemberCommand>
{
      public async Task Handle(RemoveServiceMemberCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ServiceManagers, cancellationToken);

            var assignment = await context.ChurchServiceMemberRepository.GetAssignmentAsync(request.ChurchServiceId, request.ChurchMemberId, cancellationToken)
                  ?? throw new NotFoundException("Service member assignment not found.");

            assignment.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

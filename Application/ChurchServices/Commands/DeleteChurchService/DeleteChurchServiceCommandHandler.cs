using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Commands.DeleteChurchService;

public class DeleteChurchServiceCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<DeleteChurchServiceCommand>
{
      public async Task Handle(DeleteChurchServiceCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ServiceManagers, cancellationToken);

            service.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.DeleteSession;

public class DeleteSessionCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<DeleteSessionCommand>
{
      public async Task Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.FindAsync(
                  s => s.Id == request.Id, cancellationToken, ["ChurchService"]);
            await authorization.EnsureChurchRoleAsync(session.ChurchService.ChurchId, ChurchRoles.SessionManagers, cancellationToken);

            session.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

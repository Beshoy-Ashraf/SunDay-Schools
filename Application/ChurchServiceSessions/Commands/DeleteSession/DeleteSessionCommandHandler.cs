using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.DeleteSession;

public class DeleteSessionCommandHandler(IUnitOfWork context) : IRequestHandler<DeleteSessionCommand>
{
      public async Task Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.GetByIdAsync(request.Id, cancellationToken);

            session.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

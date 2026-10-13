using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Eparchies.Commands.DeleteEparchy;

public class DeleteEparchyCommandHandler(IUnitOfWork context) : IRequestHandler<DeleteEparchyCommand>
{
      public async Task Handle(DeleteEparchyCommand request, CancellationToken cancellationToken)
      {
            var eparchy = await context.EparchyRepository.GetWithChurchesAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException("Eparchy", request.Id);

            if (eparchy.Churches.Count != 0)
                  throw new ConflictException("Cannot delete an eparchy that still has churches.");

            eparchy.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

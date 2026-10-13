using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Commands.DeleteChurchService;

public class DeleteChurchServiceCommandHandler(IUnitOfWork context) : IRequestHandler<DeleteChurchServiceCommand>
{
      public async Task Handle(DeleteChurchServiceCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.Id, cancellationToken);

            service.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

using Application.Common.Authorization;
using Application.ChurchServices.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Commands.UpdateChurchService;

public class UpdateChurchServiceCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<UpdateChurchServiceCommand, ChurchServiceDto>
{
      public async Task<ChurchServiceDto> Handle(UpdateChurchServiceCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ServiceManagers, cancellationToken);

            service.Update(request.Name, request.Description, request.StartTime, request.EndTime);
            await context.Complete(cancellationToken);

            return ChurchServiceDto.FromEntity(service);
      }
}

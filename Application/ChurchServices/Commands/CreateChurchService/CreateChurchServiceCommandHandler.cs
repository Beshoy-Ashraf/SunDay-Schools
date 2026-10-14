using Application.Common.Authorization;
using Application.ChurchServices.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Commands.CreateChurchService;

public class CreateChurchServiceCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<CreateChurchServiceCommand, ChurchServiceDto>
{
      public async Task<ChurchServiceDto> Handle(CreateChurchServiceCommand request, CancellationToken cancellationToken)
      {
            await context.ChurchRepository.GetByIdAsync(request.ChurchId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(request.ChurchId, ChurchRoles.ServiceManagers, cancellationToken);

            var service = new ChurchService(request.ChurchId, request.Name, request.Description, request.StartTime, request.EndTime);
            await context.ChurchServiceRepository.AddAsync(service, cancellationToken);
            await context.Complete(cancellationToken);

            return ChurchServiceDto.FromEntity(service);
      }
}

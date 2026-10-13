using Application.ChurchServices.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Commands.CreateChurchService;

public class CreateChurchServiceCommandHandler(IUnitOfWork context) : IRequestHandler<CreateChurchServiceCommand, ChurchServiceDto>
{
      public async Task<ChurchServiceDto> Handle(CreateChurchServiceCommand request, CancellationToken cancellationToken)
      {
            await context.ChurchRepository.GetByIdAsync(request.ChurchId, cancellationToken);

            var service = new ChurchService(request.ChurchId, request.Name, request.Description, request.StartTime, request.EndTime);
            await context.ChurchServiceRepository.AddAsync(service, cancellationToken);
            await context.Complete(cancellationToken);

            return ChurchServiceDto.FromEntity(service);
      }
}

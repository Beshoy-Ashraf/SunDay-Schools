using Application.ChurchServices.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Queries.GetChurchServiceById;

public class GetChurchServiceByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetChurchServiceByIdQuery, ChurchServiceDto>
{
      public async Task<ChurchServiceDto> Handle(GetChurchServiceByIdQuery request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            return ChurchServiceDto.FromEntity(service);
      }
}

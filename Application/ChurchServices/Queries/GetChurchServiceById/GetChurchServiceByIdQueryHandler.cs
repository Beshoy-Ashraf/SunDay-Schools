using Application.Common.Authorization;
using Application.ChurchServices.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Queries.GetChurchServiceById;

public class GetChurchServiceByIdQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<GetChurchServiceByIdQuery, ChurchServiceDto>
{
      public async Task<ChurchServiceDto> Handle(GetChurchServiceByIdQuery request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.Id, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.AnyMember, cancellationToken);
            return ChurchServiceDto.FromEntity(service);
      }
}

using Application.Common.Authorization;
using Application.ChurchServices.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServices.Queries.GetChurchServicesByChurch;

public class GetChurchServicesByChurchQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetChurchServicesByChurchQuery, IReadOnlyList<ChurchServiceDto>>
{
      public async Task<IReadOnlyList<ChurchServiceDto>> Handle(GetChurchServicesByChurchQuery request, CancellationToken cancellationToken)
      {
            await context.ChurchRepository.GetByIdAsync(request.ChurchId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(request.ChurchId, ChurchRoles.AnyMember, cancellationToken);

            var services = await context.ChurchServiceRepository.GetByChurchAsync(request.ChurchId, cancellationToken);
            return services.Select(ChurchServiceDto.FromEntity).ToList();
      }
}

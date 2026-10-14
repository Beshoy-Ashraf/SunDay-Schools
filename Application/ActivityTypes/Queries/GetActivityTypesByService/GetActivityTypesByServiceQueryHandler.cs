using Application.Common.Authorization;
using Application.ActivityTypes.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ActivityTypes.Queries.GetActivityTypesByService;

public class GetActivityTypesByServiceQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetActivityTypesByServiceQuery, IReadOnlyList<ActivityTypeDto>>
{
      public async Task<IReadOnlyList<ActivityTypeDto>> Handle(GetActivityTypesByServiceQuery request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.AnyMember, cancellationToken);

            var types = await context.ActivityTypeRepository.GetByServiceAsync(request.ChurchServiceId, cancellationToken);
            return types.Select(ActivityTypeDto.FromEntity).ToList();
      }
}

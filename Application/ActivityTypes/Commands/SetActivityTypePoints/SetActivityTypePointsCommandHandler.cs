using Application.Common.Authorization;
using Application.ActivityTypes.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ActivityTypes.Commands.SetActivityTypePoints;

public class SetActivityTypePointsCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<SetActivityTypePointsCommand, ActivityTypeDto>
{
      public async Task<ActivityTypeDto> Handle(SetActivityTypePointsCommand request, CancellationToken cancellationToken)
      {
            var activityType = await context.ActivityTypeRepository.GetByIdAsync(request.Id, cancellationToken);
            var service = await context.ChurchServiceRepository.GetByIdAsync(activityType.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ActivityTypeManagers, cancellationToken);

            activityType.SetPoints(request.Points, request.MaxPerMonth);
            await context.Complete(cancellationToken);

            return ActivityTypeDto.FromEntity(activityType);
      }
}

using Application.Common.Authorization;
using Application.ActivityTypes.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ActivityTypes.Commands.SetActivityTypeStatus;

public class SetActivityTypeStatusCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<SetActivityTypeStatusCommand, ActivityTypeDto>
{
      public async Task<ActivityTypeDto> Handle(SetActivityTypeStatusCommand request, CancellationToken cancellationToken)
      {
            var activityType = await context.ActivityTypeRepository.GetByIdAsync(request.Id, cancellationToken);
            var service = await context.ChurchServiceRepository.GetByIdAsync(activityType.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ActivityTypeManagers, cancellationToken);

            if (request.IsActive) activityType.Activate();
            else activityType.Deactivate();

            await context.Complete(cancellationToken);

            return ActivityTypeDto.FromEntity(activityType);
      }
}

using Application.Common.Authorization;
using Application.ActivityTypes.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.ActivityTypes.Commands.CreateActivityType;

public class CreateActivityTypeCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<CreateActivityTypeCommand, ActivityTypeDto>
{
      public async Task<ActivityTypeDto> Handle(CreateActivityTypeCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ActivityTypeManagers, cancellationToken);

            if (await context.ActivityTypeRepository.GetByServiceAndCodeAsync(request.ChurchServiceId, request.Code, cancellationToken) is not null)
                  throw new ConflictException($"An activity type with code '{request.Code}' already exists for this service.");

            var activityType = new ActivityType(request.ChurchServiceId, request.Code, request.Name,
                                                request.AppliesTo, request.Points, request.MaxPerMonth);
            await context.ActivityTypeRepository.AddAsync(activityType, cancellationToken);
            await context.Complete(cancellationToken);

            return ActivityTypeDto.FromEntity(activityType);
      }
}

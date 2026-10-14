using Application.ActivityTypes.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.ActivityTypes.Commands.CreateActivityType;

public record CreateActivityTypeCommand(
    Guid ChurchServiceId,
    ActivityTypeCode Code,
    string Name,
    ActivityAppliesTo AppliesTo,
    int Points,
    int? MaxPerMonth)
    : IRequest<ActivityTypeDto>;

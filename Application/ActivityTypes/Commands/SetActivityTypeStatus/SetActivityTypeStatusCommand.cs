using Application.ActivityTypes.Dtos;
using MediatR;

namespace Application.ActivityTypes.Commands.SetActivityTypeStatus;

public record SetActivityTypeStatusCommand(Guid Id, bool IsActive) : IRequest<ActivityTypeDto>;

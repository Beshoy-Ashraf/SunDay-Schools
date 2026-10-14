using Application.ActivityTypes.Dtos;
using MediatR;

namespace Application.ActivityTypes.Commands.SetActivityTypePoints;

public record SetActivityTypePointsCommand(Guid Id, int Points, int? MaxPerMonth) : IRequest<ActivityTypeDto>;

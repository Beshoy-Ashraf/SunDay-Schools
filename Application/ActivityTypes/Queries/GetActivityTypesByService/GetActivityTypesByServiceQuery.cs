using Application.ActivityTypes.Dtos;
using MediatR;

namespace Application.ActivityTypes.Queries.GetActivityTypesByService;

public record GetActivityTypesByServiceQuery(Guid ChurchServiceId) : IRequest<IReadOnlyList<ActivityTypeDto>>;

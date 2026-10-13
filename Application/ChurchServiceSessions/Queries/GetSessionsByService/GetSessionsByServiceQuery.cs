using Application.ChurchServiceSessions.Dtos;
using MediatR;

namespace Application.ChurchServiceSessions.Queries.GetSessionsByService;

public record GetSessionsByServiceQuery(Guid ChurchServiceId, DateTime? From, DateTime? To) : IRequest<IReadOnlyList<ChurchServiceSessionDto>>;

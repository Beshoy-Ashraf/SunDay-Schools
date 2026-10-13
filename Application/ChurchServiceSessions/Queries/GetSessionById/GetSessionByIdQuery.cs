using Application.ChurchServiceSessions.Dtos;
using MediatR;

namespace Application.ChurchServiceSessions.Queries.GetSessionById;

public record GetSessionByIdQuery(Guid Id) : IRequest<ChurchServiceSessionDto>;

using Application.ChurchServiceSessions.Dtos;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.RescheduleSession;

public record RescheduleSessionCommand(Guid Id, DateTime StartTime, DateTime EndTime) : IRequest<ChurchServiceSessionDto>;

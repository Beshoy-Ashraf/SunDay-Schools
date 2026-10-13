using Application.ChurchServiceSessions.Dtos;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.ScheduleSession;

public record ScheduleSessionCommand(Guid ChurchServiceId, DateTime StartTime, DateTime EndTime) : IRequest<ChurchServiceSessionDto>;

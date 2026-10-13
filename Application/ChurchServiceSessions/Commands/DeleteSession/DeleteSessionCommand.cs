using MediatR;

namespace Application.ChurchServiceSessions.Commands.DeleteSession;

public record DeleteSessionCommand(Guid Id) : IRequest;

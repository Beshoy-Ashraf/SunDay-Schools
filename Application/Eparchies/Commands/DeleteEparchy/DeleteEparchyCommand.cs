using MediatR;

namespace Application.Eparchies.Commands.DeleteEparchy;

public record DeleteEparchyCommand(Guid Id) : IRequest;

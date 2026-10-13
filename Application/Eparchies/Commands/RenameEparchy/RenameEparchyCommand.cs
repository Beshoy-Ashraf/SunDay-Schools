using Application.Eparchies.Dtos;
using MediatR;

namespace Application.Eparchies.Commands.RenameEparchy;

public record RenameEparchyCommand(Guid Id, string Name) : IRequest<EparchyDto>;

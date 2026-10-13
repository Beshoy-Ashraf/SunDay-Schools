using Application.Eparchies.Dtos;
using MediatR;

namespace Application.Eparchies.Commands.CreateEparchy;

public record CreateEparchyCommand(string Name) : IRequest<EparchyDto>;

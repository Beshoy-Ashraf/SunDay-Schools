using Application.Eparchies.Dtos;
using MediatR;

namespace Application.Eparchies.Queries.GetEparchies;

public record GetEparchiesQuery : IRequest<IReadOnlyList<EparchyDto>>;

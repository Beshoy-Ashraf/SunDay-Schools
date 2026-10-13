using Application.Churches.Dtos;
using MediatR;

namespace Application.Churches.Queries.GetChurchesByEparchy;

public record GetChurchesByEparchyQuery(Guid EparchyId) : IRequest<IReadOnlyList<ChurchDto>>;

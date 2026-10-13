using Application.Eparchies.Dtos;
using MediatR;

namespace Application.Eparchies.Queries.GetEparchyById;

public record GetEparchyByIdQuery(Guid Id) : IRequest<EparchyDto>;

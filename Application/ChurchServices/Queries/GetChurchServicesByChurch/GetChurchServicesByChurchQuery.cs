using Application.ChurchServices.Dtos;
using MediatR;

namespace Application.ChurchServices.Queries.GetChurchServicesByChurch;

public record GetChurchServicesByChurchQuery(Guid ChurchId) : IRequest<IReadOnlyList<ChurchServiceDto>>;

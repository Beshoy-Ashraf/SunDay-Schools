using Application.ChurchServices.Dtos;
using MediatR;

namespace Application.ChurchServices.Queries.GetChurchServiceById;

public record GetChurchServiceByIdQuery(Guid Id) : IRequest<ChurchServiceDto>;

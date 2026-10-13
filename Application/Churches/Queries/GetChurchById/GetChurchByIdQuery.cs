using Application.Churches.Dtos;
using MediatR;

namespace Application.Churches.Queries.GetChurchById;

public record GetChurchByIdQuery(Guid Id) : IRequest<ChurchDto>;

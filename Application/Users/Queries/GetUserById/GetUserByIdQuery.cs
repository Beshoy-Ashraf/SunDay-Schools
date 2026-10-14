using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;

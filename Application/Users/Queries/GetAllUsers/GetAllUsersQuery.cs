using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>;

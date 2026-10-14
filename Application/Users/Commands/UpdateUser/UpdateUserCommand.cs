using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string Username,
    string Email,
    string DisplayName,
    string? NationalId,
    Guid? ParentId) : IRequest<UserDto>;

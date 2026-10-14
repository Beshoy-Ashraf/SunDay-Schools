using Application.Users.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Users.Commands.ChangeSystemRole;

public record ChangeSystemRoleCommand(Guid UserId, SystemRole Role) : IRequest<UserDto>;

using MediatR;

namespace Application.Users.Commands.SoftDeleteUser;

public record SoftDeleteUserCommand(Guid UserId) : IRequest;

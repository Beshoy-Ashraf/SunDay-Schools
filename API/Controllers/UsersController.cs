namespace API.Controllers;

using Application.Users.Commands.ChangeSystemRole;
using Application.Users.Commands.SoftDeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries.GetAllUsers;
using Application.Users.Queries.GetUserById;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetAllUsersQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserByIdQuery(id), cancellationToken));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new UpdateUserCommand(id, request.Username, request.Email, request.DisplayName, request.NationalId, request.ParentId), cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new SoftDeleteUserCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPut("{id:guid}/system-role")]
    public async Task<IActionResult> ChangeSystemRole(Guid id, ChangeSystemRoleRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new ChangeSystemRoleCommand(id, request.Role), cancellationToken));
}

public record UpdateUserRequest(string Username, string Email, string DisplayName, string? NationalId, Guid? ParentId);
public record ChangeSystemRoleRequest(SystemRole Role);

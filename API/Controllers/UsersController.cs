namespace API.Controllers;

using Application.Users.Commands.ChangeSystemRole;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/users")]
public class UsersController(ISender mediator) : ControllerBase
{
    [HttpPut("{id:guid}/system-role")]
    public async Task<IActionResult> ChangeSystemRole(Guid id, ChangeSystemRoleRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new ChangeSystemRoleCommand(id, request.Role), cancellationToken));
}

public record ChangeSystemRoleRequest(SystemRole Role);

namespace API.Controllers;

using Application.Attendances.Queries.GetMemberAttendance;
using Application.ChurchMembers.Commands.AddChurchMember;
using Application.ChurchMembers.Commands.ChangeMemberRole;
using Application.ChurchMembers.Commands.LeaveChurch;
using Application.ChurchMembers.Queries.GetChurchMemberById;
using Application.ChurchMembers.Queries.GetUserMemberships;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ApiController]
[Authorize]
[Route("api/church-members")]
public class ChurchMembersController(ISender mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetChurchMemberByIdQuery(id), cancellationToken));

    [HttpGet("me")]
    public async Task<IActionResult> GetMyMemberships(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserMembershipsQuery(null), cancellationToken));

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetUserMemberships(Guid userId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetUserMembershipsQuery(userId), cancellationToken));

    [HttpGet("{id:guid}/attendance")]
    public async Task<IActionResult> GetAttendance(Guid id, DateTime? from, DateTime? to, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMemberAttendanceQuery(id, from, to), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Add(AddChurchMemberCommand command, CancellationToken cancellationToken)
    {
        var member = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
    }

    [HttpPut("{id:guid}/role")]
    public async Task<IActionResult> ChangeRole(Guid id, ChangeMemberRoleRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new ChangeMemberRoleCommand(id, request.Role), cancellationToken));

    [HttpPost("{id:guid}/leave")]
    public async Task<IActionResult> Leave(
        Guid id,
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] LeaveChurchRequest? request,
        CancellationToken cancellationToken)
        => Ok(await mediator.Send(new LeaveChurchCommand(id, request?.DateLeft), cancellationToken));
}

public record ChangeMemberRoleRequest(ChurchMemberRole Role);

public record LeaveChurchRequest(DateTime? DateLeft);

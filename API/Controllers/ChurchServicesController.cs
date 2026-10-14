namespace API.Controllers;

using Application.Attendances.Commands.RecordAttendance;
using Application.Attendances.Queries.GetServiceAttendance;
using Application.Attendances.Queries.GetServantAttendanceByService;
using Application.ChurchServices.Commands.CreateChurchService;
using Application.ChurchServices.Commands.DeleteChurchService;
using Application.ChurchServices.Commands.UpdateChurchService;
using Application.ChurchServices.Queries.GetChurchServiceById;
using Application.ChurchServiceMembers.Commands.AssignServiceMember;
using Application.ChurchServiceMembers.Commands.RemoveServiceMember;
using Application.ChurchServiceMembers.Queries.GetServiceMembers;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/church-services")]
public class ChurchServicesController(ISender mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetChurchServiceByIdQuery(id), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(CreateChurchServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateChurchServiceRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(
            new UpdateChurchServiceCommand(id, request.Name, request.Description, request.StartTime, request.EndTime), cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteChurchServiceCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/members")]
    public async Task<IActionResult> GetMembers(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetServiceMembersQuery(id), cancellationToken));

    [HttpPost("{id:guid}/members")]
    public async Task<IActionResult> AssignMember(Guid id, AssignServiceMemberRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new AssignServiceMemberCommand(id, request.ChurchMemberId, request.Role), cancellationToken));

    [HttpDelete("{id:guid}/members/{memberId:guid}")]
    public async Task<IActionResult> RemoveMember(Guid id, Guid memberId, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveServiceMemberCommand(id, memberId), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/attendance")]
    public async Task<IActionResult> GetAttendance(Guid id, DateTime? from, DateTime? to, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetServiceAttendanceQuery(id, from, to), cancellationToken));

    [HttpPost("{id:guid}/attendance")]
    public async Task<IActionResult> RecordAttendance(Guid id, RecordAttendanceRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new RecordAttendanceCommand(id, request.ChurchMemberId, request.AttendedAt), cancellationToken));

    [HttpGet("{id:guid}/servant-attendance")]
    public async Task<IActionResult> GetServantAttendance(Guid id, DateTime? from, DateTime? to, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetServantAttendanceByServiceQuery(id, from, to), cancellationToken));
}

public record UpdateChurchServiceRequest(string Name, string? Description, DateTime StartTime, DateTime EndTime);
public record AssignServiceMemberRequest(Guid ChurchMemberId, ChurchServiceRole Role);
public record RecordAttendanceRequest(Guid ChurchMemberId, DateTime? AttendedAt);

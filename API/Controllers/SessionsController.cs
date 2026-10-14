namespace API.Controllers;

using Application.Attendances.Commands.RecordAttendance;
using Application.Attendances.Queries.GetServantAttendanceBySession;
using Application.Attendances.Queries.GetSessionAttendance;
using Application.ChurchServiceSessions.Commands.DeleteSession;
using Application.ChurchServiceSessions.Commands.RescheduleSession;
using Application.ChurchServiceSessions.Commands.ScheduleSession;
using Application.ChurchServiceSessions.Queries.GetSessionById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/sessions")]
public class SessionsController(ISender mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetSessionByIdQuery(id), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Schedule(ScheduleSessionCommand command, CancellationToken cancellationToken)
    {
        var session = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Reschedule(Guid id, RescheduleSessionRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new RescheduleSessionCommand(id, request.StartTime, request.EndTime), cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteSessionCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/attendance")]
    public async Task<IActionResult> GetAttendance(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetSessionAttendanceQuery(id), cancellationToken));

    [HttpPost("{id:guid}/attendance")]
    public async Task<IActionResult> RecordAttendance(Guid id, RecordAttendanceRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new RecordAttendanceCommand(id, request.ChurchMemberId, request.AttendedAt), cancellationToken));

    [HttpGet("{id:guid}/servant-attendance")]
    public async Task<IActionResult> GetServantAttendance(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetServantAttendanceBySessionQuery(id), cancellationToken));
}

public record RescheduleSessionRequest(DateTime StartTime, DateTime EndTime);

public record RecordAttendanceRequest(Guid ChurchMemberId, DateTime? AttendedAt);

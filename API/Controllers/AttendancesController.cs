namespace API.Controllers;

using Application.Attendances.Commands.RemoveAttendance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/attendances")]
public class AttendancesController(ISender mediator) : ControllerBase
{
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveAttendanceCommand(id), cancellationToken);
        return NoContent();
    }
}

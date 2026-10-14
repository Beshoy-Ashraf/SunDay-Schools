namespace API.Controllers;

using Application.ActivityTypes.Commands.CreateActivityType;
using Application.ActivityTypes.Commands.SetActivityTypePoints;
using Application.ActivityTypes.Commands.SetActivityTypeStatus;
using Application.ActivityTypes.Queries.GetActivityTypesByService;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/activity-types")]
public class ActivityTypesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByService([FromQuery] Guid churchServiceId, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetActivityTypesByServiceQuery(churchServiceId), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(CreateActivityTypeCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(command, cancellationToken));

    [HttpPatch("{id:guid}/points")]
    public async Task<IActionResult> SetPoints(Guid id, SetPointsRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new SetActivityTypePointsCommand(id, request.Points, request.MaxPerMonth), cancellationToken));

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> SetStatus(Guid id, SetStatusRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new SetActivityTypeStatusCommand(id, request.IsActive), cancellationToken));
}

public record SetPointsRequest(int Points, int? MaxPerMonth);
public record SetStatusRequest(bool IsActive);

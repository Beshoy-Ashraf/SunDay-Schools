namespace API.Controllers;

using Application.ChurchServices.Commands.CreateChurchService;
using Application.ChurchServices.Commands.DeleteChurchService;
using Application.ChurchServices.Commands.UpdateChurchService;
using Application.ChurchServices.Queries.GetChurchServiceById;
using Application.ChurchServiceSessions.Queries.GetSessionsByService;
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

    [HttpGet("{id:guid}/sessions")]
    public async Task<IActionResult> GetSessions(Guid id, DateTime? from, DateTime? to, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetSessionsByServiceQuery(id, from, to), cancellationToken));

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
}

public record UpdateChurchServiceRequest(string Name, string? Description, DateTime StartTime, DateTime EndTime);

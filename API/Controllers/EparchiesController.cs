namespace API.Controllers;

using Application.Churches.Queries.GetChurchesByEparchy;
using Application.Eparchies.Commands.CreateEparchy;
using Application.Eparchies.Commands.DeleteEparchy;
using Application.Eparchies.Commands.RenameEparchy;
using Application.Eparchies.Queries.GetEparchies;
using Application.Eparchies.Queries.GetEparchyById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/eparchies")]
public class EparchiesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetEparchiesQuery(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetEparchyByIdQuery(id), cancellationToken));

    [HttpGet("{id:guid}/churches")]
    public async Task<IActionResult> GetChurches(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetChurchesByEparchyQuery(id), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(CreateEparchyCommand command, CancellationToken cancellationToken)
    {
        var eparchy = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = eparchy.Id }, eparchy);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Rename(Guid id, RenameEparchyRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new RenameEparchyCommand(id, request.Name), cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteEparchyCommand(id), cancellationToken);
        return NoContent();
    }
}

public record RenameEparchyRequest(string Name);

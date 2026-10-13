namespace API.Controllers;

using Application.ChurchMembers.Queries.GetChurchMembers;
using Application.Churches.Commands.CreateChurch;
using Application.Churches.Commands.DeleteChurch;
using Application.Churches.Commands.UpdateChurch;
using Application.Churches.Queries.GetChurchById;
using Application.ChurchServices.Queries.GetChurchServicesByChurch;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/churches")]
public class ChurchesController(ISender mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetChurchByIdQuery(id), cancellationToken));

    [HttpGet("{id:guid}/members")]
    public async Task<IActionResult> GetMembers(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetChurchMembersQuery(id), cancellationToken));

    [HttpGet("{id:guid}/services")]
    public async Task<IActionResult> GetServices(Guid id, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetChurchServicesByChurchQuery(id), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(CreateChurchCommand command, CancellationToken cancellationToken)
    {
        var church = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = church.Id }, church);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateChurchRequest request, CancellationToken cancellationToken)
        => Ok(await mediator.Send(
            new UpdateChurchCommand(id, request.Name, request.Address, request.PhoneNumber, request.Email), cancellationToken));

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteChurchCommand(id), cancellationToken);
        return NoContent();
    }
}

public record UpdateChurchRequest(string Name, string Address, string PhoneNumber, string Email);

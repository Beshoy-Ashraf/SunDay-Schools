using Application.ChurchServices.Dtos;
using MediatR;

namespace Application.ChurchServices.Commands.UpdateChurchService;

public record UpdateChurchServiceCommand(Guid Id, string Name, string? Description, DateTime StartTime, DateTime EndTime)
    : IRequest<ChurchServiceDto>;

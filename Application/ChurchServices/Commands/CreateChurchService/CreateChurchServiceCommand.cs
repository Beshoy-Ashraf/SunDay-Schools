using Application.ChurchServices.Dtos;
using MediatR;

namespace Application.ChurchServices.Commands.CreateChurchService;

public record CreateChurchServiceCommand(Guid ChurchId, string Name, string? Description, DateTime StartTime, DateTime EndTime)
    : IRequest<ChurchServiceDto>;

using MediatR;

namespace Application.ChurchServices.Commands.DeleteChurchService;

public record DeleteChurchServiceCommand(Guid Id) : IRequest;

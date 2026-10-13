using MediatR;

namespace Application.Churches.Commands.DeleteChurch;

public record DeleteChurchCommand(Guid Id) : IRequest;

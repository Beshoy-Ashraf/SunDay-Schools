using Application.Churches.Dtos;
using MediatR;

namespace Application.Churches.Commands.UpdateChurch;

public record UpdateChurchCommand(Guid Id, string Name, string Address, string PhoneNumber, string Email) : IRequest<ChurchDto>;

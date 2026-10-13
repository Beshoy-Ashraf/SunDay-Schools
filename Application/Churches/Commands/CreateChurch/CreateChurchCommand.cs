using Application.Churches.Dtos;
using MediatR;

namespace Application.Churches.Commands.CreateChurch;

public record CreateChurchCommand(Guid EparchyId, string Name, string Address, string PhoneNumber, string Email) : IRequest<ChurchDto>;

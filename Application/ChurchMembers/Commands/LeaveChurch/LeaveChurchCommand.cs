using Application.ChurchMembers.Dtos;
using MediatR;

namespace Application.ChurchMembers.Commands.LeaveChurch;

public record LeaveChurchCommand(Guid Id, DateTime? DateLeft) : IRequest<ChurchMemberDto>;

using Application.ChurchMembers.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.ChurchMembers.Commands.AddChurchMember;

public record AddChurchMemberCommand(Guid ChurchId, Guid UserId, ChurchMemberRole Role, DateTime? DateJoined, string? Notes)
    : IRequest<ChurchMemberDto>;

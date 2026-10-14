using Application.ChurchServiceMembers.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.ChurchServiceMembers.Commands.AssignServiceMember;

public record AssignServiceMemberCommand(Guid ChurchServiceId, Guid ChurchMemberId, ChurchServiceRole Role)
    : IRequest<ChurchServiceMemberDto>;

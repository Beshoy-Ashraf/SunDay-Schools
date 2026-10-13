using Application.ChurchMembers.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.ChurchMembers.Commands.ChangeMemberRole;

public record ChangeMemberRoleCommand(Guid Id, ChurchMemberRole Role) : IRequest<ChurchMemberDto>;

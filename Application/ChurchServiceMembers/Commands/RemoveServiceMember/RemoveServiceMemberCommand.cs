using MediatR;

namespace Application.ChurchServiceMembers.Commands.RemoveServiceMember;

public record RemoveServiceMemberCommand(Guid ChurchServiceId, Guid ChurchMemberId) : IRequest;

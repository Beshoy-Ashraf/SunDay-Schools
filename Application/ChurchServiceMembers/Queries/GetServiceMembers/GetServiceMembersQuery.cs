using Application.ChurchServiceMembers.Dtos;
using MediatR;

namespace Application.ChurchServiceMembers.Queries.GetServiceMembers;

public record GetServiceMembersQuery(Guid ChurchServiceId) : IRequest<IReadOnlyList<ChurchServiceMemberDto>>;

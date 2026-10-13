using Application.ChurchMembers.Dtos;
using MediatR;

namespace Application.ChurchMembers.Queries.GetChurchMembers;

/// <summary>Active members of a church.</summary>
public record GetChurchMembersQuery(Guid ChurchId) : IRequest<IReadOnlyList<ChurchMemberDto>>;

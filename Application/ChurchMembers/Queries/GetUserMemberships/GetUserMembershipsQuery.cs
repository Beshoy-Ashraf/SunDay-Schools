using Application.ChurchMembers.Dtos;
using MediatR;

namespace Application.ChurchMembers.Queries.GetUserMemberships;

/// <summary>All memberships (current and past) of a user. A null UserId means the current user.</summary>
public record GetUserMembershipsQuery(Guid? UserId) : IRequest<IReadOnlyList<ChurchMemberDto>>;

using Application.Common.Authorization;
using Application.ChurchMembers.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Queries.GetChurchMembers;

public class GetChurchMembersQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<GetChurchMembersQuery, IReadOnlyList<ChurchMemberDto>>
{
      public async Task<IReadOnlyList<ChurchMemberDto>> Handle(GetChurchMembersQuery request, CancellationToken cancellationToken)
      {
            await context.ChurchRepository.GetByIdAsync(request.ChurchId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(request.ChurchId, ChurchRoles.AnyMember, cancellationToken);

            var members = await context.ChurchMemberRepository.GetActiveMembersAsync(request.ChurchId, cancellationToken);
            return members.Select(ChurchMemberDto.FromEntity).ToList();
      }
}

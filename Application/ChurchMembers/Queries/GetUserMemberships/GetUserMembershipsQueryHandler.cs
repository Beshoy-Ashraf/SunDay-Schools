using Application.ChurchMembers.Dtos;
using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Queries.GetUserMemberships;

public class GetUserMembershipsQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetUserMembershipsQuery, IReadOnlyList<ChurchMemberDto>>
{
      public async Task<IReadOnlyList<ChurchMemberDto>> Handle(GetUserMembershipsQuery request, CancellationToken cancellationToken)
      {
            var userId = request.UserId ?? authorization.CurrentUserId;
            if (userId != authorization.CurrentUserId)
                  await authorization.EnsureSystemAdminAsync(cancellationToken);

            var memberships = await context.ChurchMemberRepository.GetByUserAsync(userId, cancellationToken);
            return memberships.Select(ChurchMemberDto.FromEntity).ToList();
      }
}

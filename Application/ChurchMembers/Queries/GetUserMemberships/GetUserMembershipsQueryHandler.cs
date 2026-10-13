using Application.ChurchMembers.Dtos;
using Application.Common.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Queries.GetUserMemberships;

public class GetUserMembershipsQueryHandler(IUnitOfWork context, ICurrentUserService currentUser)
    : IRequestHandler<GetUserMembershipsQuery, IReadOnlyList<ChurchMemberDto>>
{
      public async Task<IReadOnlyList<ChurchMemberDto>> Handle(GetUserMembershipsQuery request, CancellationToken cancellationToken)
      {
            var userId = request.UserId ?? currentUser.UserId
                ?? throw new UnauthorizedException("User is not authenticated.");

            var memberships = await context.ChurchMemberRepository.GetByUserAsync(userId, cancellationToken);
            return memberships.Select(ChurchMemberDto.FromEntity).ToList();
      }
}

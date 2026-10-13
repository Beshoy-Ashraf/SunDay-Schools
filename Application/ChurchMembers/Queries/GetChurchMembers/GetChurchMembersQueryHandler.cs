using Application.ChurchMembers.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Queries.GetChurchMembers;

public class GetChurchMembersQueryHandler(IUnitOfWork context) : IRequestHandler<GetChurchMembersQuery, IReadOnlyList<ChurchMemberDto>>
{
      public async Task<IReadOnlyList<ChurchMemberDto>> Handle(GetChurchMembersQuery request, CancellationToken cancellationToken)
      {
            await context.ChurchRepository.GetByIdAsync(request.ChurchId, cancellationToken);

            var members = await context.ChurchMemberRepository.GetActiveMembersAsync(request.ChurchId, cancellationToken);
            return members.Select(ChurchMemberDto.FromEntity).ToList();
      }
}

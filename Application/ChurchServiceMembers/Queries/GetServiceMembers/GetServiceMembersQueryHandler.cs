using Application.Common.Authorization;
using Application.ChurchServiceMembers.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceMembers.Queries.GetServiceMembers;

public class GetServiceMembersQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetServiceMembersQuery, IReadOnlyList<ChurchServiceMemberDto>>
{
      public async Task<IReadOnlyList<ChurchServiceMemberDto>> Handle(GetServiceMembersQuery request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.AnyMember, cancellationToken);

            var members = await context.ChurchServiceMemberRepository.GetByServiceAsync(request.ChurchServiceId, cancellationToken);
            return members.Select(ChurchServiceMemberDto.FromEntity).ToList();
      }
}

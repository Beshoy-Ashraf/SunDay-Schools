using Application.Common.Authorization;
using Application.ChurchMembers.Dtos;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Commands.ChangeMemberRole;

public class ChangeMemberRoleCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<ChangeMemberRoleCommand, ChurchMemberDto>
{
      public async Task<ChurchMemberDto> Handle(ChangeMemberRoleCommand request, CancellationToken cancellationToken)
      {
            var member = await context.ChurchMemberRepository.GetByIdAsync(request.Id, cancellationToken);

            var touchesAdmin = request.Role == ChurchMemberRole.Admin || member.Role == ChurchMemberRole.Admin;
            await authorization.EnsureChurchRoleAsync(member.ChurchId,
                  touchesAdmin ? ChurchRoles.AdminOnly : ChurchRoles.Managers, cancellationToken);

            member.ChangeRole(request.Role);
            await context.Complete(cancellationToken);

            return ChurchMemberDto.FromEntity(member);
      }
}

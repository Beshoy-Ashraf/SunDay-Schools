using Application.Common.Authorization;
using Application.ChurchMembers.Dtos;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Commands.LeaveChurch;

public class LeaveChurchCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<LeaveChurchCommand, ChurchMemberDto>
{
      public async Task<ChurchMemberDto> Handle(LeaveChurchCommand request, CancellationToken cancellationToken)
      {
            var member = await context.ChurchMemberRepository.GetByIdAsync(request.Id, cancellationToken);

            // Members may leave on their own; removing someone else needs a manager (or an Admin for an Admin).
            if (member.UserId != authorization.CurrentUserId)
                  await authorization.EnsureChurchRoleAsync(member.ChurchId,
                        member.Role == ChurchMemberRole.Admin ? ChurchRoles.AdminOnly : ChurchRoles.Managers, cancellationToken);

            member.Leave(request.DateLeft);
            await context.Complete(cancellationToken);

            return ChurchMemberDto.FromEntity(member);
      }
}

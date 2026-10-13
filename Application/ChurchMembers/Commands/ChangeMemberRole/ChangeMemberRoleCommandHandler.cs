using Application.ChurchMembers.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Commands.ChangeMemberRole;

public class ChangeMemberRoleCommandHandler(IUnitOfWork context) : IRequestHandler<ChangeMemberRoleCommand, ChurchMemberDto>
{
      public async Task<ChurchMemberDto> Handle(ChangeMemberRoleCommand request, CancellationToken cancellationToken)
      {
            var member = await context.ChurchMemberRepository.GetByIdAsync(request.Id, cancellationToken);

            member.ChangeRole(request.Role);
            await context.Complete(cancellationToken);

            return ChurchMemberDto.FromEntity(member);
      }
}

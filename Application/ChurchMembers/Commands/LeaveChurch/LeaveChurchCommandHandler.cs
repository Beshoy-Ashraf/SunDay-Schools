using Application.ChurchMembers.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Commands.LeaveChurch;

public class LeaveChurchCommandHandler(IUnitOfWork context) : IRequestHandler<LeaveChurchCommand, ChurchMemberDto>
{
      public async Task<ChurchMemberDto> Handle(LeaveChurchCommand request, CancellationToken cancellationToken)
      {
            var member = await context.ChurchMemberRepository.GetByIdAsync(request.Id, cancellationToken);

            member.Leave(request.DateLeft);
            await context.Complete(cancellationToken);

            return ChurchMemberDto.FromEntity(member);
      }
}

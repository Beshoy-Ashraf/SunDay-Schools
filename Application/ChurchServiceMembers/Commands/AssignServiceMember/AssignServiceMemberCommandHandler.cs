using Application.Common.Authorization;
using Application.ChurchServiceMembers.Dtos;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceMembers.Commands.AssignServiceMember;

public class AssignServiceMemberCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<AssignServiceMemberCommand, ChurchServiceMemberDto>
{
      public async Task<ChurchServiceMemberDto> Handle(AssignServiceMemberCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ServiceManagers, cancellationToken);

            var member = await context.ChurchMemberRepository.GetByIdAsync(request.ChurchMemberId, cancellationToken);

            if (await context.ChurchServiceMemberRepository.GetAssignmentAsync(request.ChurchServiceId, request.ChurchMemberId, cancellationToken) is not null)
                  throw new ConflictException("Member is already assigned to this service.");

            var assignment = service.AssignMember(member, request.Role);
            await context.ChurchServiceMemberRepository.AddAsync(assignment, cancellationToken);
            await context.Complete(cancellationToken);

            return ChurchServiceMemberDto.FromEntity(assignment);
      }
}

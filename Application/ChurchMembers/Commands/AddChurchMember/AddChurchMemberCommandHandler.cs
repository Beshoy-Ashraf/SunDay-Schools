using Application.ChurchMembers.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Commands.AddChurchMember;

public class AddChurchMemberCommandHandler(IUnitOfWork context) : IRequestHandler<AddChurchMemberCommand, ChurchMemberDto>
{
      public async Task<ChurchMemberDto> Handle(AddChurchMemberCommand request, CancellationToken cancellationToken)
      {
            await context.UserRepository.GetByIdAsync(request.UserId, cancellationToken);
            await context.ChurchRepository.GetByIdAsync(request.ChurchId, cancellationToken);

            if (await context.ChurchMemberRepository.GetActiveMembershipAsync(request.UserId, request.ChurchId, cancellationToken) is not null)
                  throw new ConflictException("User is already an active member of this church.");

            var member = new ChurchMember(request.UserId, request.ChurchId, request.Role,
                                          request.DateJoined ?? DateTime.UtcNow, request.Notes?.Trim());
            await context.ChurchMemberRepository.AddAsync(member, cancellationToken);
            await context.Complete(cancellationToken);

            return ChurchMemberDto.FromEntity(member);
      }
}

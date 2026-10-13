using Application.ChurchMembers.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchMembers.Queries.GetChurchMemberById;

public class GetChurchMemberByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetChurchMemberByIdQuery, ChurchMemberDto>
{
      public async Task<ChurchMemberDto> Handle(GetChurchMemberByIdQuery request, CancellationToken cancellationToken)
      {
            var member = await context.ChurchMemberRepository.FindAsync(
                  m => m.Id == request.Id, cancellationToken, ["User", "Church"]);
            return ChurchMemberDto.FromEntity(member);
      }
}

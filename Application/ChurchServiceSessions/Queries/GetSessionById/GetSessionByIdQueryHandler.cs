using Application.Common.Authorization;
using Application.ChurchServiceSessions.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Queries.GetSessionById;

public class GetSessionByIdQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<GetSessionByIdQuery, ChurchServiceSessionDto>
{
      public async Task<ChurchServiceSessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.FindAsync(
                  s => s.Id == request.Id, cancellationToken, ["ChurchService"]);
            await authorization.EnsureChurchRoleAsync(session.ChurchService.ChurchId, ChurchRoles.AnyMember, cancellationToken);
            return ChurchServiceSessionDto.FromEntity(session);
      }
}

using Application.Common.Authorization;
using Application.ChurchServiceSessions.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.RescheduleSession;

public class RescheduleSessionCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<RescheduleSessionCommand, ChurchServiceSessionDto>
{
      public async Task<ChurchServiceSessionDto> Handle(RescheduleSessionCommand request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.FindAsync(
                  s => s.Id == request.Id, cancellationToken, ["ChurchService"]);
            await authorization.EnsureChurchRoleAsync(session.ChurchService.ChurchId, ChurchRoles.SessionManagers, cancellationToken);

            session.Reschedule(request.StartTime, request.EndTime);
            await context.Complete(cancellationToken);

            return ChurchServiceSessionDto.FromEntity(session);
      }
}

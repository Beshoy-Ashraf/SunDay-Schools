using Application.ChurchServiceSessions.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.RescheduleSession;

public class RescheduleSessionCommandHandler(IUnitOfWork context) : IRequestHandler<RescheduleSessionCommand, ChurchServiceSessionDto>
{
      public async Task<ChurchServiceSessionDto> Handle(RescheduleSessionCommand request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.GetByIdAsync(request.Id, cancellationToken);

            session.Reschedule(request.StartTime, request.EndTime);
            await context.Complete(cancellationToken);

            return ChurchServiceSessionDto.FromEntity(session);
      }
}

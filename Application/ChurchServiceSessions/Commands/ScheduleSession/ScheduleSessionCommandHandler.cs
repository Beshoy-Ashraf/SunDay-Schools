using Application.ChurchServiceSessions.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Commands.ScheduleSession;

public class ScheduleSessionCommandHandler(IUnitOfWork context) : IRequestHandler<ScheduleSessionCommand, ChurchServiceSessionDto>
{
      public async Task<ChurchServiceSessionDto> Handle(ScheduleSessionCommand request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);

            var session = service.ScheduleSession(request.StartTime, request.EndTime);
            await context.ChurchServiceSessionRepository.AddAsync(session, cancellationToken);
            await context.Complete(cancellationToken);

            return ChurchServiceSessionDto.FromEntity(session);
      }
}

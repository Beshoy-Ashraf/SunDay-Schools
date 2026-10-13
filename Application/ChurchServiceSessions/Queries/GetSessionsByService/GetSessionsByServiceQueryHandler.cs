using Application.ChurchServiceSessions.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Queries.GetSessionsByService;

public class GetSessionsByServiceQueryHandler(IUnitOfWork context)
    : IRequestHandler<GetSessionsByServiceQuery, IReadOnlyList<ChurchServiceSessionDto>>
{
      public async Task<IReadOnlyList<ChurchServiceSessionDto>> Handle(GetSessionsByServiceQuery request, CancellationToken cancellationToken)
      {
            await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);

            var sessions = await context.ChurchServiceSessionRepository.GetByServiceAsync(
                  request.ChurchServiceId, request.From, request.To, cancellationToken);
            return sessions.Select(ChurchServiceSessionDto.FromEntity).ToList();
      }
}

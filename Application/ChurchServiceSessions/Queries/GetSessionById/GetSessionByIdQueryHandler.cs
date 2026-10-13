using Application.ChurchServiceSessions.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.ChurchServiceSessions.Queries.GetSessionById;

public class GetSessionByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetSessionByIdQuery, ChurchServiceSessionDto>
{
      public async Task<ChurchServiceSessionDto> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.GetByIdAsync(request.Id, cancellationToken);
            return ChurchServiceSessionDto.FromEntity(session);
      }
}

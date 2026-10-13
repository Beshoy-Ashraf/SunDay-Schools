using Application.Attendances.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetSessionAttendance;

public class GetSessionAttendanceQueryHandler(IUnitOfWork context)
    : IRequestHandler<GetSessionAttendanceQuery, IReadOnlyList<AttendanceDto>>
{
      public async Task<IReadOnlyList<AttendanceDto>> Handle(GetSessionAttendanceQuery request, CancellationToken cancellationToken)
      {
            await context.ChurchServiceSessionRepository.GetByIdAsync(request.SessionId, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetBySessionAsync(request.SessionId, cancellationToken);
            return attendances.Select(AttendanceDto.FromEntity).ToList();
      }
}

using Application.Common.Authorization;
using Application.Attendances.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetSessionAttendance;

public class GetSessionAttendanceQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetSessionAttendanceQuery, IReadOnlyList<AttendanceDto>>
{
      public async Task<IReadOnlyList<AttendanceDto>> Handle(GetSessionAttendanceQuery request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.FindAsync(
                  s => s.Id == request.SessionId, cancellationToken, ["ChurchService"]);
            await authorization.EnsureChurchRoleAsync(session.ChurchService.ChurchId, ChurchRoles.Servants, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetBySessionAsync(request.SessionId, cancellationToken);
            return attendances.Select(AttendanceDto.FromEntity).ToList();
      }
}

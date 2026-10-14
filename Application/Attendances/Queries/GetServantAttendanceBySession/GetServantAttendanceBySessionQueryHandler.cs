using Application.Attendances.Dtos;
using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetServantAttendanceBySession;

public class GetServantAttendanceBySessionQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetServantAttendanceBySessionQuery, IReadOnlyList<AttendanceDto>>
{
      public async Task<IReadOnlyList<AttendanceDto>> Handle(GetServantAttendanceBySessionQuery request, CancellationToken cancellationToken)
      {
            var session = await context.ChurchServiceSessionRepository.FindAsync(
                  s => s.Id == request.SessionId, cancellationToken, ["ChurchService"]);
            await authorization.EnsureChurchRoleAsync(session.ChurchService.ChurchId, ChurchRoles.ServantAttendanceViewers, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetServantsBySessionAsync(request.SessionId, cancellationToken);
            return attendances.Select(AttendanceDto.FromEntity).ToList();
      }
}

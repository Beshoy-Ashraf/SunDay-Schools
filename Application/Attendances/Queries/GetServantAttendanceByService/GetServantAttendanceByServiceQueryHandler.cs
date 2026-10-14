using Application.Attendances.Dtos;
using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetServantAttendanceByService;

public class GetServantAttendanceByServiceQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetServantAttendanceByServiceQuery, IReadOnlyList<AttendanceDto>>
{
      public async Task<IReadOnlyList<AttendanceDto>> Handle(GetServantAttendanceByServiceQuery request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.ServantAttendanceViewers, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetServantsByServiceAsync(
                  request.ChurchServiceId, request.From, request.To, cancellationToken);
            return attendances.Select(AttendanceDto.FromEntity).ToList();
      }
}

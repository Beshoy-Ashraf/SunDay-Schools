using Application.Attendances.Dtos;
using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetServiceAttendance;

public class GetServiceAttendanceQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetServiceAttendanceQuery, IReadOnlyList<AttendanceDto>>
{
      public async Task<IReadOnlyList<AttendanceDto>> Handle(GetServiceAttendanceQuery request, CancellationToken cancellationToken)
      {
            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);
            await authorization.EnsureChurchRoleAsync(service.ChurchId, ChurchRoles.Servants, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetByServiceAsync(
                  request.ChurchServiceId, request.From, request.To, cancellationToken);
            return attendances.Select(AttendanceDto.FromEntity).ToList();
      }
}

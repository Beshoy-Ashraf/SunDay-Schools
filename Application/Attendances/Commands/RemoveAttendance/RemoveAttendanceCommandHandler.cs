using Application.Common.Authorization;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Commands.RemoveAttendance;

public class RemoveAttendanceCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<RemoveAttendanceCommand>
{
      public async Task Handle(RemoveAttendanceCommand request, CancellationToken cancellationToken)
      {
            var attendance = await context.ChurchServiceAttendanceRepository.FindAsync(
                  a => a.Id == request.Id, cancellationToken, ["ChurchServiceSession.ChurchService"]);

            // Same rule as recording: only a servant of the church may change attendance.
            await authorization.EnsureChurchRoleAsync(
                  attendance.ChurchServiceSession.ChurchService.ChurchId, ChurchRoles.Servants, cancellationToken);

            attendance.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

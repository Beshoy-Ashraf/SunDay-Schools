using Application.Common.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Commands.RemoveAttendance;

public class RemoveAttendanceCommandHandler(IUnitOfWork context, ICurrentUserService currentUser)
    : IRequestHandler<RemoveAttendanceCommand>
{
      public async Task Handle(RemoveAttendanceCommand request, CancellationToken cancellationToken)
      {
            var userId = currentUser.UserId ?? throw new UnauthorizedException("User is not authenticated.");

            var attendance = await context.ChurchServiceAttendanceRepository.FindAsync(
                  a => a.Id == request.Id, cancellationToken, ["ChurchServiceSession.ChurchService"]);

            // Same rule as recording: only an active servant of the church may change attendance.
            var churchId = attendance.ChurchServiceSession.ChurchService.ChurchId;
            var remover = await context.ChurchMemberRepository.GetActiveMembershipAsync(userId, churchId, cancellationToken);
            if (remover is null || remover.Role == ChurchMemberRole.Member)
                  throw new ForbiddenException("Only servants of this church can remove attendance.");

            attendance.SoftDelete();
            await context.Complete(cancellationToken);
      }
}

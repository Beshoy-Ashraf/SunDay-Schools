using Application.Attendances.Dtos;
using Application.Common.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Commands.RecordAttendance;

public class RecordAttendanceCommandHandler(IUnitOfWork context, ICurrentUserService currentUser)
    : IRequestHandler<RecordAttendanceCommand, AttendanceDto>
{
      public async Task<AttendanceDto> Handle(RecordAttendanceCommand request, CancellationToken cancellationToken)
      {
            var userId = currentUser.UserId ?? throw new UnauthorizedException("User is not authenticated.");

            var service = await context.ChurchServiceRepository.GetByIdAsync(request.ChurchServiceId, cancellationToken);

            var member = await context.ChurchMemberRepository.FindAsync(
                  m => m.Id == request.ChurchMemberId, cancellationToken, ["User"]);

            var recorder = await context.ChurchMemberRepository.GetActiveMembershipAsync(userId, service.ChurchId, cancellationToken)
                ?? throw new ForbiddenException("You are not an active member of this church.");

            var attendedAt = request.AttendedAt ?? DateTime.UtcNow;

            var alreadyRecorded = await context.ChurchServiceAttendanceRepository.ExistsForDayAsync(
                  request.ChurchServiceId, request.ChurchMemberId, attendedAt, cancellationToken);
            if (alreadyRecorded)
                  throw new ConflictException("Attendance is already recorded for this member on this day.");

            var attendance = service.RecordAttendance(member, recorder, attendedAt);
            await context.ChurchServiceAttendanceRepository.AddAsync(attendance, cancellationToken);
            await context.Complete(cancellationToken);

            return AttendanceDto.FromEntity(attendance);
      }
}

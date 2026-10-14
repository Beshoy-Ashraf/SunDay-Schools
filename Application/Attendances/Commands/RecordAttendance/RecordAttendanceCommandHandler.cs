using Application.Attendances.Dtos;
using Application.Common.Interfaces;
using Domain.Enums;
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

            var session = await context.ChurchServiceSessionRepository.GetForAttendanceAsync(request.SessionId, cancellationToken)
                ?? throw new NotFoundException("Session", request.SessionId);

            var member = await context.ChurchMemberRepository.FindAsync(
                  m => m.Id == request.ChurchMemberId, cancellationToken, ["User"]);

            var recorder = await context.ChurchMemberRepository.GetActiveMembershipAsync(userId, session.ChurchService.ChurchId, cancellationToken)
                ?? throw new ForbiddenException("You are not an active member of this church.");

            // Domain enforces: same church, both active, recorder is a servant, no duplicate.
            var attendance = session.RecordAttendance(member, recorder, request.AttendedAt);
            await context.ChurchServiceAttendanceRepository.AddAsync(attendance, cancellationToken);

            if (request.ActivityAnswers is { Count: > 0 })
            {
                  var activityTypes = await context.ActivityTypeRepository.GetByServiceAsync(
                        session.ChurchServiceId, cancellationToken);

                  var servantActivityTypes = activityTypes
                        .Where(a => a.IsActive && a.AppliesTo == ActivityAppliesTo.Servant)
                        .ToDictionary(a => a.Id);

                  foreach (var answer in request.ActivityAnswers)
                  {
                        if (!servantActivityTypes.TryGetValue(answer.ActivityTypeId, out var activityType))
                              throw new BadRequestException(
                                    $"Activity type {answer.ActivityTypeId} is not an active servant activity for this service.");

                        var record = attendance.RecordActivity(activityType, answer.Answered);
                        await context.ChurchServiceAttendanceRepository.AddActivityRecordAsync(record, cancellationToken);
                  }
            }

            await context.Complete(cancellationToken);

            return AttendanceDto.FromEntity(attendance);
      }
}

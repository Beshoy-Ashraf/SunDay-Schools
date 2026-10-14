using Application.Common.Authorization;
using Application.Attendances.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetMemberAttendance;

public class GetMemberAttendanceQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetMemberAttendanceQuery, IReadOnlyList<MemberAttendanceDto>>
{
      public async Task<IReadOnlyList<MemberAttendanceDto>> Handle(GetMemberAttendanceQuery request, CancellationToken cancellationToken)
      {
            var member = await context.ChurchMemberRepository.GetByIdAsync(request.ChurchMemberId, cancellationToken);

            // Members can see their own history; servants can see anyone's in their church.
            if (member.UserId != authorization.CurrentUserId)
                  await authorization.EnsureChurchRoleAsync(member.ChurchId, ChurchRoles.Servants, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetByMemberAsync(
                  request.ChurchMemberId, request.From, request.To, cancellationToken);
            return attendances.Select(MemberAttendanceDto.FromEntity).ToList();
      }
}

using Application.Attendances.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Attendances.Queries.GetMemberAttendance;

public class GetMemberAttendanceQueryHandler(IUnitOfWork context)
    : IRequestHandler<GetMemberAttendanceQuery, IReadOnlyList<MemberAttendanceDto>>
{
      public async Task<IReadOnlyList<MemberAttendanceDto>> Handle(GetMemberAttendanceQuery request, CancellationToken cancellationToken)
      {
            await context.ChurchMemberRepository.GetByIdAsync(request.ChurchMemberId, cancellationToken);

            var attendances = await context.ChurchServiceAttendanceRepository.GetByMemberAsync(
                  request.ChurchMemberId, request.From, request.To, cancellationToken);
            return attendances.Select(MemberAttendanceDto.FromEntity).ToList();
      }
}

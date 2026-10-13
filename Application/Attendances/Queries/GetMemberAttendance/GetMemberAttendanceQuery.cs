using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Queries.GetMemberAttendance;

public record GetMemberAttendanceQuery(Guid ChurchMemberId, DateTime? From, DateTime? To) : IRequest<IReadOnlyList<MemberAttendanceDto>>;

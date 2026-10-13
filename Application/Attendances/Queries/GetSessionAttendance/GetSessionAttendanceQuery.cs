using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Queries.GetSessionAttendance;

public record GetSessionAttendanceQuery(Guid SessionId) : IRequest<IReadOnlyList<AttendanceDto>>;

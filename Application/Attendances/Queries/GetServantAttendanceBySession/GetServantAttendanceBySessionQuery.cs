using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Queries.GetServantAttendanceBySession;

public record GetServantAttendanceBySessionQuery(Guid SessionId) : IRequest<IReadOnlyList<AttendanceDto>>;

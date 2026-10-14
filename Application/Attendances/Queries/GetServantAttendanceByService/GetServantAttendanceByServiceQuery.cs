using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Queries.GetServantAttendanceByService;

public record GetServantAttendanceByServiceQuery(Guid ChurchServiceId, DateTime? From, DateTime? To) : IRequest<IReadOnlyList<AttendanceDto>>;

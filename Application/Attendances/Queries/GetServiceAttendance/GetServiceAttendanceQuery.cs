using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Queries.GetServiceAttendance;

public record GetServiceAttendanceQuery(Guid ChurchServiceId, DateTime? From, DateTime? To) : IRequest<IReadOnlyList<AttendanceDto>>;

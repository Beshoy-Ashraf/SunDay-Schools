using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Commands.RecordAttendance;

public record RecordAttendanceCommand(Guid ChurchServiceId, Guid ChurchMemberId, DateTime? AttendedAt) : IRequest<AttendanceDto>;

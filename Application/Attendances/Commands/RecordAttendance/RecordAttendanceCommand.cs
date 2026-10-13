using Application.Attendances.Dtos;
using MediatR;

namespace Application.Attendances.Commands.RecordAttendance;

/// <summary>The current user is recorded as the servant who took attendance.</summary>
public record RecordAttendanceCommand(Guid SessionId, Guid ChurchMemberId, DateTime? AttendedAt) : IRequest<AttendanceDto>;

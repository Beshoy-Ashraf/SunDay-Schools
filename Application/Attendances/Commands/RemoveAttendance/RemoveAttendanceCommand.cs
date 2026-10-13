using MediatR;

namespace Application.Attendances.Commands.RemoveAttendance;

public record RemoveAttendanceCommand(Guid Id) : IRequest;

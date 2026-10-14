using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchServiceAttendanceRepository : IBaseRepository<ChurchServiceAttendance>
{
      Task<IReadOnlyList<ChurchServiceAttendance>> GetByServiceAsync(Guid churchServiceId, DateTime? from, DateTime? to, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchServiceAttendance>> GetServantsByServiceAsync(Guid churchServiceId, DateTime? from, DateTime? to, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchServiceAttendance>> GetByMemberAsync(Guid churchMemberId, DateTime? from, DateTime? to, CancellationToken cancellationToken);
      Task<bool> ExistsForDayAsync(Guid churchServiceId, Guid churchMemberId, DateTime date, CancellationToken cancellationToken);
}

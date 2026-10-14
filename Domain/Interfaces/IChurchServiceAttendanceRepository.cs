using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchServiceAttendanceRepository : IBaseRepository<ChurchServiceAttendance>
{
      Task<IReadOnlyList<ChurchServiceAttendance>> GetBySessionAsync(Guid sessionId, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchServiceAttendance>> GetServantsBySessionAsync(Guid sessionId, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchServiceAttendance>> GetByMemberAsync(Guid churchMemberId, DateTime? from, DateTime? to, CancellationToken cancellationToken);
      Task AddActivityRecordAsync(ServantActivityRecord record, CancellationToken cancellationToken);
}

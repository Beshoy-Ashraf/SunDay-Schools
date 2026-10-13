using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchServiceSessionRepository : IBaseRepository<ChurchServiceSession>
{
      /// <summary>Loads the session with what <see cref="ChurchServiceSession.RecordAttendance"/> needs.</summary>
      Task<ChurchServiceSession?> GetForAttendanceAsync(Guid sessionId, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchServiceSession>> GetByServiceAsync(Guid churchServiceId, DateTime? from, DateTime? to, CancellationToken cancellationToken);
}

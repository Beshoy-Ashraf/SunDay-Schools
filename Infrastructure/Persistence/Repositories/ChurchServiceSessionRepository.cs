using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchServiceSessionRepository(AppDbContext context) : BaseRepository<ChurchServiceSession>(context), IChurchServiceSessionRepository
{
      public async Task<ChurchServiceSession?> GetForAttendanceAsync(Guid sessionId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchServiceSessions
                  .Include(s => s.ChurchService)
                  .Include(s => s.Attendances)
                  .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchServiceSession>> GetByServiceAsync(Guid churchServiceId, DateTime? from, DateTime? to, CancellationToken cancellationToken)
      {
            var query = _dbContext.ChurchServiceSessions.Where(s => s.ChurchServiceId == churchServiceId);

            if (from is not null)
                  query = query.Where(s => s.StartTime >= from);
            if (to is not null)
                  query = query.Where(s => s.StartTime < to);

            return await query.OrderBy(s => s.StartTime).ToListAsync(cancellationToken);
      }
}

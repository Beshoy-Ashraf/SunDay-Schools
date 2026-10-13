using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchServiceAttendanceRepository(AppDbContext context) : BaseRepository<ChurchServiceAttendance>(context), IChurchServiceAttendanceRepository
{
      public async Task<IReadOnlyList<ChurchServiceAttendance>> GetBySessionAsync(Guid sessionId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchServiceAttendances
                  .Include(a => a.ChurchMember).ThenInclude(m => m.User)
                  .Where(a => a.ChurchServiceSessionId == sessionId)
                  .OrderBy(a => a.AttendedAt)
                  .ToListAsync(cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchServiceAttendance>> GetByMemberAsync(Guid churchMemberId, DateTime? from, DateTime? to, CancellationToken cancellationToken)
      {
            var query = _dbContext.ChurchServiceAttendances
                  .Include(a => a.ChurchServiceSession).ThenInclude(s => s.ChurchService)
                  .Where(a => a.ChurchMemberId == churchMemberId);

            if (from is not null)
                  query = query.Where(a => a.ChurchServiceSession.StartTime >= from);
            if (to is not null)
                  query = query.Where(a => a.ChurchServiceSession.StartTime < to);

            return await query.OrderByDescending(a => a.ChurchServiceSession.StartTime).ToListAsync(cancellationToken);
      }
}

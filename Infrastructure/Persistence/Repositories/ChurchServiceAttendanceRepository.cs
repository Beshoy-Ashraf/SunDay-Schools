using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchServiceAttendanceRepository(AppDbContext context) : BaseRepository<ChurchServiceAttendance>(context), IChurchServiceAttendanceRepository
{
      public async Task<IReadOnlyList<ChurchServiceAttendance>> GetByServiceAsync(Guid churchServiceId, DateTime? from, DateTime? to, CancellationToken cancellationToken)
      {
            var query = _dbContext.ChurchServiceAttendances
                  .Include(a => a.ChurchService)
                  .Include(a => a.ChurchMember).ThenInclude(m => m.User)
                  .Where(a => a.ChurchServiceId == churchServiceId);

            if (from is not null)
                  query = query.Where(a => a.AttendedAt >= from);
            if (to is not null)
                  query = query.Where(a => a.AttendedAt < to);

            return await query.OrderByDescending(a => a.AttendedAt).ToListAsync(cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchServiceAttendance>> GetServantsByServiceAsync(Guid churchServiceId, DateTime? from, DateTime? to, CancellationToken cancellationToken)
      {
            var query = _dbContext.ChurchServiceAttendances
                  .Include(a => a.ChurchService)
                  .Include(a => a.ChurchMember).ThenInclude(m => m.User)
                  .Where(a => a.ChurchServiceId == churchServiceId && a.ChurchMember.Role != ChurchMemberRole.Member);

            if (from is not null)
                  query = query.Where(a => a.AttendedAt >= from);
            if (to is not null)
                  query = query.Where(a => a.AttendedAt < to);

            return await query.OrderByDescending(a => a.AttendedAt).ToListAsync(cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchServiceAttendance>> GetByMemberAsync(Guid churchMemberId, DateTime? from, DateTime? to, CancellationToken cancellationToken)
      {
            var query = _dbContext.ChurchServiceAttendances
                  .Include(a => a.ChurchService)
                  .Where(a => a.ChurchMemberId == churchMemberId);

            if (from is not null)
                  query = query.Where(a => a.AttendedAt >= from);
            if (to is not null)
                  query = query.Where(a => a.AttendedAt < to);

            return await query.OrderByDescending(a => a.AttendedAt).ToListAsync(cancellationToken);
      }

      public async Task<bool> ExistsForDayAsync(Guid churchServiceId, Guid churchMemberId, DateTime date, CancellationToken cancellationToken)
      {
            var dayStart = date.Date;
            var dayEnd = dayStart.AddDays(1);
            return await _dbContext.ChurchServiceAttendances.AnyAsync(
                  a => a.ChurchServiceId == churchServiceId
                    && a.ChurchMemberId == churchMemberId
                    && a.AttendedAt >= dayStart
                    && a.AttendedAt < dayEnd,
                  cancellationToken);
      }
}

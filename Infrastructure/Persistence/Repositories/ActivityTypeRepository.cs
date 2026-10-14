using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ActivityTypeRepository(AppDbContext context) : BaseRepository<ActivityType>(context), IActivityTypeRepository
{
      public async Task<IReadOnlyList<ActivityType>> GetByServiceAsync(Guid churchServiceId, CancellationToken cancellationToken)
      {
            return await _dbContext.ActivityTypes
                  .Where(a => a.ChurchServiceId == churchServiceId)
                  .OrderBy(a => a.Code)
                  .ToListAsync(cancellationToken);
      }

      public async Task<ActivityType?> GetByServiceAndCodeAsync(Guid churchServiceId, ActivityTypeCode code, CancellationToken cancellationToken)
      {
            return await _dbContext.ActivityTypes
                  .FirstOrDefaultAsync(a => a.ChurchServiceId == churchServiceId && a.Code == code, cancellationToken);
      }
}

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchServiceRepository(AppDbContext context) : BaseRepository<ChurchService>(context), IChurchServiceRepository
{
      public async Task<IReadOnlyList<ChurchService>> GetByChurchAsync(Guid churchId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchServices
                  .Where(s => s.ChurchId == churchId)
                  .OrderBy(s => s.Name)
                  .ToListAsync(cancellationToken);
      }
}

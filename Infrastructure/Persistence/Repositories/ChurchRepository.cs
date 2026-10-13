using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchRepository(AppDbContext context) : BaseRepository<Church>(context), IChurchRepository
{
      public async Task<IReadOnlyList<Church>> GetByEparchyAsync(Guid eparchyId, CancellationToken cancellationToken)
      {
            return await _dbContext.Churches
                  .Where(c => c.EparchyId == eparchyId)
                  .OrderBy(c => c.Name)
                  .ToListAsync(cancellationToken);
      }

      public async Task<bool> NameExistsInEparchyAsync(Guid eparchyId, string name, CancellationToken cancellationToken)
      {
            name = name.Trim();
            return await _dbContext.Churches.AnyAsync(c => c.EparchyId == eparchyId && c.Name == name, cancellationToken);
      }
}

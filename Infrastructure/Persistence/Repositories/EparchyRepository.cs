using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EparchyRepository(AppDbContext context) : BaseRepository<Eparchy>(context), IEparchyRepository
{
      public async Task<Eparchy?> GetByNameAsync(string name, CancellationToken cancellationToken)
      {
            name = name.Trim();
            return await _dbContext.Eparchies.FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
      }

      public async Task<Eparchy?> GetWithChurchesAsync(Guid id, CancellationToken cancellationToken)
      {
            return await _dbContext.Eparchies
                  .Include(e => e.Churches)
                  .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
      }
}

using Domain.Entities;

namespace Domain.Interfaces;

public interface IEparchyRepository : IBaseRepository<Eparchy>
{
      Task<Eparchy?> GetByNameAsync(string name, CancellationToken cancellationToken);
      Task<Eparchy?> GetWithChurchesAsync(Guid id, CancellationToken cancellationToken);
}

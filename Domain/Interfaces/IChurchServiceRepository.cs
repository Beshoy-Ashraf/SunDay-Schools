using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchServiceRepository : IBaseRepository<ChurchService>
{
      Task<IReadOnlyList<ChurchService>> GetByChurchAsync(Guid churchId, CancellationToken cancellationToken);
}

using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchRepository : IBaseRepository<Church>
{
      Task<IReadOnlyList<Church>> GetByEparchyAsync(Guid eparchyId, CancellationToken cancellationToken);
      Task<bool> NameExistsInEparchyAsync(Guid eparchyId, string name, CancellationToken cancellationToken);
}

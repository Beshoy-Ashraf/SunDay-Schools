using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces;

public interface IActivityTypeRepository : IBaseRepository<ActivityType>
{
      Task<IReadOnlyList<ActivityType>> GetByServiceAsync(Guid churchServiceId, CancellationToken cancellationToken);
      Task<ActivityType?> GetByServiceAndCodeAsync(Guid churchServiceId, ActivityTypeCode code, CancellationToken cancellationToken);
}

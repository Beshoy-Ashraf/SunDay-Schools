using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchServiceMemberRepository : IBaseRepository<ChurchServiceMember>
{
      Task<ChurchServiceMember?> GetAssignmentAsync(Guid churchServiceId, Guid churchMemberId, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchServiceMember>> GetByServiceAsync(Guid churchServiceId, CancellationToken cancellationToken);
}

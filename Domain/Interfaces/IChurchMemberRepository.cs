using Domain.Entities;

namespace Domain.Interfaces;

public interface IChurchMemberRepository : IBaseRepository<ChurchMember>
{
      Task<ChurchMember?> GetActiveMembershipAsync(Guid userId, Guid churchId, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchMember>> GetActiveMembersAsync(Guid churchId, CancellationToken cancellationToken);
      Task<IReadOnlyList<ChurchMember>> GetByUserAsync(Guid userId, CancellationToken cancellationToken);
}

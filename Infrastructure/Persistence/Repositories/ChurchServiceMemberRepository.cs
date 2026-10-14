using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchServiceMemberRepository(AppDbContext context) : BaseRepository<ChurchServiceMember>(context), IChurchServiceMemberRepository
{
      public async Task<ChurchServiceMember?> GetAssignmentAsync(Guid churchServiceId, Guid churchMemberId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchServiceMembers
                  .FirstOrDefaultAsync(sm => sm.ChurchServiceId == churchServiceId && sm.ChurchMemberId == churchMemberId, cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchServiceMember>> GetByServiceAsync(Guid churchServiceId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchServiceMembers
                  .Include(sm => sm.ChurchMember)
                        .ThenInclude(m => m.User)
                  .Where(sm => sm.ChurchServiceId == churchServiceId)
                  .OrderBy(sm => sm.Role)
                  .ToListAsync(cancellationToken);
      }
}

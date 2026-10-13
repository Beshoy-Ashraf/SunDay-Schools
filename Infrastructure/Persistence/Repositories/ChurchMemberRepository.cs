using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ChurchMemberRepository(AppDbContext context) : BaseRepository<ChurchMember>(context), IChurchMemberRepository
{
      public async Task<ChurchMember?> GetActiveMembershipAsync(Guid userId, Guid churchId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchMembers
                  .FirstOrDefaultAsync(m => m.UserId == userId && m.ChurchId == churchId && m.DateLeft == null, cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchMember>> GetActiveMembersAsync(Guid churchId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchMembers
                  .Include(m => m.User)
                  .Where(m => m.ChurchId == churchId && m.DateLeft == null)
                  .OrderBy(m => m.User.DisplayName)
                  .ToListAsync(cancellationToken);
      }

      public async Task<IReadOnlyList<ChurchMember>> GetByUserAsync(Guid userId, CancellationToken cancellationToken)
      {
            return await _dbContext.ChurchMembers
                  .Include(m => m.Church)
                  .Where(m => m.UserId == userId)
                  .OrderByDescending(m => m.DateJoined)
                  .ToListAsync(cancellationToken);
      }
}

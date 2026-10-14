using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
      private readonly AppDbContext _dbContext;

      public IBaseRepository<User> Users { get; private set; } = null!;
      public IUserRepository UserRepository { get; private set; }
      public IEparchyRepository EparchyRepository { get; private set; }
      public IChurchRepository ChurchRepository { get; private set; }
      public IChurchMemberRepository ChurchMemberRepository { get; private set; }
      public IChurchServiceRepository ChurchServiceRepository { get; private set; }
      public IChurchServiceMemberRepository ChurchServiceMemberRepository { get; private set; }
      public IChurchServiceAttendanceRepository ChurchServiceAttendanceRepository { get; private set; }

      public UnitOfWork(AppDbContext dBContext)
      {
            _dbContext = dBContext;
            Users = new BaseRepository<User>(_dbContext);
            UserRepository = new UserRepository(_dbContext);
            EparchyRepository = new EparchyRepository(_dbContext);
            ChurchRepository = new ChurchRepository(_dbContext);
            ChurchMemberRepository = new ChurchMemberRepository(_dbContext);
            ChurchServiceRepository = new ChurchServiceRepository(_dbContext);
            ChurchServiceMemberRepository = new ChurchServiceMemberRepository(_dbContext);
            ChurchServiceAttendanceRepository = new ChurchServiceAttendanceRepository(_dbContext);
      }

      public async Task<int> Complete(CancellationToken cancellationToken)
      {
            return await _dbContext.SaveChangesAsync(cancellationToken);
      }

      public void Dispose()
      {
            _dbContext.Dispose();
      }
}

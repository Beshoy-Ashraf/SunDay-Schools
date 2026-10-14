using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
      IBaseRepository<User> Users { get; }
      IUserRepository UserRepository { get; }
      IEparchyRepository EparchyRepository { get; }
      IChurchRepository ChurchRepository { get; }
      IChurchMemberRepository ChurchMemberRepository { get; }
      IChurchServiceRepository ChurchServiceRepository { get; }
      IChurchServiceMemberRepository ChurchServiceMemberRepository { get; }
      IChurchServiceAttendanceRepository ChurchServiceAttendanceRepository { get; }
      Task<int> Complete(CancellationToken cancellationToken);
}

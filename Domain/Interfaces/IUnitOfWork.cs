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
      IChurchServiceSessionRepository ChurchServiceSessionRepository { get; }
      IChurchServiceMemberRepository ChurchServiceMemberRepository { get; }
      IActivityTypeRepository ActivityTypeRepository { get; }
      IChurchServiceAttendanceRepository ChurchServiceAttendanceRepository { get; }
      Task<int> Complete(CancellationToken cancellationToken);
}

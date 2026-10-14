using Application.Common.Authorization;
using Application.Users.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUnitOfWork context, IChurchAuthorizationService authorization)
    : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserDto>>
{
      public async Task<IReadOnlyList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            var users = await context.UserRepository.GetAllAsync(u => u.DeletedAt == null, cancellationToken);
            return users.Select(u => new UserDto(u.Id, u.Username, u.Email, u.DisplayName, u.NationalId, u.ParentId, u.Role.ToString()))
                        .ToList();
      }
}

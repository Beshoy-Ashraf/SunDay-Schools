using Application.Churches.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Churches.Queries.GetChurchesByEparchy;

public class GetChurchesByEparchyQueryHandler(IUnitOfWork context) : IRequestHandler<GetChurchesByEparchyQuery, IReadOnlyList<ChurchDto>>
{
      public async Task<IReadOnlyList<ChurchDto>> Handle(GetChurchesByEparchyQuery request, CancellationToken cancellationToken)
      {
            await context.EparchyRepository.GetByIdAsync(request.EparchyId, cancellationToken);

            var churches = await context.ChurchRepository.GetByEparchyAsync(request.EparchyId, cancellationToken);
            return churches.Select(ChurchDto.FromEntity).ToList();
      }
}

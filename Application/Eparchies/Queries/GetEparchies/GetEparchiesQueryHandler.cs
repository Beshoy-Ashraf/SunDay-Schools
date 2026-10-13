using Application.Eparchies.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Eparchies.Queries.GetEparchies;

public class GetEparchiesQueryHandler(IUnitOfWork context) : IRequestHandler<GetEparchiesQuery, IReadOnlyList<EparchyDto>>
{
      public async Task<IReadOnlyList<EparchyDto>> Handle(GetEparchiesQuery request, CancellationToken cancellationToken)
      {
            var eparchies = await context.EparchyRepository.GetAllAsync(_ => true, cancellationToken);
            return eparchies.OrderBy(e => e.Name).Select(EparchyDto.FromEntity).ToList();
      }
}

using Application.Eparchies.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Eparchies.Queries.GetEparchyById;

public class GetEparchyByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetEparchyByIdQuery, EparchyDto>
{
      public async Task<EparchyDto> Handle(GetEparchyByIdQuery request, CancellationToken cancellationToken)
      {
            var eparchy = await context.EparchyRepository.GetByIdAsync(request.Id, cancellationToken);
            return EparchyDto.FromEntity(eparchy);
      }
}

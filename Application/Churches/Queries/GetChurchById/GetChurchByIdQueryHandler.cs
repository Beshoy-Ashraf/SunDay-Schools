using Application.Churches.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.Churches.Queries.GetChurchById;

public class GetChurchByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetChurchByIdQuery, ChurchDto>
{
      public async Task<ChurchDto> Handle(GetChurchByIdQuery request, CancellationToken cancellationToken)
      {
            var church = await context.ChurchRepository.GetByIdAsync(request.Id, cancellationToken);
            return ChurchDto.FromEntity(church);
      }
}

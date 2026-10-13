using Application.Churches.Dtos;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Churches.Commands.UpdateChurch;

public class UpdateChurchCommandHandler(IUnitOfWork context) : IRequestHandler<UpdateChurchCommand, ChurchDto>
{
      public async Task<ChurchDto> Handle(UpdateChurchCommand request, CancellationToken cancellationToken)
      {
            var church = await context.ChurchRepository.GetByIdAsync(request.Id, cancellationToken);

            var nameChanged = !string.Equals(church.Name, request.Name.Trim(), StringComparison.Ordinal);
            if (nameChanged && await context.ChurchRepository.NameExistsInEparchyAsync(church.EparchyId, request.Name, cancellationToken))
                  throw new ConflictException($"Church '{request.Name.Trim()}' already exists in this eparchy.");

            church.UpdateDetails(request.Name, request.Address, request.PhoneNumber, request.Email);
            await context.Complete(cancellationToken);

            return ChurchDto.FromEntity(church);
      }
}

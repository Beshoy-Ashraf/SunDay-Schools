using Application.Churches.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Churches.Commands.CreateChurch;

public class CreateChurchCommandHandler(IUnitOfWork context) : IRequestHandler<CreateChurchCommand, ChurchDto>
{
      public async Task<ChurchDto> Handle(CreateChurchCommand request, CancellationToken cancellationToken)
      {
            await context.EparchyRepository.GetByIdAsync(request.EparchyId, cancellationToken);

            if (await context.ChurchRepository.NameExistsInEparchyAsync(request.EparchyId, request.Name, cancellationToken))
                  throw new ConflictException($"Church '{request.Name.Trim()}' already exists in this eparchy.");

            var church = new Church(request.Name, request.Address, request.PhoneNumber, request.Email, request.EparchyId);
            await context.ChurchRepository.AddAsync(church, cancellationToken);
            await context.Complete(cancellationToken);

            return ChurchDto.FromEntity(church);
      }
}

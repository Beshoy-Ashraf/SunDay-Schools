using Application.Common.Authorization;
using Application.Eparchies.Dtos;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Eparchies.Commands.CreateEparchy;

public class CreateEparchyCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<CreateEparchyCommand, EparchyDto>
{
      public async Task<EparchyDto> Handle(CreateEparchyCommand request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            if (await context.EparchyRepository.GetByNameAsync(request.Name, cancellationToken) is not null)
                  throw new ConflictException($"Eparchy '{request.Name.Trim()}' already exists.");

            var eparchy = new Eparchy(request.Name);
            await context.EparchyRepository.AddAsync(eparchy, cancellationToken);
            await context.Complete(cancellationToken);

            return EparchyDto.FromEntity(eparchy);
      }
}

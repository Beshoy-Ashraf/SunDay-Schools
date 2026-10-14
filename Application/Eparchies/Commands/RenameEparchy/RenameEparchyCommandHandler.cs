using Application.Common.Authorization;
using Application.Eparchies.Dtos;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Eparchies.Commands.RenameEparchy;

public class RenameEparchyCommandHandler(IUnitOfWork context, IChurchAuthorizationService authorization) : IRequestHandler<RenameEparchyCommand, EparchyDto>
{
      public async Task<EparchyDto> Handle(RenameEparchyCommand request, CancellationToken cancellationToken)
      {
            await authorization.EnsureSystemAdminAsync(cancellationToken);

            var eparchy = await context.EparchyRepository.GetByIdAsync(request.Id, cancellationToken);

            var existing = await context.EparchyRepository.GetByNameAsync(request.Name, cancellationToken);
            if (existing is not null && existing.Id != eparchy.Id)
                  throw new ConflictException($"Eparchy '{request.Name.Trim()}' already exists.");

            eparchy.Rename(request.Name);
            await context.Complete(cancellationToken);

            return EparchyDto.FromEntity(eparchy);
      }
}

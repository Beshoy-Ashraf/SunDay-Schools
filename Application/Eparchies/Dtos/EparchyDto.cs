using Domain.Entities;

namespace Application.Eparchies.Dtos;

public record EparchyDto(Guid Id, string Name, DateTime CreatedAt)
{
      public static EparchyDto FromEntity(Eparchy eparchy) => new(eparchy.Id, eparchy.Name, eparchy.CreatedAt);
}

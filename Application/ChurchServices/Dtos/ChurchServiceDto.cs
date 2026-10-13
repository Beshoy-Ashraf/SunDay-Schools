using Domain.Entities;

namespace Application.ChurchServices.Dtos;

public record ChurchServiceDto(Guid Id, Guid ChurchId, string Name, string? Description, DateTime StartTime, DateTime EndTime)
{
      public static ChurchServiceDto FromEntity(ChurchService service) =>
            new(service.Id, service.ChurchId, service.Name, service.Description, service.StartTime, service.EndTime);
}

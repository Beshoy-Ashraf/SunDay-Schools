using Domain.Entities;

namespace Application.Churches.Dtos;

public record ChurchDto(Guid Id, string Name, string Address, string PhoneNumber, string Email, Guid EparchyId, DateTime CreatedAt)
{
      public static ChurchDto FromEntity(Church church) =>
            new(church.Id, church.Name, church.Address, church.PhoneNumber, church.Email, church.EparchyId, church.CreatedAt);
}

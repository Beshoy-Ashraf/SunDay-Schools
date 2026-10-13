using Domain.Entities;

namespace Application.ChurchServiceSessions.Dtos;

public record ChurchServiceSessionDto(Guid Id, Guid ChurchServiceId, DateTime StartTime, DateTime EndTime)
{
      public static ChurchServiceSessionDto FromEntity(ChurchServiceSession session) =>
            new(session.Id, session.ChurchServiceId, session.StartTime, session.EndTime);
}

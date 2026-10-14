using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class ChurchServiceMember : BaseEntity
{
      public Guid ChurchServiceId { get; private set; }
      public ChurchService ChurchService { get; private set; } = null!;

      public Guid ChurchMemberId { get; private set; }
      public ChurchMember ChurchMember { get; private set; } = null!;

      public ChurchServiceRole Role { get; private set; }

      private ChurchServiceMember() { }

      internal ChurchServiceMember(ChurchService service, ChurchMember member, ChurchServiceRole role)
      {
            ChurchService = service;
            ChurchServiceId = service.Id;
            ChurchMember = member;
            ChurchMemberId = member.Id;
            Role = role;
      }

      public void ChangeRole(ChurchServiceRole role)
      {
            Role = role;
            MarkUpdated();
      }
}

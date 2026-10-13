using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class ChurchMember : BaseEntity
{
      public Guid UserId { get; private set; }
      public User User { get; private set; } = null!;

      public Guid ChurchId { get; private set; }
      public Church Church { get; private set; } = null!;

      public ChurchMemberRole Role { get; private set; }
      public DateTime DateJoined { get; private set; }
      public DateTime? DateLeft { get; private set; }
      public string? Notes { get; private set; }

      public bool IsActive => DateLeft is null && !IsDeleted;

      private ChurchMember() { }

      public ChurchMember(Guid userId, Guid churchId, ChurchMemberRole role,
                          DateTime dateJoined, string? notes = null)
      {
            if (userId == Guid.Empty) throw new ArgumentException("User is required.", nameof(userId));
            if (churchId == Guid.Empty) throw new ArgumentException("Church is required.", nameof(churchId));

            UserId = userId;
            ChurchId = churchId;
            Role = role;
            DateJoined = dateJoined;
            Notes = notes;
      }

      public void ChangeRole(ChurchMemberRole role)
      {
            if (DateLeft is not null)
                  throw new BadRequestException("Cannot change the role of a member who has left.");

            Role = role;
            MarkUpdated();
      }

      public void Leave(DateTime? date = null)
      {
            if (DateLeft is not null)
                  throw new BadRequestException("Member has already left this church.");

            var left = date ?? DateTime.UtcNow;
            if (left < DateJoined)
                  throw new BadRequestException("Leave date cannot be before join date.");

            DateLeft = left;
            MarkUpdated();
      }
}
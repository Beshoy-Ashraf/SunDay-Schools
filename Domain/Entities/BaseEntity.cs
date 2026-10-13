namespace Domain.Common;

public abstract class BaseEntity
{
      public Guid Id { get; protected set; } = Guid.NewGuid();
      public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
      public DateTime? UpdatedAt { get; private set; }
      public DateTime? DeletedAt { get; private set; }
      public bool IsDeleted => DeletedAt.HasValue;

      public void MarkUpdated() => UpdatedAt = DateTime.UtcNow;

      public void SoftDelete()
      {
            DeletedAt = DateTime.UtcNow;
            MarkUpdated();
      }

      public void Restore()
      {
            DeletedAt = null;
            MarkUpdated();
      }
}
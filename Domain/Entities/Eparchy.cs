using Domain.Common;

namespace Domain.Entities;

public class Eparchy : BaseEntity
{
      private readonly List<Church> _churches = [];

      public string Name { get; private set; } = null!;
      public IReadOnlyCollection<Church> Churches => _churches.AsReadOnly();

      private Eparchy() { } // EF Core

      public Eparchy(string name)
      {
            Rename(name);
      }

      public void Rename(string name)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            Name = name.Trim();
            MarkUpdated();
      }
}
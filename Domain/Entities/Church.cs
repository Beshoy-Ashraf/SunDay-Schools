using Domain.Common;

namespace Domain.Entities;

public class Church : BaseEntity
{
      private readonly List<ChurchMember> _members = [];
      private readonly List<ChurchService> _services = [];

      public string Name { get; private set; } = null!;
      public string Address { get; private set; } = null!;
      public string PhoneNumber { get; private set; } = null!;
      public string Email { get; private set; } = null!;

      public Guid EparchyId { get; private set; }
      public Eparchy Eparchy { get; private set; } = null!;

      public IReadOnlyCollection<ChurchMember> Members => _members.AsReadOnly();
      public IReadOnlyCollection<ChurchService> Services => _services.AsReadOnly();

      private Church() { }

      public Church(string name, string address, string phoneNumber, string email, Guid eparchyId)
      {
            if (eparchyId == Guid.Empty)
                  throw new ArgumentException("Eparchy is required.", nameof(eparchyId));

            EparchyId = eparchyId;
            UpdateDetails(name, address, phoneNumber, email);
      }

      public void UpdateDetails(string name, string address, string phoneNumber, string email)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(address);
            ArgumentException.ThrowIfNullOrWhiteSpace(phoneNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            Name = name.Trim();
            Address = address.Trim();
            PhoneNumber = phoneNumber.Trim();
            Email = email.Trim().ToLowerInvariant();
            MarkUpdated();
      }
}
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
      private readonly List<RefreshToken> _refreshTokens = [];
      private readonly List<ChurchMember> _memberships = [];

      public string Username { get; private set; } = null!;
      public string Email { get; private set; } = null!;
      public string PasswordHash { get; private set; } = null!;
      public string DisplayName { get; private set; } = null!;
      public SystemRole Role { get; private set; }   // Admin / User  (غير ChurchMemberRole)

      public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
      public IReadOnlyCollection<ChurchMember> Memberships => _memberships.AsReadOnly();

      private User() { }

      public User(string username, string email, string passwordHash, string displayName, SystemRole role)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(username);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);
            ArgumentException.ThrowIfNullOrWhiteSpace(displayName);

            Username = username.Trim();
            Email = email.Trim().ToLowerInvariant();
            PasswordHash = passwordHash;
            DisplayName = displayName.Trim();
            Role = role;
      }

      public void ChangePassword(string newHash)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(newHash);
            PasswordHash = newHash;
            MarkUpdated();
      }

      public void AddRefreshToken(RefreshToken token) => _refreshTokens.Add(token);
}
namespace Domain.Entities;

public class RefreshToken
{
      public Guid Id { get; private set; } = Guid.NewGuid();
      public string Token { get; private set; } = null!;
      public DateTime ExpiresAt { get; private set; }
      public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
      public DateTime? RevokedAt { get; private set; }

      public Guid UserId { get; private set; }
      public User User { get; private set; } = null!;

      public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
      public bool IsRevoked => RevokedAt.HasValue;
      public bool IsActive => !IsRevoked && !IsExpired;

      private RefreshToken() { }

      public RefreshToken(Guid userId, string token, DateTime expiresAt)
      {
            ArgumentException.ThrowIfNullOrWhiteSpace(token);
            UserId = userId;
            Token = token;
            ExpiresAt = expiresAt;
      }

      public void Revoke() => RevokedAt ??= DateTime.UtcNow;
}
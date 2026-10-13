using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
      public void Configure(EntityTypeBuilder<RefreshToken> builder)
      {
            builder.ToTable("RefreshTokens");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedNever();

            builder.Property(t => t.Token).HasMaxLength(200).IsRequired();
            builder.Property(t => t.ExpiresAt).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();

            builder.Ignore(t => t.IsExpired);
            builder.Ignore(t => t.IsRevoked);
            builder.Ignore(t => t.IsActive);

            builder.HasIndex(t => t.Token).IsUnique();

            builder.HasOne(t => t.User)
                  .WithMany(u => u.RefreshTokens)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Matches User's soft-delete filter: tokens of a deleted user are not returned.
            builder.HasQueryFilter(t => t.User.DeletedAt == null);
      }
}

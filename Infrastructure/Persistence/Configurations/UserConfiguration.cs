using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
      protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
      {
            builder.ToTable("Users");

            builder.Property(u => u.Username).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(256).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(512).IsRequired();
            builder.Property(u => u.DisplayName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Role).HasConversion<string>().HasMaxLength(20).IsRequired();

            // Unique among non-deleted users only, so a deleted account's name/email can be reused.
            builder.HasIndex(u => u.Username).IsUnique().HasFilter("\"DeletedAt\" IS NULL");
            builder.HasIndex(u => u.Email).IsUnique().HasFilter("\"DeletedAt\" IS NULL");

            builder.Navigation(u => u.RefreshTokens).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(u => u.Memberships).UsePropertyAccessMode(PropertyAccessMode.Field);
      }
}

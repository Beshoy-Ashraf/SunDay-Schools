using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ChurchMemberConfiguration : BaseEntityConfiguration<ChurchMember>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ChurchMember> builder)
      {
            builder.ToTable("ChurchMembers");

            builder.Property(m => m.Role).HasConversion<string>().HasMaxLength(30).IsRequired();
            builder.Property(m => m.DateJoined).IsRequired();
            builder.Property(m => m.DateLeft);
            builder.Property(m => m.Notes).HasMaxLength(1000);

            builder.Ignore(m => m.IsActive);

            // A user can have only one active membership per church, but may leave and rejoin later.
            builder.HasIndex(m => new { m.UserId, m.ChurchId })
                  .IsUnique()
                  .HasFilter("\"DateLeft\" IS NULL AND \"DeletedAt\" IS NULL");

            builder.HasOne(m => m.User)
                  .WithMany(u => u.Memberships)
                  .HasForeignKey(m => m.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Church)
                  .WithMany(c => c.Members)
                  .HasForeignKey(m => m.ChurchId)
                  .OnDelete(DeleteBehavior.Restrict);
      }
}

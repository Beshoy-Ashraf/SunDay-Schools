using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ChurchServiceMemberConfiguration : BaseEntityConfiguration<ChurchServiceMember>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ChurchServiceMember> builder)
      {
            builder.ToTable("ChurchServiceMembers");

            builder.Property(sm => sm.Role).HasConversion<string>().HasMaxLength(30).IsRequired();

            // A member can only be assigned once per service (active assignments).
            builder.HasIndex(sm => new { sm.ChurchServiceId, sm.ChurchMemberId })
                  .IsUnique()
                  .HasFilter("\"DeletedAt\" IS NULL");

            builder.HasOne(sm => sm.ChurchService)
                  .WithMany(s => s.ServiceMembers)
                  .HasForeignKey(sm => sm.ChurchServiceId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sm => sm.ChurchMember)
                  .WithMany()
                  .HasForeignKey(sm => sm.ChurchMemberId)
                  .OnDelete(DeleteBehavior.Restrict);
      }
}

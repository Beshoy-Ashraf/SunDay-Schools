using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ActivityTypeConfiguration : BaseEntityConfiguration<ActivityType>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ActivityType> builder)
      {
            builder.ToTable("ActivityTypes");

            builder.Property(a => a.Code).HasConversion<string>().HasMaxLength(30).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.AppliesTo).HasConversion<string>().HasMaxLength(10).IsRequired();
            builder.Property(a => a.Points).IsRequired();
            builder.Property(a => a.MaxPerMonth);
            builder.Property(a => a.IsActive).IsRequired();

            // A service can only have one active configuration per activity code.
            builder.HasIndex(a => new { a.ChurchServiceId, a.Code })
                  .IsUnique()
                  .HasFilter("\"DeletedAt\" IS NULL");

            builder.HasOne(a => a.ChurchService)
                  .WithMany(s => s.ActivityTypes)
                  .HasForeignKey(a => a.ChurchServiceId)
                  .OnDelete(DeleteBehavior.Restrict);
      }
}

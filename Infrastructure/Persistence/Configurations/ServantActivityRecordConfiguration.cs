using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ServantActivityRecordConfiguration : BaseEntityConfiguration<ServantActivityRecord>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ServantActivityRecord> builder)
      {
            builder.ToTable("ServantActivityRecords");

            builder.Property(r => r.Answered).IsRequired();
            builder.Property(r => r.PointsEarned).IsRequired();

            // One answer per activity type per attendance record.
            builder.HasIndex(r => new { r.ChurchServiceAttendanceId, r.ActivityTypeId })
                  .IsUnique()
                  .HasFilter("\"DeletedAt\" IS NULL");

            builder.HasOne(r => r.ChurchServiceAttendance)
                  .WithMany(a => a.ActivityRecords)
                  .HasForeignKey(r => r.ChurchServiceAttendanceId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.ActivityType)
                  .WithMany()
                  .HasForeignKey(r => r.ActivityTypeId)
                  .OnDelete(DeleteBehavior.Restrict);
      }
}

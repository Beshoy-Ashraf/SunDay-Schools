using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ChurchServiceAttendanceConfiguration : BaseEntityConfiguration<ChurchServiceAttendance>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ChurchServiceAttendance> builder)
      {
            builder.ToTable("ChurchServiceAttendances");

            builder.Property(a => a.AttendedAt).IsRequired();

            // A member is recorded at most once per day per service (enforced at application level).
            builder.HasIndex(a => new { a.ChurchServiceId, a.ChurchMemberId, a.AttendedAt });

            builder.HasOne(a => a.ChurchService)
                  .WithMany()
                  .HasForeignKey(a => a.ChurchServiceId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.ChurchMember)
                  .WithMany()
                  .HasForeignKey(a => a.ChurchMemberId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.RecordedBy)
                  .WithMany()
                  .HasForeignKey(a => a.RecordedById)
                  .OnDelete(DeleteBehavior.Restrict);
      }
}

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

            // A member is recorded at most once per session.
            builder.HasIndex(a => new { a.ChurchServiceSessionId, a.ChurchMemberId })
                  .IsUnique()
                  .HasFilter("\"DeletedAt\" IS NULL");

            builder.HasOne(a => a.ChurchServiceSession)
                  .WithMany(s => s.Attendances)
                  .HasForeignKey(a => a.ChurchServiceSessionId)
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

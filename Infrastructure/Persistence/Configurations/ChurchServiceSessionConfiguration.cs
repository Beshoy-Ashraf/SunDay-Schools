using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ChurchServiceSessionConfiguration : BaseEntityConfiguration<ChurchServiceSession>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ChurchServiceSession> builder)
      {
            builder.ToTable("ChurchServiceSessions", t =>
                  t.HasCheckConstraint("CK_ChurchServiceSessions_EndTime_After_StartTime", "\"EndTime\" > \"StartTime\""));

            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s => s.EndTime).IsRequired();

            builder.HasIndex(s => new { s.ChurchServiceId, s.StartTime });

            builder.HasOne(s => s.ChurchService)
                  .WithMany(cs => cs.Sessions)
                  .HasForeignKey(s => s.ChurchServiceId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(s => s.Attendances).UsePropertyAccessMode(PropertyAccessMode.Field);
      }
}

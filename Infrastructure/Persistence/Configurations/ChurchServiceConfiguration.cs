using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ChurchServiceConfiguration : BaseEntityConfiguration<ChurchService>
{
      protected override void ConfigureEntity(EntityTypeBuilder<ChurchService> builder)
      {
            builder.ToTable("ChurchServices", t =>
                  t.HasCheckConstraint("CK_ChurchServices_EndTime_After_StartTime", "\"EndTime\" > \"StartTime\""));

            builder.Property(s => s.Name).HasMaxLength(200).IsRequired();
            builder.Property(s => s.Description).HasMaxLength(1000);
            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s => s.EndTime).IsRequired();

            builder.HasIndex(s => new { s.ChurchId, s.StartTime });

            builder.HasOne(s => s.Church)
                  .WithMany(c => c.Services)
                  .HasForeignKey(s => s.ChurchId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(s => s.Sessions).UsePropertyAccessMode(PropertyAccessMode.Field);
      }
}

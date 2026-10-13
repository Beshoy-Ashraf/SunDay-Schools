using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ChurchConfiguration : BaseEntityConfiguration<Church>
{
      protected override void ConfigureEntity(EntityTypeBuilder<Church> builder)
      {
            builder.ToTable("Churches");

            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Address).HasMaxLength(500).IsRequired();
            builder.Property(c => c.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(256).IsRequired();

            // Church names are unique within the same eparchy.
            builder.HasIndex(c => new { c.EparchyId, c.Name }).IsUnique().HasFilter("\"DeletedAt\" IS NULL");

            builder.HasOne(c => c.Eparchy)
                  .WithMany(e => e.Churches)
                  .HasForeignKey(c => c.EparchyId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(c => c.Members).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(c => c.Services).UsePropertyAccessMode(PropertyAccessMode.Field);
      }
}

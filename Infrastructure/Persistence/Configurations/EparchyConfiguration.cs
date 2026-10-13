using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EparchyConfiguration : BaseEntityConfiguration<Eparchy>
{
      protected override void ConfigureEntity(EntityTypeBuilder<Eparchy> builder)
      {
            builder.ToTable("Eparchies");

            builder.Property(e => e.Name).HasMaxLength(200).IsRequired();

            builder.HasIndex(e => e.Name).IsUnique().HasFilter("\"DeletedAt\" IS NULL");

            builder.Navigation(e => e.Churches).UsePropertyAccessMode(PropertyAccessMode.Field);
      }
}

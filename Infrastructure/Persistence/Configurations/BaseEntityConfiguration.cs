using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
      where TEntity : BaseEntity
{
      public void Configure(EntityTypeBuilder<TEntity> builder)
      {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreatedAt).IsRequired();
            builder.Property(e => e.UpdatedAt);
            builder.Property(e => e.DeletedAt);
            builder.Ignore(e => e.IsDeleted);

            // Soft-deleted rows are hidden from every query; use IgnoreQueryFilters() to see them.
            builder.HasQueryFilter(e => e.DeletedAt == null);

            ConfigureEntity(builder);
      }

      protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}

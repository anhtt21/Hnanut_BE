using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class MealImageConfiguration : IEntityTypeConfiguration<MealImage>
{
    public void Configure(EntityTypeBuilder<MealImage> builder)
    {
        builder.ToTable("MealImages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MealId)
            .IsRequired();

        builder.Property(x => x.ObjectKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SizeBytes)
            .IsRequired();

        builder.Property(x => x.Width);

        builder.Property(x => x.Height);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.MealId);

        builder.HasIndex(x => x.ObjectKey)
            .IsUnique();
    }
}
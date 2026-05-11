using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class FoodConfiguration : IEntityTypeConfiguration<Food> // Map Food sang bảng Foods.
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.ToTable("Foods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NormalizedName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DefaultServingGram)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.IsVerified)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => x.NormalizedName);
        builder.HasIndex(x => x.Category);
        builder.HasIndex(x => x.IsVerified);

        builder.HasMany(x => x.Aliases)
            .WithOne(x => x.Food)
            .HasForeignKey(x => x.FoodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.NutritionPer100g)
            .WithOne(x => x.Food)
            .HasForeignKey<NutritionPer100g>(x => x.FoodId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
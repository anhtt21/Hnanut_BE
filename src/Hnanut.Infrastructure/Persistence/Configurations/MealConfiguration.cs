using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class MealConfiguration : IEntityTypeConfiguration<Meal> // Map Meal sang bảng Meals.
{
    public void Configure(EntityTypeBuilder<Meal> builder)
    {
        builder.ToTable("Meals");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.MealType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.EatenAt)
            .IsRequired();

        builder.Property(x => x.Note)
            .HasMaxLength(500);

        builder.Property(x => x.TotalCalories)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.TotalProtein)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.TotalFat)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.TotalCarbs)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasIndex(x => new { x.UserId, x.EatenAt });
        builder.HasIndex(x => new { x.UserId, x.MealType });

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Meal)
            .HasForeignKey(x => x.MealId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Images)
            .WithOne(x => x.Meal)
            .HasForeignKey(x => x.MealId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
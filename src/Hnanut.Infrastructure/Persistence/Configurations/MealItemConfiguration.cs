using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class MealItemConfiguration : IEntityTypeConfiguration<MealItem> // Map MealItem sang bảng MealItems.
{
    public void Configure(EntityTypeBuilder<MealItem> builder)
    {
        builder.ToTable("MealItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MealId)
            .IsRequired();

        builder.Property(x => x.FoodId)
            .IsRequired();

        builder.Property(x => x.NameSnapshot)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Gram)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Calories)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.Protein)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.Fat)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.Carbs)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.IsUserEdited)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.MealId);
        builder.HasIndex(x => x.FoodId);

        builder.HasOne(x => x.Food)
            .WithMany()
            .HasForeignKey(x => x.FoodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class NutritionPer100gConfiguration : IEntityTypeConfiguration<NutritionPer100g>
{
    public void Configure(EntityTypeBuilder<NutritionPer100g> builder)
    {
        builder.ToTable("NutritionPer100g");

        builder.HasKey(x => x.FoodId);

        builder.Property(x => x.Calories)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Protein)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Fat)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Carbs)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Fiber)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Sugar)
            .IsRequired()
            .HasPrecision(8, 2);

        builder.Property(x => x.Sodium)
            .IsRequired()
            .HasPrecision(8, 2);
    }
}
using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class FoodAliasConfiguration : IEntityTypeConfiguration<FoodAlias> // Map FoodAlias sang bảng FoodAliases.
{
    public void Configure(EntityTypeBuilder<FoodAlias> builder)
    {
        builder.ToTable("FoodAliases");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FoodId)
            .IsRequired();

        builder.Property(x => x.AliasName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.NormalizedAlias)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.FoodId);
        builder.HasIndex(x => x.NormalizedAlias);
    }
}
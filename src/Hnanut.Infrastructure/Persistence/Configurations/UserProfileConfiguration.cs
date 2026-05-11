using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hnanut.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile> // Map UserProfile sang bảng UserProfiles.
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.HeightCm)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.WeightKg)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.Age)
            .IsRequired();

        builder.Property(x => x.Gender)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(x => x.Goal)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.ActivityLevel)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.DailyCalorieTarget)
            .IsRequired();
    }
}
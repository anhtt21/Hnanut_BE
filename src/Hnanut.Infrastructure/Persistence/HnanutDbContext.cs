using Hnanut.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hnanut.Infrastructure.Persistence;

public class HnanutDbContext : DbContext // Là cầu nối giữa code C# và SQL Server.
{
    public HnanutDbContext(DbContextOptions<HnanutDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();

    public DbSet<Food> Foods => Set<Food>();
    public DbSet<FoodAlias> FoodAliases => Set<FoodAlias>();
    public DbSet<NutritionPer100g> NutritionPer100g => Set<NutritionPer100g>();

    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealItem> MealItems => Set<MealItem>();
    public DbSet<MealImage> MealImages => Set<MealImage>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HnanutDbContext).Assembly);
    }
}
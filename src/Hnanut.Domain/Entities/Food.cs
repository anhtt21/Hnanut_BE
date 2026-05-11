using Hnanut.Domain.Common;

namespace Hnanut.Domain.Entities;

public class Food : BaseEntity
{
    private readonly List<FoodAlias> _aliases = new();

    private Food()
    {
    }

    private Food(
        string name,
        string normalizedName,
        string category,
        decimal defaultServingGram,
        bool isVerified)
    {
        ValidateBasicInfo(name, normalizedName, defaultServingGram);

        Name = name.Trim();
        NormalizedName = normalizedName.Trim();
        Category = category.Trim();
        DefaultServingGram = defaultServingGram;
        IsVerified = isVerified;
        CreatedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public decimal DefaultServingGram { get; private set; }
    public bool IsVerified { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public NutritionPer100g? NutritionPer100g { get; private set; }

    public IReadOnlyCollection<FoodAlias> Aliases => _aliases.AsReadOnly();

    public static Food Create(
        string name,
        string normalizedName,
        string category,
        decimal defaultServingGram,
        bool isVerified = false)
    {
        return new Food(
            name,
            normalizedName,
            category,
            defaultServingGram,
            isVerified);
    }

    public void UpdateBasicInfo(
        string name,
        string normalizedName,
        string category,
        decimal defaultServingGram,
        bool isVerified)
    {
        ValidateBasicInfo(name, normalizedName, defaultServingGram);

        Name = name.Trim();
        NormalizedName = normalizedName.Trim();
        Category = category.Trim();
        DefaultServingGram = defaultServingGram;
        IsVerified = isVerified;
        UpdatedAt = DateTime.UtcNow;
    }

    public FoodAlias AddAlias(string aliasName, string normalizedAlias)
    {
        var alias = FoodAlias.Create(Id, aliasName, normalizedAlias);
        _aliases.Add(alias);

        return alias;
    }

    public void SetNutrition(NutritionPer100g nutrition)
    {
        if (nutrition.FoodId != Id)
            throw new InvalidOperationException("Nutrition does not belong to this food.");

        NutritionPer100g = nutrition;
        UpdatedAt = DateTime.UtcNow;
    }

    private static void ValidateBasicInfo(
        string name,
        string normalizedName,
        decimal defaultServingGram)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Food name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(normalizedName))
            throw new ArgumentException("Normalized food name is required.", nameof(normalizedName));

        if (defaultServingGram <= 0)
            throw new ArgumentException("Default serving gram must be greater than 0.", nameof(defaultServingGram));
    }
}
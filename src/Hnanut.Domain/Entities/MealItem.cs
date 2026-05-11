using Hnanut.Domain.Common;

namespace Hnanut.Domain.Entities;

public class MealItem : BaseEntity
{
    private MealItem()
    {
    }

    private MealItem(
        Guid mealId,
        Guid foodId,
        string nameSnapshot,
        decimal gram,
        decimal calories,
        decimal protein,
        decimal fat,
        decimal carbs,
        bool isUserEdited)
    {
        if (mealId == Guid.Empty)
            throw new ArgumentException("Meal id is required.", nameof(mealId));

        if (foodId == Guid.Empty)
            throw new ArgumentException("Food id is required.", nameof(foodId));

        if (string.IsNullOrWhiteSpace(nameSnapshot))
            throw new ArgumentException("Food name snapshot is required.", nameof(nameSnapshot));

        if (gram <= 0)
            throw new ArgumentException("Gram must be greater than 0.", nameof(gram));

        MealId = mealId;
        FoodId = foodId;
        NameSnapshot = nameSnapshot.Trim();
        Gram = gram;
        Calories = calories;
        Protein = protein;
        Fat = fat;
        Carbs = carbs;
        IsUserEdited = isUserEdited;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid MealId { get; private set; }
    public Guid FoodId { get; private set; }

    public string NameSnapshot { get; private set; } = string.Empty;

    public decimal Gram { get; private set; }

    public decimal Calories { get; private set; }
    public decimal Protein { get; private set; }
    public decimal Fat { get; private set; }
    public decimal Carbs { get; private set; }

    public bool IsUserEdited { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Meal? Meal { get; private set; }
    public Food? Food { get; private set; }

    public static MealItem Create(
        Guid mealId,
        Guid foodId,
        string foodName,
        NutritionPer100g nutritionPer100g,
        decimal gram,
        bool isUserEdited = false)
    {
        if (nutritionPer100g.FoodId != foodId)
            throw new InvalidOperationException("Nutrition does not belong to the selected food.");

        var calculated = CalculateNutrition(nutritionPer100g, gram);

        return new MealItem(
            mealId,
            foodId,
            foodName,
            gram,
            calculated.Calories,
            calculated.Protein,
            calculated.Fat,
            calculated.Carbs,
            isUserEdited);
    }

    public void ChangeGram(NutritionPer100g nutritionPer100g, decimal gram)
    {
        if (nutritionPer100g.FoodId != FoodId)
            throw new InvalidOperationException("Nutrition does not belong to the selected food.");

        if (gram <= 0)
            throw new ArgumentException("Gram must be greater than 0.", nameof(gram));

        var calculated = CalculateNutrition(nutritionPer100g, gram);

        Gram = gram;
        Calories = calculated.Calories;
        Protein = calculated.Protein;
        Fat = calculated.Fat;
        Carbs = calculated.Carbs;
        IsUserEdited = true;
    }

    private static CalculatedNutrition CalculateNutrition(
        NutritionPer100g nutritionPer100g,
        decimal gram)
    {
        if (gram <= 0)
            throw new ArgumentException("Gram must be greater than 0.", nameof(gram));

        var ratio = gram / 100m;

        return new CalculatedNutrition(
            Math.Round(nutritionPer100g.Calories * ratio, 2),
            Math.Round(nutritionPer100g.Protein * ratio, 2),
            Math.Round(nutritionPer100g.Fat * ratio, 2),
            Math.Round(nutritionPer100g.Carbs * ratio, 2));
    }

    private sealed record CalculatedNutrition(
        decimal Calories,
        decimal Protein,
        decimal Fat,
        decimal Carbs);
}
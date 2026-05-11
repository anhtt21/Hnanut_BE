namespace Hnanut.Domain.Entities;

public class NutritionPer100g
{
    private NutritionPer100g()
    {
    }

    private NutritionPer100g(
        Guid foodId,
        decimal calories,
        decimal protein,
        decimal fat,
        decimal carbs,
        decimal fiber,
        decimal sugar,
        decimal sodium)
    {
        if (foodId == Guid.Empty)
            throw new ArgumentException("Food id is required.", nameof(foodId));

        ValidateNutrition(calories, protein, fat, carbs, fiber, sugar, sodium);

        FoodId = foodId;
        Calories = calories;
        Protein = protein;
        Fat = fat;
        Carbs = carbs;
        Fiber = fiber;
        Sugar = sugar;
        Sodium = sodium;
    }

    public Guid FoodId { get; private set; }

    public decimal Calories { get; private set; }
    public decimal Protein { get; private set; }
    public decimal Fat { get; private set; }
    public decimal Carbs { get; private set; }
    public decimal Fiber { get; private set; }
    public decimal Sugar { get; private set; }
    public decimal Sodium { get; private set; }

    public Food? Food { get; private set; }

    public static NutritionPer100g Create(
        Guid foodId,
        decimal calories,
        decimal protein,
        decimal fat,
        decimal carbs,
        decimal fiber = 0,
        decimal sugar = 0,
        decimal sodium = 0)
    {
        return new NutritionPer100g(
            foodId,
            calories,
            protein,
            fat,
            carbs,
            fiber,
            sugar,
            sodium);
    }

    public void Update(
        decimal calories,
        decimal protein,
        decimal fat,
        decimal carbs,
        decimal fiber = 0,
        decimal sugar = 0,
        decimal sodium = 0)
    {
        ValidateNutrition(calories, protein, fat, carbs, fiber, sugar, sodium);

        Calories = calories;
        Protein = protein;
        Fat = fat;
        Carbs = carbs;
        Fiber = fiber;
        Sugar = sugar;
        Sodium = sodium;
    }

    private static void ValidateNutrition(
        decimal calories,
        decimal protein,
        decimal fat,
        decimal carbs,
        decimal fiber,
        decimal sugar,
        decimal sodium)
    {
        if (calories < 0)
            throw new ArgumentException("Calories cannot be negative.", nameof(calories));

        if (protein < 0)
            throw new ArgumentException("Protein cannot be negative.", nameof(protein));

        if (fat < 0)
            throw new ArgumentException("Fat cannot be negative.", nameof(fat));

        if (carbs < 0)
            throw new ArgumentException("Carbs cannot be negative.", nameof(carbs));

        if (fiber < 0)
            throw new ArgumentException("Fiber cannot be negative.", nameof(fiber));

        if (sugar < 0)
            throw new ArgumentException("Sugar cannot be negative.", nameof(sugar));

        if (sodium < 0)
            throw new ArgumentException("Sodium cannot be negative.", nameof(sodium));
    }
}
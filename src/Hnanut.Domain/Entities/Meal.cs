using Hnanut.Domain.Common;
using Hnanut.Domain.Enums;

namespace Hnanut.Domain.Entities;

public class Meal : BaseEntity
{
    private readonly List<MealItem> _items = new();
    private readonly List<MealImage> _images = new();

    private Meal()
    {
    }

    private Meal(
        Guid userId,
        MealType mealType,
        DateTime eatenAt,
        string? note)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id is required.", nameof(userId));

        UserId = userId;
        MealType = mealType;
        EatenAt = eatenAt;
        Note = note?.Trim();
        CreatedAt = DateTime.UtcNow;

        RecalculateTotals();
    }

    public Guid UserId { get; private set; }
    public MealType MealType { get; private set; }
    public DateTime EatenAt { get; private set; }
    public string? Note { get; private set; }

    public decimal TotalCalories { get; private set; }
    public decimal TotalProtein { get; private set; }
    public decimal TotalFat { get; private set; }
    public decimal TotalCarbs { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public User? User { get; private set; }

    public IReadOnlyCollection<MealItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<MealImage> Images => _images.AsReadOnly();

    public static Meal Create(
        Guid userId,
        MealType mealType,
        DateTime eatenAt,
        string? note = null)
    {
        return new Meal(userId, mealType, eatenAt, note);
    }

    public void UpdateInfo(
        MealType mealType,
        DateTime eatenAt,
        string? note)
    {
        MealType = mealType;
        EatenAt = eatenAt;
        Note = note?.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public MealItem AddItem(
        Guid foodId,
        string foodName,
        NutritionPer100g nutritionPer100g,
        decimal gram,
        bool isUserEdited = false)
    {
        var item = MealItem.Create(
            Id,
            foodId,
            foodName,
            nutritionPer100g,
            gram,
            isUserEdited);

        _items.Add(item);

        RecalculateTotals();
        UpdatedAt = DateTime.UtcNow;

        return item;
    }

    public void RemoveItem(Guid mealItemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == mealItemId);

        if (item is null)
            throw new InvalidOperationException("Meal item not found.");

        _items.Remove(item);

        RecalculateTotals();
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeItemGram(
        Guid mealItemId,
        NutritionPer100g nutritionPer100g,
        decimal gram)
    {
        var item = _items.FirstOrDefault(x => x.Id == mealItemId);

        if (item is null)
            throw new InvalidOperationException("Meal item not found.");

        item.ChangeGram(nutritionPer100g, gram);

        RecalculateTotals();
        UpdatedAt = DateTime.UtcNow;
    }

    public MealImage AttachImage(
        string objectKey,
        string contentType,
        long sizeBytes,
        int? width,
        int? height)
    {
        var image = MealImage.Create(
            Id,
            objectKey,
            contentType,
            sizeBytes,
            width,
            height);

        _images.Add(image);
        UpdatedAt = DateTime.UtcNow;

        return image;
    }

    private void RecalculateTotals()
    {
        TotalCalories = Math.Round(_items.Sum(x => x.Calories), 2);
        TotalProtein = Math.Round(_items.Sum(x => x.Protein), 2);
        TotalFat = Math.Round(_items.Sum(x => x.Fat), 2);
        TotalCarbs = Math.Round(_items.Sum(x => x.Carbs), 2);
    }
}
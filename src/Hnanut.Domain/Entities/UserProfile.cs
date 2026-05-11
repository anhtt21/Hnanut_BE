using Hnanut.Domain.Enums;

namespace Hnanut.Domain.Entities;

public class UserProfile
{
    private UserProfile()
    {
    }

    private UserProfile(
        Guid userId,
        decimal heightCm,
        decimal weightKg,
        int age,
        Gender gender,
        Goal goal,
        ActivityLevel activityLevel,
        int dailyCalorieTarget)
    {
        Validate(heightCm, weightKg, age, dailyCalorieTarget);

        UserId = userId;
        HeightCm = heightCm;
        WeightKg = weightKg;
        Age = age;
        Gender = gender;
        Goal = goal;
        ActivityLevel = activityLevel;
        DailyCalorieTarget = dailyCalorieTarget;
    }

    public Guid UserId { get; private set; }
    public decimal HeightCm { get; private set; }
    public decimal WeightKg { get; private set; }
    public int Age { get; private set; }
    public Gender Gender { get; private set; }
    public Goal Goal { get; private set; }
    public ActivityLevel ActivityLevel { get; private set; }
    public int DailyCalorieTarget { get; private set; }

    public User? User { get; private set; }

    public static UserProfile Create(
        Guid userId,
        decimal heightCm,
        decimal weightKg,
        int age,
        Gender gender,
        Goal goal,
        ActivityLevel activityLevel,
        int dailyCalorieTarget)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id is required.", nameof(userId));

        return new UserProfile(
            userId,
            heightCm,
            weightKg,
            age,
            gender,
            goal,
            activityLevel,
            dailyCalorieTarget);
    }

    public void Update(
        decimal heightCm,
        decimal weightKg,
        int age,
        Gender gender,
        Goal goal,
        ActivityLevel activityLevel,
        int dailyCalorieTarget)
    {
        Validate(heightCm, weightKg, age, dailyCalorieTarget);

        HeightCm = heightCm;
        WeightKg = weightKg;
        Age = age;
        Gender = gender;
        Goal = goal;
        ActivityLevel = activityLevel;
        DailyCalorieTarget = dailyCalorieTarget;
    }

    private static void Validate(
        decimal heightCm,
        decimal weightKg,
        int age,
        int dailyCalorieTarget)
    {
        if (heightCm <= 0)
            throw new ArgumentException("Height must be greater than 0.");

        if (weightKg <= 0)
            throw new ArgumentException("Weight must be greater than 0.");

        if (age <= 0)
            throw new ArgumentException("Age must be greater than 0.");

        if (dailyCalorieTarget <= 0)
            throw new ArgumentException("Daily calorie target must be greater than 0.");
    }
}
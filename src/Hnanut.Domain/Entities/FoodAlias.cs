using Hnanut.Domain.Common;

namespace Hnanut.Domain.Entities;

public class FoodAlias : BaseEntity
{
    private FoodAlias()
    {
    }

    private FoodAlias(Guid foodId, string aliasName, string normalizedAlias)
    {
        if (foodId == Guid.Empty)
            throw new ArgumentException("Food id is required.", nameof(foodId));

        if (string.IsNullOrWhiteSpace(aliasName))
            throw new ArgumentException("Alias name is required.", nameof(aliasName));

        if (string.IsNullOrWhiteSpace(normalizedAlias))
            throw new ArgumentException("Normalized alias is required.", nameof(normalizedAlias));

        FoodId = foodId;
        AliasName = aliasName.Trim();
        NormalizedAlias = normalizedAlias.Trim();
    }

    public Guid FoodId { get; private set; }
    public string AliasName { get; private set; } = string.Empty;
    public string NormalizedAlias { get; private set; } = string.Empty;

    public Food? Food { get; private set; }

    public static FoodAlias Create(Guid foodId, string aliasName, string normalizedAlias)
    {
        return new FoodAlias(foodId, aliasName, normalizedAlias);
    }
}
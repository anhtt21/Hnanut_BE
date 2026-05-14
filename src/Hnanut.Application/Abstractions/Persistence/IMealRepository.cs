using Hnanut.Domain.Entities;

namespace Hnanut.Application.Abstractions.Persistence;

public interface IMealRepository
{
    Task<Meal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Meal>> GetByUserIdAsync(
        Guid userId,
        DateTime? from,
        DateTime? to,
        CancellationToken cancellationToken = default);

    Task AddAsync(Meal meal, CancellationToken cancellationToken = default);

    void Remove(Meal meal);
}

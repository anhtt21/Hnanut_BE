using Hnanut.Domain.Entities;

namespace Hnanut.Application.Abstractions.Persistence;

public interface IFoodRepository
{
    Task<Food?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Food>> SearchAsync(
        string normalizedKeyword,
        int limit = 20,
        CancellationToken cancellationToken = default);
}

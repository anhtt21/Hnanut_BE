using Hnanut.Domain.Entities;

namespace Hnanut.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    Task<bool> ExistsByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    Task AddAsync(User user, CancellationToken cancellationToken = default);
}

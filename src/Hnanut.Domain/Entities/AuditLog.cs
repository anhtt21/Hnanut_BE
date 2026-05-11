using Hnanut.Domain.Common;

namespace Hnanut.Domain.Entities;

public class AuditLog : BaseEntity
{
    private AuditLog()
    {
    }

    private AuditLog(
        Guid userId,
        string action,
        string entityName,
        string? entityId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id is required.", nameof(userId));

        if (string.IsNullOrWhiteSpace(action))
            throw new ArgumentException("Action is required.", nameof(action));

        if (string.IsNullOrWhiteSpace(entityName))
            throw new ArgumentException("Entity name is required.", nameof(entityName));

        UserId = userId;
        Action = action.Trim();
        EntityName = entityName.Trim();
        EntityId = entityId?.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid UserId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string EntityName { get; private set; } = string.Empty;
    public string? EntityId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User? User { get; private set; }

    public static AuditLog Create(
        Guid userId,
        string action,
        string entityName,
        string? entityId = null)
    {
        return new AuditLog(
            userId,
            action,
            entityName,
            entityId);
    }
}
namespace Hnanut.Domain.Common;

public abstract class BaseEntity // BaseEntity dùng cho các entity có Id riêng
{
    public Guid Id { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}
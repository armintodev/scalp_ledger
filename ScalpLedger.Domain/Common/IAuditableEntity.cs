namespace ScalpLedger.Domain.Common;

/// <typeparam name="TKey">Due to unification, you can use the type based in struct like as int, long, GUID, and so on.</typeparam>
public interface IAuditableEntity<TKey> where TKey : struct
{
    TKey Id { get; set; }
    public DateTime CreatedAt { get; }
    public DateTime? LastModifiedAt { get; }
    public DateTime? DeletedAt { get; }
    public long? CreatedBy { get; }
    public long? LastModifiedBy { get; }
    public bool IsDeleted { get; }

    void DeleteItem();
    void RestoreItem();
}

public interface IAuditableEntity : IAuditableEntity<long>;
namespace ScalpLedger.Domain.Common;

public abstract class AuditableEntity<TKey> : IAuditableEntity<TKey> where TKey : struct
{
    public TKey Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public long? CreatedBy { get; set; }
    public long? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; }

    public void DeleteItem()
    {
        IsDeleted = true;
    }

    public void RestoreItem()
    {
        IsDeleted = false;
    }
}

public abstract class AuditableEntity : AuditableEntity<long>;
namespace ScalpLedger.Domain.Common;

public interface IBaseRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>, IAggregateRoot, IAuditableEntity<TKey>
    where TKey : struct
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken ct);

    void AddRange(TEntity[] entities);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct);

    void Update(TEntity entity);
    void UpdateRange(List<TEntity> entities);

    void Delete(TEntity entity);

    Task SaveAsync(CancellationToken ct);
    void Save();
}

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, long>
    where TEntity : AuditableEntity, IAggregateRoot;
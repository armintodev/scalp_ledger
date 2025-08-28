using System.Linq.Expressions;

namespace ScalpLedger.Domain.Common;

public interface IQueryRepository<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>, IAggregateRoot
    where TKey : struct
{
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> Find();

    Task<TResult[]> GetResult<TResult>(
        IQueryable<TEntity> query,
        Expression<Func<TEntity, TResult>> resultSelector);

    Task<TEntity?> FindToUpdateAsync(TKey id, CancellationToken ct);

    TEntity? FindById(params object[] ids);
    ValueTask<TEntity?> FindByIdAsync(CancellationToken ct, params object[] ids);
    Task<TEntity?> FindWithoutQueryFilterAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);

    Task<bool> IsExistsAsync(CancellationToken ct);
    bool IsExists();

    Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct);
    bool IsExists(Expression<Func<TEntity, bool>> predicate);
    Task<bool> IsDuplicatedAsync(TKey id, Expression<Func<TEntity, bool>> condition, CancellationToken ct);
}

public interface IQueryRepository<TEntity> : IQueryRepository<TEntity, long>
    where TEntity : AuditableEntity, IAggregateRoot;
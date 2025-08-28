using System.Linq.Expressions;
using ScalpLedger.Domain.Common;

namespace ScalpLedger.Infrastructure.Persistence.Repositories.Base;

public class EfQueryRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>, IAggregateRoot
    where TKey : struct
{
    protected DbSet<TEntity> Entities { get; }
    protected IQueryable<TEntity> Table => Entities;
    protected IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    protected DbContext Context { get; }

    public EfQueryRepository(DbContext context)
    {
        Context = context;
        Entities = Context.Set<TEntity>();
    }

    /// <summary>
    /// list of <see cref="TEntity"/> by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return TableNoTracking.Where(predicate);
    }

    public IQueryable<TEntity> Find()
    {
        return TableNoTracking;
    }

    public async Task<TResult[]> GetResult<TResult>(IQueryable<TEntity> query,
        Expression<Func<TEntity, TResult>> resultSelector)
    {
        return await query.Select(resultSelector).ToArrayAsync();
    }

    public async Task<TEntity?> FindToUpdateAsync(TKey id, CT ct)
    {
        return await Entities.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken: ct);
    }

    /// <summary>
    /// find item in <see cref="TEntity"/> by tracking.
    /// </summary>
    /// <returns></returns>
    public virtual TEntity? FindById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    /// <summary>
    /// find async item in <see cref="TEntity"/> by tracking.
    /// </summary>
    /// <returns></returns>
    public virtual ValueTask<TEntity?> FindByIdAsync(CT ct, params object[] ids)
    {
        return Entities.FindAsync(ids, ct);
    }

    public async Task<TEntity?> FindWithoutQueryFilterAsync(Expression<Func<TEntity, bool>> predicate, CT ct)
    {
        return await Entities.IgnoreQueryFilters().SingleOrDefaultAsync(predicate, ct);
    }

    /// <summary>
    /// is this <see cref="TEntity"/> exists in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsExists()
    {
        return TableNoTracking.Any();
    }

    /// <summary>
    /// is this <see cref="TEntity"/> exists async in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> IsExistsAsync(CT ct)
    {
        return await TableNoTracking.AnyAsync(ct);
    }

    /// <summary>
    /// is this <see cref="TEntity"/> exists in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsExists(Expression<Func<TEntity, bool>> predicate)
    {
        return TableNoTracking.Any(predicate);
    }

    public async Task<bool> IsDuplicatedAsync(TKey id, Expression<Func<TEntity, bool>> condition, CT ct)
    {
        Expression<Func<TEntity, bool>> c1 = e => !e.Id.Equals(id);

        var parameter = Expression.Parameter(typeof(TEntity));

        var body = Expression.AndAlso(
            Expression.Invoke(c1, parameter),
            Expression.Invoke(condition, parameter)
        );

        var predicate = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

        return await TableNoTracking.AnyAsync(predicate, ct);
    }

    /// <summary>
    /// is this <see cref="TEntity"/> exists async in database by no tracking.
    /// </summary>
    /// <returns></returns>
    public virtual async Task<bool> IsExistsAsync(Expression<Func<TEntity, bool>> predicate, CT ct)
    {
        return await TableNoTracking.AnyAsync(predicate, ct);
    }
}
using ScalpLedger.Domain.Common;

namespace ScalpLedger.Infrastructure.Persistence.Repositories.Base;

public class EfRepository<TEntity, TKey> : EfQueryRepository<TEntity, TKey>, IBaseRepository<TEntity, TKey>
    where TEntity : AuditableEntity<TKey>, IAggregateRoot
    where TKey : struct
{
    public EfRepository(DbContext context) : base(context)
    {
    }

    public virtual void Add(TEntity entity)
    {
        Entities.Add(entity);
    }

    public virtual async Task AddAsync(TEntity entity, CT ct)
    {
        await Entities.AddAsync(entity, ct);
    }

    public virtual void AddRange(TEntity[] entities)
    {
        Entities.AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CT ct)
    {
        await Entities.AddRangeAsync(entities, ct);
    }

    public virtual void Update(TEntity entity)
    {
        Entities.Update(entity);
    }

    public virtual void UpdateRange(List<TEntity> entities)
    {
        Entities.UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public virtual void Save()
    {
        Context.SaveChanges();
    }

    public virtual async Task SaveAsync(CT ct)
    {
        await Context.SaveChangesAsync(ct);
    }
}

public class EfRepository<TEntity> : EfRepository<TEntity, long>
    where TEntity : AuditableEntity<long>, IAggregateRoot
{
    public EfRepository(DbContext context) : base(context)
    {
    }
}
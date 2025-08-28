using Microsoft.EntityFrameworkCore.Diagnostics;
using ScalpLedger.Domain.Common;

namespace ScalpLedger.Infrastructure.Persistence.Interceptors;

public sealed class UpdateAuditableInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CT ct = new())
    {
        if (eventData.Context is not null)
            UpdateAudit(eventData.Context);

        return base.SavingChangesAsync(eventData, result, ct);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not null)
            UpdateAudit(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    private static void UpdateAudit(DbContext context)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(p => p.CreatedAt).CurrentValue = utcNow;
                    entry.Property(p => p.LastModifiedAt).CurrentValue = utcNow;

                    break;
                case EntityState.Modified:
                    entry.Property(p => p.LastModifiedAt).CurrentValue = utcNow;

                    break;
                case EntityState.Deleted:
                    entry.Property(p => p.IsDeleted).CurrentValue = true;
                    entry.Property(p => p.DeletedAt).CurrentValue = utcNow;

                    break;
            }
        }
    }
}
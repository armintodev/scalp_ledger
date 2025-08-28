using System.Linq.Expressions;
using ScalpLedger.Domain.Common;

namespace ScalpLedger.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDeletedQueryFilters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType)) continue;

            // Build the expression:  IsDeleted == false
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var isDeletedProperty = Expression.Property(parameter, nameof(IAuditableEntity.IsDeleted));
            var isDeletedFalse = Expression.Equal(isDeletedProperty, Expression.Constant(false));
            var lambda = Expression.Lambda(isDeletedFalse, parameter);

            // Apply filter
            modelBuilder.Entity(entityType.ClrType)
                .HasQueryFilter(lambda);
        }
    }
}
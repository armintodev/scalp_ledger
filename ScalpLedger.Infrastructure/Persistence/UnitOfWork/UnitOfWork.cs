using Microsoft.EntityFrameworkCore.Storage;
using ScalpLedger.Application.Common.Databases;

namespace ScalpLedger.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    private readonly TContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync(CT ct)
    {
        if (_currentTransaction == null)
            _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitTransactionAsync(CT ct)
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.CommitAsync(ct);
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task RollbackTransactionAsync(CT ct)
    {
        try
        {
            if (_currentTransaction != null)
                await _currentTransaction.RollbackAsync(ct);
        }
        finally
        {
            await DisposeTransactionAsync();
        }
    }

    public async Task<int> SaveChangesAsync(CT ct)
    {
        return await _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    private async Task DisposeTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }
}
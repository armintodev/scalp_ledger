namespace ScalpLedger.Application.Common.Databases;

public interface IUnitOfWork : IContextTransaction, IDisposable, IAsyncDisposable
{
    Task<int> SaveChangesAsync(CT ct);
}
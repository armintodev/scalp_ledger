namespace ScalpLedger.Application.Common.
    Databases;

public interface IContextTransaction
{
    Task BeginTransactionAsync(CT ct);
    Task CommitTransactionAsync(CT ct);
    Task RollbackTransactionAsync(CT ct);
}
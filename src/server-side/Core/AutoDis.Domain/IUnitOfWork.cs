using System.Data;

namespace AutoDis.Domain;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync( 
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken cancellationToken = default
    );

    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    bool HasActiveTransaction { get; }
}

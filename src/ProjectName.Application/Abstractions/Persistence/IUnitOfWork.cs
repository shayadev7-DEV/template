namespace ProjectName.Application.Abstractions.Persistence;
/// <summary>Commits all repository changes in one transaction boundary.</summary>
/// <remarks>Use cases call this abstraction to remain independent of EF Core transactions.</remarks>
public interface IUnitOfWork { Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default); }

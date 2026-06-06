namespace ProjectName.Infrastructure.ExternalServices;
/// <summary>Email service port for infrastructure adapters.</summary>
public interface IEmailService { Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default); }
/// <summary>SMS service port for infrastructure adapters.</summary>
public interface ISmsService { Task SendAsync(string to, string message, CancellationToken cancellationToken = default); }
/// <summary>File service port for storage adapters.</summary>
public interface IFileService { Task<string> SaveAsync(Stream stream, string fileName, CancellationToken cancellationToken = default); }
/// <summary>Background job queue abstraction.</summary>
public interface IBackgroundJobService { ValueTask QueueAsync(Func<CancellationToken, ValueTask> workItem, CancellationToken cancellationToken = default); }

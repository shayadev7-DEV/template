namespace ProjectName.Shared.Options;
/// <summary>JWT token configuration bound through Options Pattern.</summary>
public sealed class JwtOptions { public const string SectionName = "Jwt"; public string Issuer { get; init; } = string.Empty; public string Audience { get; init; } = string.Empty; public string SigningKey { get; init; } = string.Empty; public int ExpirationMinutes { get; init; } = 60; }
/// <summary>Database configuration bound through Options Pattern.</summary>
public sealed class DatabaseOptions { public const string SectionName = "Database"; public string ConnectionString { get; init; } = string.Empty; public bool EnableSensitiveDataLogging { get; init; } }
/// <summary>Cache configuration bound through Options Pattern.</summary>
public sealed class CacheOptions { public const string SectionName = "Cache"; public string Provider { get; init; } = "Memory"; public string? RedisConnectionString { get; init; } }
/// <summary>Hybrid authentication configuration bound through Options Pattern.</summary>
public sealed class AuthenticationOptions { public const string SectionName = "Authentication"; public string Provider { get; init; } = "Identity"; }
/// <summary>Email configuration bound through Options Pattern.</summary>
public sealed class EmailOptions { public const string SectionName = "Email"; public string From { get; init; } = string.Empty; public string SmtpHost { get; init; } = string.Empty; public int SmtpPort { get; init; } = 587; }
/// <summary>Active Directory LDAP configuration bound through Options Pattern.</summary>
public sealed class ActiveDirectoryOptions { public const string SectionName = "ActiveDirectory"; public string Domain { get; init; } = string.Empty; public string LdapUrl { get; init; } = string.Empty; }

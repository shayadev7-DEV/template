# TestAdmin Manual Migration

This repository does not contain generated EF Core migration files, so `TestAdmin_Manual_Migration.sql` is a hand-authored SQL Server script generated from the refactored active EF Core model.

## Detected DbContext names

- `EnterpriseTemplate.Persistence.ApplicationDbContext`
  - Inherits from `IdentityDbContext<IdentityApplicationUser, IdentityRole<Guid>, Guid>`.
  - Registered with SQL Server through `AddDbContext<ApplicationDbContext>()`.
  - Uses the persistence assembly as the EF Core migrations assembly.

## Migration files used

No EF Core migration files were found in the project. The manual SQL was derived from:

1. `src/EnterpriseTemplate.Persistence/ApplicationDbContext.cs`
2. `src/EnterpriseTemplate.Persistence/DependencyInjection.cs`
3. `src/EnterpriseTemplate.Persistence/Configurations/*.cs`
4. `src/EnterpriseTemplate.Persistence/Seed/SeedData.cs`
5. ASP.NET Core Identity's default EF Core schema for `IdentityDbContext<IdentityApplicationUser, IdentityRole<Guid>, Guid>`.

## Target authorization schema

- Authentication and role membership are stored in ASP.NET Core Identity tables:
  - `AspNetUsers`
  - `AspNetRoles`
  - `AspNetUserRoles`
  - Identity claims, logins, tokens, and role claims tables
- Business users are stored in `Domain.Users`.
- Permission definitions are stored in `Domain.Permissions`.
- Permission grants are stored in `Domain.RolePermissions`, where `RoleId` references `AspNetRoles.Id`.
- `Domain.Roles` and `Domain.UserRoles` are legacy duplicate authorization tables and are no longer created.

## Execution order

The SQL script is ordered as follows:

1. Switch to the target database with `USE [TestAdmin];`.
2. Enable standard SQL Server session settings.
3. Create the `Domain` schema if it does not already exist.
4. Create `__EFMigrationsHistory` if it does not already exist.
5. Create Identity, domain user, permission, and role-permission tables.
6. Migrate legacy `Domain.Roles` rows into `AspNetRoles` when those legacy tables already exist.
7. Migrate legacy `Domain.UserRoles` rows into `AspNetUserRoles` by using `AspNetUsers.DomainUserId`.
8. Drop legacy `Domain.UserRoles` and `Domain.Roles` after successful migration.
9. Add foreign key constraints.
10. Add indexes and unique indexes.
11. Normalize known legacy permission codes to lower-case policy codes, then insert idempotent seed data for permissions, the `Administrator` Identity role, and Administrator role-permission grants.
12. Insert idempotent manual baseline rows into `__EFMigrationsHistory`.

## Warnings and risky operations

- **No compiled EF migration files exist.** The `__EFMigrationsHistory` rows use manual ids: `20260607000000_ManualInitialCreate` and `20260607000100_RemoveDuplicateDomainRoles`.
- **Legacy role migration is guarded.** If `Domain.Roles` conflicts with an existing `AspNetRoles` row by normalized name but not by id, or if `Domain.UserRoles` contains memberships that cannot be mapped to an Identity user through `AspNetUsers.DomainUserId`, the script throws and stops before dropping legacy tables.
- **Existing partially-created schemas may need review.** The script uses `IF NOT EXISTS` guards for tables, constraints, and indexes. It does not deeply validate that an existing table has the exact expected column definitions.
- **Seed data uses deterministic GUIDs.** Runtime seeding creates entities with generated GUIDs, but the manual script uses stable GUIDs so repeated executions remain idempotent. Known legacy permission codes such as `Users.Read` are normalized to lower-case policy codes such as `users.read`.
- **Default constraints are not added to domain audit columns.** The EF Core model does not configure SQL defaults for those columns, and the application writes values through its audit logic.

## How to run in SSMS

1. Open SQL Server Management Studio.
2. Connect to the SQL Server instance that hosts the `TestAdmin` database.
3. Back up the `TestAdmin` database before running the script.
4. Open `TestAdmin_Manual_Migration.sql` in a new query window.
5. Confirm the script starts with:

   ```sql
   USE [TestAdmin];
   GO
   ```

6. Execute the full script.
7. Review the Messages tab for any SQL Server errors.
8. Optionally verify that these tables exist:
   - `dbo.AspNetUsers`
   - `dbo.AspNetRoles`
   - `dbo.AspNetUserRoles`
   - `Domain.Users`
   - `Domain.Permissions`
   - `Domain.RolePermissions`
   - `__EFMigrationsHistory`
9. Optionally verify that these legacy duplicate tables do not exist after successful migration:
   - `Domain.Roles`
   - `Domain.UserRoles`

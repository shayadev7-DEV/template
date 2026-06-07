USE [TestAdmin];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/*
    Manual SQL Server migration script for EnterpriseTemplate.Persistence.ApplicationDbContext.
    Generated from the refactored active EF Core model because no EF Core migration files were found.

    Target authorization architecture:
    - ASP.NET Core Identity owns authentication users, roles, claims, logins, tokens, lockout, and role membership.
    - Domain.Users owns the business user aggregate.
    - Domain.Permissions and Domain.RolePermissions model permission grants for Identity roles.
    - Domain.Roles and Domain.UserRoles are legacy duplicate authorization tables and are not created.
*/

IF SCHEMA_ID(N'Domain') IS NULL
BEGIN
    EXEC(N'CREATE SCHEMA [Domain]');
END;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]', N'U') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory]
    (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

IF OBJECT_ID(N'[AspNetRoles]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetRoles]
    (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'[AspNetUsers]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetUsers]
    (
        [Id] uniqueidentifier NOT NULL,
        [DomainUserId] uniqueidentifier NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'[Domain].[Permissions]', N'U') IS NULL
BEGIN
    CREATE TABLE [Domain].[Permissions]
    (
        [Id] uniqueidentifier NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetimeoffset NOT NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [ModifiedDate] datetimeoffset NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedDate] datetimeoffset NULL,
        [IsDeleted] bit NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Code] nvarchar(256) NOT NULL,
        [Type] int NOT NULL,
        CONSTRAINT [PK_Permissions] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'[Domain].[Users]', N'U') IS NULL
BEGIN
    CREATE TABLE [Domain].[Users]
    (
        [Id] uniqueidentifier NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetimeoffset NOT NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [ModifiedDate] datetimeoffset NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedDate] datetimeoffset NULL,
        [IsDeleted] bit NOT NULL,
        [UserType] int NOT NULL,
        [Status] int NOT NULL,
        [Email] nvarchar(320) NOT NULL,
        [FirstName] nvarchar(100) NOT NULL,
        [LastName] nvarchar(100) NOT NULL,
        [MobileNumber] nvarchar(20) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'[AspNetRoleClaims]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetRoleClaims]
    (
        [Id] int IDENTITY(1,1) NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'[AspNetUserClaims]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetUserClaims]
    (
        [Id] int IDENTITY(1,1) NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF OBJECT_ID(N'[AspNetUserLogins]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetUserLogins]
    (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey])
    );
END;
GO

IF OBJECT_ID(N'[AspNetUserRoles]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetUserRoles]
    (
        [UserId] uniqueidentifier NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
    );
END;
GO

IF OBJECT_ID(N'[AspNetUserTokens]', N'U') IS NULL
BEGIN
    CREATE TABLE [AspNetUserTokens]
    (
        [UserId] uniqueidentifier NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
    );
END;
GO

IF OBJECT_ID(N'[Domain].[RolePermissions]', N'U') IS NULL
BEGIN
    CREATE TABLE [Domain].[RolePermissions]
    (
        [Id] uniqueidentifier NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetimeoffset NOT NULL,
        [ModifiedBy] nvarchar(max) NULL,
        [ModifiedDate] datetimeoffset NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedDate] datetimeoffset NULL,
        [IsDeleted] bit NOT NULL,
        [RoleId] uniqueidentifier NOT NULL,
        [PermissionId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_RolePermissions] PRIMARY KEY ([Id])
    );
END;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_RolePermissions_Roles_RoleId')
BEGIN
    ALTER TABLE [Domain].[RolePermissions] DROP CONSTRAINT [FK_RolePermissions_Roles_RoleId];
END;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_UserRoles_Roles_RoleId')
BEGIN
    ALTER TABLE [Domain].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Roles_RoleId];
END;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_UserRoles_Users_UserId')
BEGIN
    ALTER TABLE [Domain].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Users_UserId];
END;
GO

IF OBJECT_ID(N'[Domain].[Roles]', N'U') IS NOT NULL
BEGIN
    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    SELECT [Id], [Name], UPPER([Name]), CONVERT(nvarchar(36), NEWID())
    FROM [Domain].[Roles] AS source
    WHERE NOT EXISTS (SELECT 1 FROM [AspNetRoles] AS target WHERE target.[Id] = source.[Id])
      AND NOT EXISTS (SELECT 1 FROM [AspNetRoles] AS target WHERE target.[NormalizedName] = UPPER(source.[Name]));
END;
GO

IF OBJECT_ID(N'[Domain].[UserRoles]', N'U') IS NOT NULL
BEGIN
    IF EXISTS (
        SELECT 1
        FROM [Domain].[UserRoles] AS legacyUserRole
        LEFT JOIN [AspNetUsers] AS identityUser ON identityUser.[DomainUserId] = legacyUserRole.[UserId]
        LEFT JOIN [AspNetRoles] AS identityRole ON identityRole.[Id] = legacyUserRole.[RoleId]
        WHERE identityUser.[Id] IS NULL OR identityRole.[Id] IS NULL)
    BEGIN
        THROW 51000, 'Domain.UserRoles contains memberships that cannot be mapped to AspNetUserRoles. Verify AspNetUsers.DomainUserId and migrated AspNetRoles before rerunning.', 1;
    END;

    INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
    SELECT identityUser.[Id], legacyUserRole.[RoleId]
    FROM [Domain].[UserRoles] AS legacyUserRole
    INNER JOIN [AspNetUsers] AS identityUser ON identityUser.[DomainUserId] = legacyUserRole.[UserId]
    WHERE NOT EXISTS (
        SELECT 1
        FROM [AspNetUserRoles] AS target
        WHERE target.[UserId] = identityUser.[Id] AND target.[RoleId] = legacyUserRole.[RoleId]);

    DROP TABLE [Domain].[UserRoles];
END;
GO

IF OBJECT_ID(N'[Domain].[Roles]', N'U') IS NOT NULL
BEGIN
    DROP TABLE [Domain].[Roles];
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_AspNetRoleClaims_AspNetRoles_RoleId')
BEGIN
    ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
        FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_AspNetUserClaims_AspNetUsers_UserId')
BEGIN
    ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_AspNetUserLogins_AspNetUsers_UserId')
BEGIN
    ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_AspNetUserRoles_AspNetRoles_RoleId')
BEGIN
    ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
        FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_AspNetUserRoles_AspNetUsers_UserId')
BEGIN
    ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_AspNetUserTokens_AspNetUsers_UserId')
BEGIN
    ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
        FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_RolePermissions_AspNetRoles_RoleId')
BEGIN
    ALTER TABLE [Domain].[RolePermissions] ADD CONSTRAINT [FK_RolePermissions_AspNetRoles_RoleId]
        FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE [name] = N'FK_RolePermissions_Permissions_PermissionId')
BEGIN
    ALTER TABLE [Domain].[RolePermissions] ADD CONSTRAINT [FK_RolePermissions_Permissions_PermissionId]
        FOREIGN KEY ([PermissionId]) REFERENCES [Domain].[Permissions] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_AspNetRoleClaims_RoleId' AND [object_id] = OBJECT_ID(N'[AspNetRoleClaims]'))
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'RoleNameIndex' AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_AspNetUserClaims_UserId' AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_AspNetUserLogins_UserId' AND [object_id] = OBJECT_ID(N'[AspNetUserLogins]'))
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_AspNetUserRoles_RoleId' AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'EmailIndex' AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'UserNameIndex' AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_Permissions_Code' AND [object_id] = OBJECT_ID(N'[Domain].[Permissions]'))
BEGIN
    CREATE UNIQUE INDEX [IX_Permissions_Code] ON [Domain].[Permissions] ([Code]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_RolePermissions_PermissionId' AND [object_id] = OBJECT_ID(N'[Domain].[RolePermissions]'))
BEGIN
    CREATE INDEX [IX_RolePermissions_PermissionId] ON [Domain].[RolePermissions] ([PermissionId]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_RolePermissions_RoleId_PermissionId' AND [object_id] = OBJECT_ID(N'[Domain].[RolePermissions]'))
BEGIN
    CREATE UNIQUE INDEX [IX_RolePermissions_RoleId_PermissionId] ON [Domain].[RolePermissions] ([RoleId], [PermissionId]);
END;
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE [name] = N'IX_Users_Email' AND [object_id] = OBJECT_ID(N'[Domain].[Users]'))
BEGIN
    CREATE UNIQUE INDEX [IX_Users_Email] ON [Domain].[Users] ([Email]);
END;
GO

DECLARE @SeedCreatedDate datetimeoffset = SYSDATETIMEOFFSET();

IF EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'Users.Read')
    AND NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.read')
BEGIN
    UPDATE [Domain].[Permissions] SET [Code] = N'users.read' WHERE [Code] = N'Users.Read';
END;

IF EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'Users.Create')
    AND NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.create')
BEGIN
    UPDATE [Domain].[Permissions] SET [Code] = N'users.create' WHERE [Code] = N'Users.Create';
END;

IF EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'Users.Update')
    AND NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.update')
BEGIN
    UPDATE [Domain].[Permissions] SET [Code] = N'users.update' WHERE [Code] = N'Users.Update';
END;

IF EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'Users.Delete')
    AND NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.delete')
BEGIN
    UPDATE [Domain].[Permissions] SET [Code] = N'users.delete' WHERE [Code] = N'Users.Delete';
END;

DECLARE @AdministratorRoleId uniqueidentifier = '22222222-2222-2222-2222-222222222222';
DECLARE @UsersReadPermissionId uniqueidentifier = '11111111-1111-1111-1111-111111111111';
DECLARE @UsersCreatePermissionId uniqueidentifier = '11111111-1111-1111-1111-111111111112';
DECLARE @UsersUpdatePermissionId uniqueidentifier = '11111111-1111-1111-1111-111111111113';
DECLARE @UsersDeletePermissionId uniqueidentifier = '11111111-1111-1111-1111-111111111114';
DECLARE @PermissionsManagePermissionId uniqueidentifier = '11111111-1111-1111-1111-111111111115';

IF NOT EXISTS (SELECT 1 FROM [AspNetRoles] WHERE [NormalizedName] = N'ADMINISTRATOR')
BEGIN
    INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
    VALUES (@AdministratorRoleId, N'Administrator', N'ADMINISTRATOR', CONVERT(nvarchar(36), NEWID()));
END;
ELSE
BEGIN
    SELECT @AdministratorRoleId = [Id] FROM [AspNetRoles] WHERE [NormalizedName] = N'ADMINISTRATOR';
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.read')
BEGIN
    INSERT INTO [Domain].[Permissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Name], [Code], [Type])
    VALUES (@UsersReadPermissionId, NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, N'Users Read', N'users.read', 1);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.create')
BEGIN
    INSERT INTO [Domain].[Permissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Name], [Code], [Type])
    VALUES (@UsersCreatePermissionId, NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, N'Users Create', N'users.create', 2);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.update')
BEGIN
    INSERT INTO [Domain].[Permissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Name], [Code], [Type])
    VALUES (@UsersUpdatePermissionId, NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, N'Users Update', N'users.update', 3);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'users.delete')
BEGIN
    INSERT INTO [Domain].[Permissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Name], [Code], [Type])
    VALUES (@UsersDeletePermissionId, NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, N'Users Delete', N'users.delete', 4);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[Permissions] WHERE [Code] = N'permissions.manage')
BEGIN
    INSERT INTO [Domain].[Permissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Name], [Code], [Type])
    VALUES (@PermissionsManagePermissionId, NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, N'Permissions Manage', N'permissions.manage', 5);
END;

SELECT @UsersReadPermissionId = [Id] FROM [Domain].[Permissions] WHERE [Code] = N'users.read';
SELECT @UsersCreatePermissionId = [Id] FROM [Domain].[Permissions] WHERE [Code] = N'users.create';
SELECT @UsersUpdatePermissionId = [Id] FROM [Domain].[Permissions] WHERE [Code] = N'users.update';
SELECT @UsersDeletePermissionId = [Id] FROM [Domain].[Permissions] WHERE [Code] = N'users.delete';
SELECT @PermissionsManagePermissionId = [Id] FROM [Domain].[Permissions] WHERE [Code] = N'permissions.manage';

IF NOT EXISTS (SELECT 1 FROM [Domain].[RolePermissions] WHERE [RoleId] = @AdministratorRoleId AND [PermissionId] = @UsersReadPermissionId)
BEGIN
    INSERT INTO [Domain].[RolePermissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [RoleId], [PermissionId])
    VALUES ('33333333-3333-3333-3333-333333333331', NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, @AdministratorRoleId, @UsersReadPermissionId);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[RolePermissions] WHERE [RoleId] = @AdministratorRoleId AND [PermissionId] = @UsersCreatePermissionId)
BEGIN
    INSERT INTO [Domain].[RolePermissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [RoleId], [PermissionId])
    VALUES ('33333333-3333-3333-3333-333333333332', NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, @AdministratorRoleId, @UsersCreatePermissionId);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[RolePermissions] WHERE [RoleId] = @AdministratorRoleId AND [PermissionId] = @UsersUpdatePermissionId)
BEGIN
    INSERT INTO [Domain].[RolePermissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [RoleId], [PermissionId])
    VALUES ('33333333-3333-3333-3333-333333333333', NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, @AdministratorRoleId, @UsersUpdatePermissionId);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[RolePermissions] WHERE [RoleId] = @AdministratorRoleId AND [PermissionId] = @UsersDeletePermissionId)
BEGIN
    INSERT INTO [Domain].[RolePermissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [RoleId], [PermissionId])
    VALUES ('33333333-3333-3333-3333-333333333334', NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, @AdministratorRoleId, @UsersDeletePermissionId);
END;

IF NOT EXISTS (SELECT 1 FROM [Domain].[RolePermissions] WHERE [RoleId] = @AdministratorRoleId AND [PermissionId] = @PermissionsManagePermissionId)
BEGIN
    INSERT INTO [Domain].[RolePermissions] ([Id], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [RoleId], [PermissionId])
    VALUES ('33333333-3333-3333-3333-333333333335', NULL, @SeedCreatedDate, NULL, NULL, NULL, NULL, 0, @AdministratorRoleId, @PermissionsManagePermissionId);
END;
GO
IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260607000000_ManualInitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260607000000_ManualInitialCreate', N'8.0.6');
END;
GO

IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260607000100_RemoveDuplicateDomainRoles')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260607000100_RemoveDuplicateDomainRoles', N'8.0.6');
END;
GO

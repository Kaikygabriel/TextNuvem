IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Customer] (
    [Id] uniqueidentifier NOT NULL,
    [Email] VARCHAR(255) NOT NULL,
    [HashPassword] NVARCHAR(300) NOT NULL,
    [Name] VARCHAR(120) NOT NULL,
    [RefreshToken] TEXT NULL,
    [ExpiredRefreshToken] DATETIME2 NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id])
);

CREATE TABLE [Projects] (
    [Id] uniqueidentifier NOT NULL,
    [LastUpdate] datetime2 NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [Folders] TEXT NOT NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Projects_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Customer_Email] ON [Customer] ([Email]);

CREATE INDEX [IX_Projects_CustomerId] ON [Projects] ([CustomerId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260417134238_Initial', N'10.0.6');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Projects] ADD [IsFavorite] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260421002842_UpdateModel', N'10.0.6');

COMMIT;
GO


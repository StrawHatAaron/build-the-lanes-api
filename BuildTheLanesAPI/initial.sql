IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [id] int NOT NULL IDENTITY,
    [email] nvarchar(max) NULL,
    [password_hash] varbinary(max) NULL,
    [password_salt] varbinary(max) NULL,
    [first_name] nvarchar(max) NULL,
    [last_name] nvarchar(max) NULL,
    [roles] nvarchar(max) NULL,
    [amount_donated] nvarchar(max) NULL,
    [title] nvarchar(max) NULL,
    [type] nvarchar(max) NULL,
    [created] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200402103436_Initial', N'3.1.2');

GO


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Admins] (
    [id] int NOT NULL IDENTITY,
    [email] nvarchar(max) NULL,
    [password_hash] varbinary(max) NULL,
    [password_salt] varbinary(max) NULL,
    [first_name] nvarchar(max) NULL,
    [last_name] nvarchar(max) NULL,
    [roles] nvarchar(max) NULL,
    [title] nvarchar(max) NULL,
    [created] nvarchar(max) NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY ([id])
);

GO

CREATE TABLE [Donators] (
    [id] int NOT NULL IDENTITY,
    [email] nvarchar(max) NULL,
    [password_hash] varbinary(max) NULL,
    [password_salt] varbinary(max) NULL,
    [first_name] nvarchar(max) NULL,
    [last_name] nvarchar(max) NULL,
    [roles] nvarchar(max) NULL,
    [amount_donated] nvarchar(max) NULL,
    CONSTRAINT [PK_Donators] PRIMARY KEY ([id])
);

GO

CREATE TABLE [Engineers] (
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
    CONSTRAINT [PK_Engineers] PRIMARY KEY ([id])
);

GO

CREATE TABLE [Projects] (
    [project_number] int NOT NULL IDENTITY,
    [start_date] nvarchar(max) NULL,
    [status] nvarchar(max) NULL,
    [city] nvarchar(max) NULL,
    [zip_code] nvarchar(max) NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([project_number])
);

GO

CREATE TABLE [Staffs] (
    [id] int NOT NULL IDENTITY,
    [email] nvarchar(max) NULL,
    [password_hash] varbinary(max) NULL,
    [password_salt] varbinary(max) NULL,
    [first_name] nvarchar(max) NULL,
    [last_name] nvarchar(max) NULL,
    [roles] nvarchar(max) NULL,
    [title] nvarchar(max) NULL,
    [type] nvarchar(max) NULL,
    [created] nvarchar(max) NULL,
    CONSTRAINT [PK_Staffs] PRIMARY KEY ([id])
);

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
VALUES (N'20200408094834_Initial', N'3.1.2');

GO


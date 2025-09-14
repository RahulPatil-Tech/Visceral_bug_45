CREATE TABLE [dbo].[Users] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (MAX) NOT NULL,
    [Password]     NVARCHAR (MAX) NOT NULL,
    [Email]        NVARCHAR (MAX) NOT NULL,
    [Role]         NVARCHAR (MAX) NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [ProfileImage] NVARCHAR (MAX) NULL,
    [IsActive]     BIT            NOT NULL,
    [CreatedAt]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[Tasks] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (MAX) NOT NULL,
    [Description]  NVARCHAR (MAX) NOT NULL,
    [Status]       NVARCHAR (MAX) NOT NULL,
    [ProjectId]    INT            NOT NULL,
    [AssignedToId] INT            NULL,
    [CreatedById]  INT            NOT NULL,
    [CreatedAt]    DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


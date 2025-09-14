CREATE TABLE [dbo].[BugLogs] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [BugId]       INT            NOT NULL,
    [Status]      NVARCHAR (MAX) NOT NULL,
    [Comment]     NVARCHAR (MAX) NULL,
    [ChangedById] INT            NOT NULL,
    [ChangedAt]   DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_BugLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[TaskLogs] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [TaskId]      INT            NOT NULL,
    [Status]      NVARCHAR (MAX) NOT NULL,
    [Comment]     NVARCHAR (MAX) NULL,
    [ChangedById] INT            NOT NULL,
    [ChangedAt]   DATETIME2 (7)  NOT NULL,
    [WorkTaskId]  INT            NOT NULL,
    CONSTRAINT [PK_TaskLogs] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaskLogs_Tasks_WorkTaskId] FOREIGN KEY ([WorkTaskId]) REFERENCES [dbo].[Tasks] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TaskLogs_WorkTaskId]
    ON [dbo].[TaskLogs]([WorkTaskId] ASC);


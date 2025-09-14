CREATE TABLE [dbo].[ProjectTesters] (
    [TesterProjectsId] INT NOT NULL,
    [TestersId]        INT NOT NULL,
    CONSTRAINT [PK_ProjectTesters] PRIMARY KEY CLUSTERED ([TesterProjectsId] ASC, [TestersId] ASC),
    CONSTRAINT [FK_ProjectTesters_Projects_TesterProjectsId] FOREIGN KEY ([TesterProjectsId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectTesters_Users_TestersId] FOREIGN KEY ([TestersId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProjectTesters_TestersId]
    ON [dbo].[ProjectTesters]([TestersId] ASC);


CREATE TABLE [dbo].[ProjectDevelopers] (
    [DeveloperProjectsId] INT NOT NULL,
    [DevelopersId]        INT NOT NULL,
    CONSTRAINT [PK_ProjectDevelopers] PRIMARY KEY CLUSTERED ([DeveloperProjectsId] ASC, [DevelopersId] ASC),
    CONSTRAINT [FK_ProjectDevelopers_Projects_DeveloperProjectsId] FOREIGN KEY ([DeveloperProjectsId]) REFERENCES [dbo].[Projects] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProjectDevelopers_Users_DevelopersId] FOREIGN KEY ([DevelopersId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ProjectDevelopers_DevelopersId]
    ON [dbo].[ProjectDevelopers]([DevelopersId] ASC);


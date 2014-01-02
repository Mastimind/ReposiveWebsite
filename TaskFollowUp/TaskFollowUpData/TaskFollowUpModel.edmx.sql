
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/31/2013 17:06:07
-- Generated from EDMX file: C:\Workspaces\TaskFollowUp\TaskFollowUpData\TaskFollowUpModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [tempdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SprintTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_SprintTask];
GO
IF OBJECT_ID(N'[dbo].[FK_DeveloperTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_DeveloperTask];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskBugs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bugs] DROP CONSTRAINT [FK_TaskBugs];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Developers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Developers];
GO
IF OBJECT_ID(N'[dbo].[Sprints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sprints];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[Bugs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bugs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Developers'
CREATE TABLE [dbo].[Developers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sprints'
CREATE TABLE [dbo].[Sprints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SprintName] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NULL,
    [SprintId] int  NOT NULL,
    [DeveloperId] int  NULL
);
GO

-- Creating table 'Bugs'
CREATE TABLE [dbo].[Bugs] (
    [Id] int  NOT NULL,
    [Tilte] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [TaskId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Developers'
ALTER TABLE [dbo].[Developers]
ADD CONSTRAINT [PK_Developers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sprints'
ALTER TABLE [dbo].[Sprints]
ADD CONSTRAINT [PK_Sprints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Bugs'
ALTER TABLE [dbo].[Bugs]
ADD CONSTRAINT [PK_Bugs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SprintId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_SprintTask]
    FOREIGN KEY ([SprintId])
    REFERENCES [dbo].[Sprints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SprintTask'
CREATE INDEX [IX_FK_SprintTask]
ON [dbo].[Tasks]
    ([SprintId]);
GO

-- Creating foreign key on [DeveloperId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_DeveloperTask]
    FOREIGN KEY ([DeveloperId])
    REFERENCES [dbo].[Developers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperTask'
CREATE INDEX [IX_FK_DeveloperTask]
ON [dbo].[Tasks]
    ([DeveloperId]);
GO

-- Creating foreign key on [TaskId] in table 'Bugs'
ALTER TABLE [dbo].[Bugs]
ADD CONSTRAINT [FK_TaskBugs]
    FOREIGN KEY ([TaskId])
    REFERENCES [dbo].[Tasks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskBugs'
CREATE INDEX [IX_FK_TaskBugs]
ON [dbo].[Bugs]
    ([TaskId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
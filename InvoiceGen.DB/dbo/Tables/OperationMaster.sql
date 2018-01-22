CREATE TABLE [dbo].[OperationMaster] (
    [ID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [OperationName] NVARCHAR (50)  NOT NULL,
    [Description]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


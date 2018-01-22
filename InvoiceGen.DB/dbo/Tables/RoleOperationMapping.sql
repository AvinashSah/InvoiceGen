CREATE TABLE [dbo].[RoleOperationMapping] (
    [ID]          BIGINT IDENTITY (1, 1) NOT NULL,
    [RoleId]      BIGINT NULL,
    [OperationId] BIGINT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([OperationId]) REFERENCES [dbo].[OperationMaster] ([ID]),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[RoleMaster] ([ID])
);


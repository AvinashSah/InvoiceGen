CREATE TABLE [dbo].[UserRoleMapping] (
    [Id]     INT    IDENTITY (1, 1) NOT NULL,
    [UserId] BIGINT NULL,
    [RoleId] BIGINT NULL,
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[RoleMaster] ([ID]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserMaster] ([ID])
);


CREATE TABLE [dbo].[UserMaster] (
    [ID]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserName]  NVARCHAR (50) NOT NULL,
    [Password]  NVARCHAR (50) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [EmailId]   NVARCHAR (50) NULL,
    [Mobile]    INT           NOT NULL,
    [IsActive]  BIT           NOT NULL,
    [CreatedOn] DATETIME      DEFAULT (getdate()) NULL,
    [UpdatedOn] DATETIME      NULL,
    [CreatedBy] BIGINT        NULL,
    [UpdatedBy] BIGINT        NULL,
    [EmpId]     NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


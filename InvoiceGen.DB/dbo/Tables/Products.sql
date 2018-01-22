CREATE TABLE [dbo].[Products] (
    [ID]             BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (500)  NULL,
    [HSNCode]        NVARCHAR (200)  NULL,
    [SACCode]        NVARCHAR (200)  NULL,
    [Description]    NVARCHAR (1000) NULL,
    [UoM]            NVARCHAR (100)  NULL,
    [PurchaseRate]   NVARCHAR (100)  NULL,
    [CessPercentage] NVARCHAR (100)  NULL,
    [GSTPercentage]  NVARCHAR (100)  NULL,
    [SalesRate]      NVARCHAR (100)  NULL,
    [IsActive]       BIT             DEFAULT ((1)) NULL,
    [CreatedOn]      DATETIME        DEFAULT (getdate()) NULL,
    [UpdatedOn]      DATETIME        NULL,
    [CreatedBy]      BIGINT          NULL,
    [UpdatedBy]      BIGINT          NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


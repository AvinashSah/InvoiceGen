CREATE TABLE [dbo].[CustomerProductMapping] (
    [ID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [ProductID]    BIGINT         NULL,
    [CustomerID]   BIGINT         NULL,
    [SalesRate]    NVARCHAR (100) NULL,
    [PurchaseRate] NVARCHAR (100) NULL,
    [IsActive]     BIT            DEFAULT ((1)) NULL,
    [CreatedOn]    DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedOn]    DATETIME       NULL,
    [CreatedBy]    BIGINT         NULL,
    [UpdatedBy]    BIGINT         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([ID]),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[ProductsMaster] ([ID])
);




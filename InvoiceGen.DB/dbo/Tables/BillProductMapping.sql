CREATE TABLE [dbo].[BillProductMapping] (
    [ID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [BillID]       BIGINT         NULL,
    [ProductID]    BIGINT         NULL,
    [RateCharged]  NVARCHAR (100) NULL,
    [Qyantity]     NVARCHAR (100) NULL,
    [TotalAmount]  NVARCHAR (100) NULL,
    [DiscountPerc] NVARCHAR (100) NULL,
    [CessPerc]     NVARCHAR (100) NULL,
    [IGST]         NVARCHAR (100) NULL,
    [CGST]         NVARCHAR (100) NULL,
    [SGST]         NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([BillID]) REFERENCES [dbo].[BillMaster] ([ID]),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID])
);


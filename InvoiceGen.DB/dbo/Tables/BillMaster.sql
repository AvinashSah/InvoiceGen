CREATE TABLE [dbo].[BillMaster] (
    [ID]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [BillNo]           NVARCHAR (500)  NULL,
    [PaymentDueDate]   DATETIME        NULL,
    [BillFromCustID]   BIGINT          NULL,
    [BillToCustID]     BIGINT          NULL,
    [ShippAddress]     NVARCHAR (1000) NULL,
    [BillAddress]      NVARCHAR (1000) NULL,
    [TermsConditions]  NVARCHAR (MAX)  NULL,
    [NotesForCustomer] NVARCHAR (1000) NULL,
    [CreatedOn]        DATETIME        DEFAULT (getdate()) NULL,
    [UpdatedOn]        DATETIME        NULL,
    [CreatedBy]        BIGINT          NULL,
    [UpdatedBy]        BIGINT          NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([BillFromCustID]) REFERENCES [dbo].[Customers] ([ID]),
    FOREIGN KEY ([BillToCustID]) REFERENCES [dbo].[Customers] ([ID])
);


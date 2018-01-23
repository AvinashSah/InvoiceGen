CREATE TABLE [dbo].[BillMaster] (
    [ID]               BIGINT          IDENTITY (1, 1) NOT NULL,
    [BillNo]           NVARCHAR (500)  NULL,
    [PaymentDueDate]   DATETIME        NULL,
    [BillFromCustID]   BIGINT          NULL,
    [BillToCustID]     BIGINT          NULL,
    [BillAddL1]        NVARCHAR (100)  NULL,
    [BillAddL2]        NVARCHAR (100)  NULL,
    [BillAddCityID]    BIGINT          NULL,
    [BillStateID]      BIGINT          NULL,
    [BillAddZip]       NVARCHAR (100)  NULL,
    [ShipAddL1]        NVARCHAR (100)  NULL,
    [ShipAddL2]        NVARCHAR (100)  NULL,
    [ShipAddCityID]    BIGINT          NULL,
    [ShipStateID]      BIGINT          NULL,
    [ShipAddZip]       NVARCHAR (100)  NULL,
    [TermsConditions]  NVARCHAR (MAX)  NULL,
    [NotesForCustomer] NVARCHAR (1000) NULL,
    [CreatedOn]        DATETIME        DEFAULT (getdate()) NULL,
    [UpdatedOn]        DATETIME        NULL,
    [CreatedBy]        BIGINT          NULL,
    [UpdatedBy]        BIGINT          NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([BillAddCityID]) REFERENCES [dbo].[Cities] ([ID]),
    FOREIGN KEY ([BillFromCustID]) REFERENCES [dbo].[Customers] ([ID]),
    FOREIGN KEY ([BillStateID]) REFERENCES [dbo].[States] ([ID]),
    FOREIGN KEY ([BillToCustID]) REFERENCES [dbo].[Customers] ([ID]),
    FOREIGN KEY ([ShipAddCityID]) REFERENCES [dbo].[Cities] ([ID]),
    FOREIGN KEY ([ShipStateID]) REFERENCES [dbo].[States] ([ID])
);




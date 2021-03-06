﻿CREATE TABLE [dbo].[Customers] (
    [ID]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [EmailID]          NVARCHAR (200) NULL,
    [CustomerType]     NVARCHAR (100) NOT NULL,
    [ContactName]      NVARCHAR (400) NOT NULL,
    [ContactNumber]    NVARCHAR (30)  NULL,
    [GSTIN]            NVARCHAR (40)  NULL,
    [PAN]              NVARCHAR (40)  NULL,
    [BillAddL1]        NVARCHAR (100) NULL,
    [BillAddL2]        NVARCHAR (100) NULL,
    [BillAddCityID]    BIGINT         NULL,
    [BillStateID]      BIGINT         NULL,
    [BillAddZip]       NVARCHAR (100) NULL,
    [ShipAddL1]        NVARCHAR (100) NULL,
    [ShipAddL2]        NVARCHAR (100) NULL,
    [ShipAddCityID]    BIGINT         NULL,
    [ShipStateID]      BIGINT         NULL,
    [ShipAddZip]       NVARCHAR (100) NULL,
    [IsActive]         BIT            DEFAULT ((1)) NULL,
    [CreatedOn]        DATETIME       DEFAULT (getdate()) NULL,
    [UpdatedOn]        DATETIME       NULL,
    [CreatedBy]        BIGINT         NULL,
    [UpdatedBy]        BIGINT         NULL,
    [CustomerLogoPath] NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([BillAddCityID]) REFERENCES [dbo].[Cities] ([ID]),
    FOREIGN KEY ([BillStateID]) REFERENCES [dbo].[States] ([ID]),
    FOREIGN KEY ([ShipAddCityID]) REFERENCES [dbo].[Cities] ([ID]),
    FOREIGN KEY ([ShipStateID]) REFERENCES [dbo].[States] ([ID])
);






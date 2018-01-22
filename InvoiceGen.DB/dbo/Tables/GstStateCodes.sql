CREATE TABLE [dbo].[GstStateCodes] (
    [ID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [GSTStateCode] NVARCHAR (30) NULL,
    [IsActive]     BIT           NULL,
    [StateID]      BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([StateID]) REFERENCES [dbo].[States] ([ID])
);


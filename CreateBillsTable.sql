SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Bills](
	[Company] [varchar](50) NOT NULL,
	[DueDate] [date] NOT NULL,
	[Paid] [varchar](3) NULL,
	[IndexNumber] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[DatePaid] [smalldatetime] NULL,
	[Amount] [float] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Bills] ADD  CONSTRAINT [DF_Bills_Paid]  DEFAULT ('No') FOR [Paid]
GO

ALTER TABLE [dbo].[Bills] ADD  CONSTRAINT [DF_Bills_IndexNumber]  DEFAULT (newid()) FOR [IndexNumber]
GO



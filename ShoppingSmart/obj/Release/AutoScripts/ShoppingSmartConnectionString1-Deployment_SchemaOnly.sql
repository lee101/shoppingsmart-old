CREATE USER [lee1] FOR LOGIN [lee1] WITH DEFAULT_SCHEMA=[lee1]
GO
CREATE SCHEMA [lee1] AUTHORIZATION [lee1]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[price] [int] NULL,
	[description] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[url] [varchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[largeimg] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[smallimg] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[largeimgwidth] [int] NULL,
	[largeimgheight] [int] NULL,
	[smallimgwidth] [int] NULL,
	[smallimgheight] [int] NULL,
	[company] [int] NOT NULL,
 CONSTRAINT [PK_dbo.products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cat](
	[prodid] [int] NOT NULL,
	[catid] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_dbo.cat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
ALTER TABLE [dbo].[cat]  WITH CHECK ADD  CONSTRAINT [product_cat] FOREIGN KEY([prodid])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[cat] CHECK CONSTRAINT [product_cat]
GO

USE [{0}]
GO
/****** Object:  Table [dbo].[MultiversionDocument_DocumentDefinition]    Script Date: 08/19/2008 13:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_DocumentDefinition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MultiversionDocument_DocumentDefinition](
	[ID] [int] NOT NULL,
	[Namespace] [nvarchar](128) NULL,
	[Name] [nvarchar](128) NULL,
 CONSTRAINT [PK_MultiversionDocument_DocumentDefinition] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MultiversionDocument_DocumentState]    Script Date: 08/19/2008 13:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_DocumentState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MultiversionDocument_DocumentState](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FriendlyName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_MultiversionDocument_DocumentState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MultiversionDocument_DocumentVersionState]    Script Date: 08/19/2008 13:32:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_DocumentVersionState]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MultiversionDocument_DocumentVersionState](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FriendlyName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_MultiversionDocument_DocumentVersionState] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MultiversionDocument_Document]    Script Date: 08/19/2008 13:32:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_Document]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MultiversionDocument_Document](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Metadata_MultiversionDocumentID] [int] NOT NULL,
	[DocumentStateID] [int] NOT NULL,
	[CurrentVersionID] [int] NULL,
	[DateTimeCreated] [datetime] NOT NULL CONSTRAINT [DF_MultiversionDocument_Document_DocumentDateTime]  DEFAULT (getdate()),
	[DateTimeLastModified] [datetime] NOT NULL CONSTRAINT [DF_MultiversionDocument_Document_DateTimeLastModified]  DEFAULT (getdate()),
	[DateTimeCommitted] [datetime] NULL,
 CONSTRAINT [PK_MultiversionDocument_Document] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MultiversionDocument_DocumentVersion]    Script Date: 08/19/2008 13:32:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_DocumentVersion]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MultiversionDocument_DocumentVersion](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentID] [int] NOT NULL,
	[DocumentVersionStateID] [int] NOT NULL,
	[VersionDateTime] [datetime] NOT NULL CONSTRAINT [DF_MultiversionDocument_DocumentVersion_VersionDateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_MultiversionDocument_DocumentVersion] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_MultiversionDocument_Document_Metadata_MultiversionDocument]    Script Date: 08/19/2008 13:32:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultiversionDocument_Document_Metadata_MultiversionDocument]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_Document]'))
ALTER TABLE [dbo].[MultiversionDocument_Document]  WITH CHECK ADD  CONSTRAINT [FK_MultiversionDocument_Document_Metadata_MultiversionDocument] FOREIGN KEY([Metadata_MultiversionDocumentID])
REFERENCES [dbo].[MultiversionDocument_DocumentDefinition] ([ID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[MultiversionDocument_Document] CHECK CONSTRAINT [FK_MultiversionDocument_Document_Metadata_MultiversionDocument]
GO
/****** Object:  ForeignKey [FK_MultiversionDocument_Document_MultiversionDocument_DocumentState]    Script Date: 08/19/2008 13:32:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultiversionDocument_Document_MultiversionDocument_DocumentState]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_Document]'))
ALTER TABLE [dbo].[MultiversionDocument_Document]  WITH CHECK ADD  CONSTRAINT [FK_MultiversionDocument_Document_MultiversionDocument_DocumentState] FOREIGN KEY([DocumentStateID])
REFERENCES [dbo].[MultiversionDocument_DocumentState] ([ID])
GO
ALTER TABLE [dbo].[MultiversionDocument_Document] CHECK CONSTRAINT [FK_MultiversionDocument_Document_MultiversionDocument_DocumentState]
GO
/****** Object:  ForeignKey [FK_MultiversionDocument_Document_MultiversionDocument_DocumentVersion_CurrentVersion]    Script Date: 08/19/2008 13:32:08 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultiversionDocument_Document_MultiversionDocument_DocumentVersion_CurrentVersion]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_Document]'))
ALTER TABLE [dbo].[MultiversionDocument_Document]  WITH CHECK ADD  CONSTRAINT [FK_MultiversionDocument_Document_MultiversionDocument_DocumentVersion_CurrentVersion] FOREIGN KEY([CurrentVersionID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersion] ([ID])
GO
ALTER TABLE [dbo].[MultiversionDocument_Document] CHECK CONSTRAINT [FK_MultiversionDocument_Document_MultiversionDocument_DocumentVersion_CurrentVersion]
GO
/****** Object:  ForeignKey [FK_MultiversionDocument_DocumentVersion_MultiversionDocument_Document]    Script Date: 08/19/2008 13:32:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultiversionDocument_DocumentVersion_MultiversionDocument_Document]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_DocumentVersion]'))
ALTER TABLE [dbo].[MultiversionDocument_DocumentVersion]  WITH CHECK ADD  CONSTRAINT [FK_MultiversionDocument_DocumentVersion_MultiversionDocument_Document] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[MultiversionDocument_Document] ([ID])
GO
ALTER TABLE [dbo].[MultiversionDocument_DocumentVersion] CHECK CONSTRAINT [FK_MultiversionDocument_DocumentVersion_MultiversionDocument_Document]
GO
/****** Object:  ForeignKey [FK_MultiversionDocument_DocumentVersion_MultiversionDocument_DocumentVersionState]    Script Date: 08/19/2008 13:32:12 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MultiversionDocument_DocumentVersion_MultiversionDocument_DocumentVersionState]') AND parent_object_id = OBJECT_ID(N'[dbo].[MultiversionDocument_DocumentVersion]'))
ALTER TABLE [dbo].[MultiversionDocument_DocumentVersion]  WITH CHECK ADD  CONSTRAINT [FK_MultiversionDocument_DocumentVersion_MultiversionDocument_DocumentVersionState] FOREIGN KEY([DocumentVersionStateID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersionState] ([ID])
GO
ALTER TABLE [dbo].[MultiversionDocument_DocumentVersion] CHECK CONSTRAINT [FK_MultiversionDocument_DocumentVersion_MultiversionDocument_DocumentVersionState]
GO
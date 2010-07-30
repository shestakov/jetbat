USE [{0}]
GO
/****** Object:  Table [dbo].[Metadata_DateTimeFormat]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DateTimeFormat](
	[ID] [tinyint] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Metadata_DateTimeFormat] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_GetObjectTypeID]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_GetObjectTypeID]
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128),
	@ObjectTypeID INT OUTPUT
AS
SELECT 
	@ObjectTypeID = Metadata_Object.ObjectTypeID
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name

IF @ObjectTypeID IS NULL
BEGIN
	DECLARE @ErrorMessage NVARCHAR (2000);
	SET @ErrorMessage = 'Объекта [' + @Namespace + '].[' + @Name + '] не существует'
	RAISERROR(@ErrorMessage, 16, 1)
	RETURN
END
GO
/****** Object:  Table [dbo].[Metadata_DBView]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBView](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [PK_SystemView] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_ObjectType]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_ObjectType](
	[ID] [smallint] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [MetadataObjectType_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadObjectList]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadObjectList]
AS
SELECT 
	Metadata_Namespace.Name AS NamespaceName,
	Metadata_Object.Name AS Name,
	Metadata_Object.ObjectTypeID
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
GO
/****** Object:  Table [dbo].[Metadata_DBStoredProcedure]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBStoredProcedure](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Valid] [bit] NOT NULL,
	[Existent] [bit] NOT NULL,
 CONSTRAINT [SystemStoredProcedure_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_Namespace]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_Namespace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [MetadataNamespace_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DataType]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DataType](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[HasLength] [bit] NOT NULL,
	[HasPrecisionAndScale] [bit] NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [MetadataType_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetMultiversionDocumentTypeID]    Script Date: 09/06/2008 23:53:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetMultiversionDocumentTypeID] 
(
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128)
)
RETURNS INT
AS
BEGIN
	DECLARE @Result INT
	SELECT @Result = Metadata_Object.ID FROM Metadata_Object 
		INNER JOIN Metadata_Namespace
			ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
	WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name AND ObjectTypeID = 3
	RETURN @Result
END
GO
/****** Object:  Table [dbo].[Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_Object](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectTypeID] [smallint] NOT NULL,
	[NamespaceID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [MetadataObject_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [MetadataObject_UC1] UNIQUE NONCLUSTERED 
(
	[NamespaceID] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBTableColumn]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBTableColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBTableID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[DataTypeID] [int] NOT NULL,
	[IsPrimaryKeyMember] [bit] NOT NULL,
	[IsForeignKeyMember] [bit] NULL,
	[AllowNull] [bit] NOT NULL,
	[IsIdentity] [bit] NOT NULL,
	[MaxLength] [int] NOT NULL,
	[Precision] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [PK_SystemTableColumn] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_ObjectAttribute]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_ObjectAttribute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[DataTypeID] [int] NOT NULL,
	[DBTableColumnID] [int] NULL,
	[DateTimeFormatID] [tinyint] NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsNullable] [bit] NOT NULL,
	[IsReadOnly] [bit] NOT NULL,
	[IsExternal] [bit] NOT NULL,
	[IsUserVisible] [bit] NOT NULL,
	[IsPrimaryKeyMember] [bit] NOT NULL,
	[MaxLength] [int] NOT NULL,
	[Precision] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[UILabel] [nvarchar](200) NOT NULL,
	[UIPreferredWidth] [int] NOT NULL,
	[UIPreferredIndex] [int] NOT NULL,
	[UIAllowMultilineText] [bit] NOT NULL
 CONSTRAINT [MetadataEntityAttribute_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBViewColumn]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBViewColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBViewID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[DataTypeID] [int] NOT NULL,
	[MaxLength] [int] NOT NULL,
	[Precision] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [PK_Metadata_DBViewColumn] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBTable]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBTable](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[DeleteFlagDBTableColumnID] [int] NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [PK_SystemTable] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_ObjectAction]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_ObjectAction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Enabled] [bit] NOT NULL,
	[UIFullText] [nvarchar](200) NOT NULL,
	[UIBriefText] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Metadata_ObjectAction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_ObjectMethod]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_ObjectMethod](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[DBStoredProcedureID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[ReturnsXMLErrorList] [bit] NOT NULL,
 CONSTRAINT [PK_Metadata_ObjectMethod] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_MultiversionDocument]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_MultiversionDocument](
	[ID] [int] NOT NULL,
	[DBTableID] [int] NOT NULL,
	[DBViewID] [int] NOT NULL,
	[ObjectMethodIDCreate] [int] NULL,
	[ObjectMethodIDStartEdit] [int] NULL,
	[ObjectMethodIDUpdateVersion] [int] NULL,
	[ObjectMethodIDConfirmEdit] [int] NULL,
	[ObjectMethodIDCancelEdit] [int] NULL,
	[ObjectMethodIDDelete] [int] NULL,
	[ObjectMethodIDLoad] [int] NULL,
	[ObjectMethodIDCommit] [int] NULL,
	[ObjectMethodIDRollback] [int] NULL,
	[ObjectActionIDCreate] [int] NULL,
	[ObjectActionIDStartEdit] [int] NULL,
	[ObjectActionIDConfirmEdit] [int] NULL,
	[ObjectActionIDCancelEdit] [int] NULL,
	[ObjectActionIDDelete] [int] NULL,
	[ObjectActionIDCommit] [int] NULL,
	[ObjectActionIDRollback] [int] NULL,
	[ObjectActionIDPick] [int] NULL,
 CONSTRAINT [PK_Metadata_MultiversionDocument] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBForeignKey]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBForeignKey](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[DBTableIDParent] [int] NOT NULL,
	[DBTableIDChild] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [PK_MetadataEntityRelation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_PlainObject]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_PlainObject](
	[ID] [int] NOT NULL,
	[ParentObjectID] [int] NULL,
	[DBForeignKeyToParentObjectID] [int] NULL,
	[DBTableID] [int] NOT NULL,
	[DBViewID] [int] NOT NULL,
	[ObjectMethodIDInsert] [int] NULL,
	[ObjectMethodIDUpdate] [int] NULL,
	[ObjectMethodIDDelete] [int] NULL,
	[ObjectMethodIDLoad] [int] NULL,
	[ObjectMethodIDRestore] [int] NULL,
	[ObjectMethodIDCopyByParentObject] [int] NULL,
	[ObjectMethodIDDeleteByParentObject] [int] NULL,
	[ObjectActionIDInsert] [int] NULL,
	[ObjectActionIDUpdate] [int] NULL,
	[ObjectActionIDDelete] [int] NULL,
	[ObjectActionIDRestore] [int] NULL,
	[ObjectActionIDView] [int] NULL,
	[ObjectActionIDPick] [int] NULL,
	[UIEditorName] [nvarchar](200) NULL,
	[LogicalDeletionDBTableColumnID] [int] NULL,
 CONSTRAINT [MetadataEntity_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_MultiversionDocumentListView]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_MultiversionDocumentListView](
	[ID] [int] NOT NULL,
	[MultiversionDocumentID] [int] NOT NULL,
	[DBViewID] [int] NOT NULL,
	[ObjectMethodIDLoadList] [int] NULL,
	[ObjectActionIDLoadList] [int] NULL,
	[UIListCaption] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Metadata_MultiversionDocumentListView] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_PlainObjectListView]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_PlainObjectListView](
	[ID] [int] NOT NULL,
	[EntityID] [int] NOT NULL,
	[DBViewID] [int] NOT NULL,
	[ObjectMethodIDLoadList] [int] NULL,
	[ObjectActionIDLoadList] [int] NULL,
	[UIListCaption] [nvarchar](200) NOT NULL,
 CONSTRAINT [MetadataEntityList_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBStoredProcedureParameter]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBStoredProcedureParameter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBStoredProcedureID] [int] NOT NULL,
	[DataTypeID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[IsOutput] [bit] NOT NULL,
	[MaxLength] [int] NOT NULL,
	[Precision] [int] NOT NULL,
	[Scale] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [SystemStoredProcedureParameter_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_ComplexObjectAttribute]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_ComplexObjectAttribute](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[DBForeignKeyID] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FriendlyName] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[UILabel] [nvarchar](200) NOT NULL,
	[UIPreferredIndex] [int] NOT NULL,
	[Required] [bit] NOT NULL,
 CONSTRAINT [PK_Metadata_ComplexObjectAttribute] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBStoredProcedureParameterBinding]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectID] [int] NOT NULL,
	[DBStoredProcedureParameterID] [int] NOT NULL,
	[ObjectAttributeID] [int] NULL,
	[AlternativeName] [nvarchar](128) NULL,
 CONSTRAINT [MetadataStoredProcedureParameterBinding_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_StoredQuery]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_StoredQuery](
	[ID] [int] NOT NULL,
	[ObjectMethodIDLoadList] [int] NULL,
	[ObjectActionIDLoadList] [int] NULL,
	[UIListCaption] [nvarchar](200) NOT NULL,
	[PredefinedAttributes] [bit] NOT NULL,
 CONSTRAINT [Metadata_StoredQuery_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metadata_DBForeignKeyColumn]    Script Date: 09/06/2008 23:53:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metadata_DBForeignKeyColumn](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DBForeignKeyID] [int] NOT NULL,
	[DBTableColumnIDPrimaryKey] [int] NOT NULL,
	[DBTableColumnIDForeignKey] [int] NOT NULL,
	[Valid] [bit] NOT NULL,
 CONSTRAINT [PK_MetadataEntityRelationAttribute] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_ClearMetadata]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_ClearMetadata]
AS
SET XACT_ABORT ON
BEGIN TRANSACTION

DELETE FROM Metadata_DBStoredProcedureParameterBinding

DELETE FROM Metadata_MultiversionDocumentChildListView

UPDATE Metadata_MultiversionDocumentChildEntry SET
	ObjectMethodIDInsert = NULL,
	ObjectMethodIDUpdate = NULL,
	ObjectMethodIDDelete = NULL,
	ObjectMethodIDLoad = NULL

DELETE FROM Metadata_MultiversionDocumentChildEntry 

UPDATE Metadata_MultiversionDocumentListView SET
	ObjectMethodIDLoadList = NULL

DELETE FROM Metadata_MultiversionDocumentListView 

UPDATE Metadata_MultiversionDocument SET 
	ObjectMethodIDCreate = NULL,
	ObjectMethodIDStartEdit = NULL,
	ObjectMethodIDUpdateVersion = NULL,
	ObjectMethodIDConfirmEdit = NULL,
	ObjectMethodIDCancelEdit = NULL,
	ObjectMethodIDDelete = NULL,
	ObjectMethodIDLoad = NULL,
	ObjectMethodIDCommit = NULL,
	ObjectMethodIDRollback = NULL,
	ObjectActionIDCreate = NULL,
	ObjectActionIDStartEdit = NULL,
	ObjectActionIDConfirmEdit = NULL,
	ObjectActionIDCancelEdit = NULL,
	ObjectActionIDDelete = NULL,
	ObjectActionIDCommit = NULL,
	ObjectActionIDRollback = NULL,
	ObjectActionIDPick = NULL

DELETE FROM Metadata_MultiversionDocument

UPDATE Metadata_PlainObject SET
	ObjectMethodIDInsert = NULL,
	ObjectMethodIDUpdate = NULL,
	ObjectMethodIDDelete = NULL,
	ObjectMethodIDLoad = NULL,
	ObjectMethodIDRestore = NULL,
	ObjectMethodIDCopyByParentObject = NULL,
	ObjectMethodIDDeleteByParentObject = NULL

UPDATE Metadata_PlainObjectListView SET
	ObjectMethodIDLoadList = NULL

UPDATE Metadata_PlainObject SET
	ObjectActionIDInsert = NULL,
	ObjectActionIDUpdate = NULL,
	ObjectActionIDDelete = NULL,
	ObjectActionIDRestore = NULL,
	ObjectActionIDView = NULL,
	ObjectActionIDPick = NULL

UPDATE Metadata_PlainObjectListView SET
	ObjectActionIDLoadList = NULL

DELETE FROM Metadata_ObjectMethod
DELETE FROM Metadata_ObjectAction

DELETE FROM Metadata_ComplexObjectAttribute
DELETE FROM Metadata_ObjectAttribute
DELETE FROM Metadata_PlainObjectListView
DELETE FROM Metadata_PlainObject
DELETE FROM Metadata_Object

UPDATE Metadata_DBTable SET
	DeleteFlagDBTableColumnID = NULL

DELETE FROM Metadata_DBStoredProcedureParameter
DELETE FROM Metadata_DBStoredProcedure
DELETE FROM Metadata_DBForeignKeyColumn
DELETE FROM Metadata_DBForeignKey
DELETE FROM Metadata_DBTableColumn
DELETE FROM Metadata_DBTable
DELETE FROM Metadata_DBViewColumn
DELETE FROM Metadata_DBView
COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SelectViewList]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectViewList]
AS
SELECT 
	Name,
	Description
FROM Metadata_DBView
WHERE Valid = 1

SELECT
	Metadata_DBView.Name AS ViewName,
	Metadata_DBViewColumn.Name,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_DBViewColumn.MaxLength,
	Metadata_DBViewColumn.Precision,
	Metadata_DBViewColumn.Scale,
	Metadata_DBViewColumn.Description
FROM Metadata_DBViewColumn
	INNER JOIN Metadata_DataType ON Metadata_DBViewColumn.DataTypeID = Metadata_DataType.ID
	INNER JOIN Metadata_DBView ON Metadata_DBViewColumn.DBViewID = Metadata_DBView.ID	
WHERE Metadata_DBViewColumn.Valid = 1 AND Metadata_DBView.Valid = 1
GO
/****** Object:  StoredProcedure [dbo].[SelectStoredProcedureList]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectStoredProcedureList]
AS
SELECT 
	Name,
	Description
FROM Metadata_DBStoredProcedure
WHERE Existent = 1

SELECT
	Metadata_DBStoredProcedure.Name AS StoredProcedureName,
	Metadata_DBStoredProcedureParameter.Name,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_DBStoredProcedureParameter.IsOutput,
	Metadata_DBStoredProcedureParameter.MaxLength,
	Metadata_DBStoredProcedureParameter.Precision,
	Metadata_DBStoredProcedureParameter.Scale,
	Metadata_DBStoredProcedureParameter.Description
FROM Metadata_DBStoredProcedureParameter
	INNER JOIN Metadata_DataType ON Metadata_DBStoredProcedureParameter.DataTypeID = Metadata_DataType.ID
	INNER JOIN Metadata_DBStoredProcedure ON Metadata_DBStoredProcedureParameter.DBStoredProcedureID = Metadata_DBStoredProcedure.ID	
WHERE Metadata_DBStoredProcedure.Existent = 1 --AND Metadata_DBStoredProcedureParameter.Valid = 1
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadChildObjectList]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadChildObjectList]
(
	@ObjectNamespace NVARCHAR(128),
	@ObjectName NVARCHAR(128)
)
AS
SELECT
	ChildObjectNamespace.Name AS ObjectNamespace,
	ChildObject.[Name] AS ObjectName,
	dbo.Metadata_DBForeignKey.[Name] AS ForeignKeyToParentObjectName,
	DBStoredProcedureCopyByParentObject.[Name] AS CopyByParentObjectStoredProcedureName,
	DBStoredProcedureDeleteByParentObject.[Name] AS DeleteByParentObjectStoredProcedureName
FROM dbo.Metadata_Object
	INNER JOIN dbo.Metadata_Namespace ON dbo.Metadata_Object.NamespaceID = dbo.Metadata_Namespace.ID
	INNER JOIN dbo.Metadata_PlainObject ON dbo.Metadata_Object.ID = dbo.Metadata_PlainObject.ParentObjectID
	INNER JOIN Metadata_Object ChildObject ON dbo.Metadata_PlainObject.ID = ChildObject.ID
	INNER JOIN dbo.Metadata_Namespace ChildObjectNamespace ON ChildObject.NamespaceID = ChildObjectNamespace.ID
	INNER JOIN dbo.Metadata_DBForeignKey ON dbo.Metadata_PlainObject.DBForeignKeyToParentObjectID = dbo.Metadata_DBForeignKey.ID
	LEFT JOIN dbo.Metadata_ObjectMethod ObjectMethodCopyByParentObject ON dbo.Metadata_PlainObject.ObjectMethodIDCopyByParentObject = ObjectMethodCopyByParentObject.ID
	LEFT JOIN dbo.Metadata_DBStoredProcedure DBStoredProcedureCopyByParentObject ON ObjectMethodCopyByParentObject.DBStoredProcedureID = DBStoredProcedureCopyByParentObject.ID
	LEFT JOIN dbo.Metadata_ObjectMethod ObjectMethodDeleteByParentObject ON dbo.Metadata_PlainObject.ObjectMethodIDDeleteByParentObject = ObjectMethodDeleteByParentObject.ID
	LEFT JOIN dbo.Metadata_DBStoredProcedure DBStoredProcedureDeleteByParentObject ON ObjectMethodDeleteByParentObject.DBStoredProcedureID = DBStoredProcedureDeleteByParentObject.ID
WHERE dbo.Metadata_Namespace.[Name] = @ObjectNamespace
	AND dbo.Metadata_Object.[Name] = @ObjectName
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadPlainObjectDescriptor]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadPlainObjectDescriptor]
(
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128)
)
AS
DECLARE @ObjectID INT

SELECT @ObjectID = Metadata_Object.ID FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name

IF NOT EXISTS 
(
	SELECT ID FROM Metadata_PlainObject WHERE ID = @ObjectID
)
BEGIN
	RAISERROR('Простого объекта с таким именем в таком пространстве имен не существует!', 11, 1)
	RETURN
END

SELECT
	Metadata_Object.ID AS ID,
	Metadata_Object.ObjectTypeID AS ObjectTypeID,
	Metadata_Namespace.Name AS NamespaceName,
	Metadata_Object.Name AS Name,
	Metadata_Object.FriendlyName AS FriendlyName,
	Metadata_Object.Description AS Description,
	UIEditorName AS UIEditorName
FROM Metadata_PlainObject
	INNER JOIN Metadata_Object ON Metadata_Object.ID = Metadata_PlainObject.ID
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_PlainObject.ID = @ObjectID

SELECT
	Metadata_ObjectAttribute.Name,
	Metadata_ObjectAttribute.FriendlyName,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.DateTimeFormatID,
	Metadata_ObjectAttribute.IsNullable,
	Metadata_ObjectAttribute.IsReadOnly,
	Metadata_ObjectAttribute.IsExternal,
	Metadata_ObjectAttribute.IsUserVisible,
	Metadata_ObjectAttribute.IsPrimaryKeyMember,
	Metadata_ObjectAttribute.MaxLength,
	Metadata_ObjectAttribute.Precision,
	Metadata_ObjectAttribute.Scale,
	Metadata_ObjectAttribute.UILabel,
	Metadata_ObjectAttribute.UIPreferredWidth,
	Metadata_ObjectAttribute.UIPreferredIndex,
	Metadata_ObjectAttribute.UIAllowMultilineText
FROM Metadata_ObjectAttribute
	INNER JOIN Metadata_DataType ON Metadata_DataType.ID = Metadata_ObjectAttribute.DataTypeID
WHERE Metadata_ObjectAttribute.ObjectID = @ObjectID

SELECT
	ID,
	Name,
	FriendlyName,
	Description,
	UILabel,
	UIPreferredIndex
FROM Metadata_ComplexObjectAttribute
WHERE ObjectID = @ObjectID

SELECT
	Metadata_ComplexObjectAttribute.Name AS ComplexObjectAttributeName,
	Metadata_DBTableColumnPrimary.Name AS PrimaryKeyAttributeName,
	Metadata_ObjectAttributeForeign.Name AS ForeignKeyAttributeName
FROM Metadata_ComplexObjectAttribute 
	INNER JOIN Metadata_DBForeignKey ON Metadata_ComplexObjectAttribute.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBForeignKeyColumn ON Metadata_DBForeignKeyColumn.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnPrimary ON Metadata_DBTableColumnPrimary.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDPrimaryKey
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnForeign ON Metadata_DBTableColumnForeign.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDForeignKey
	INNER JOIN Metadata_ObjectAttribute Metadata_ObjectAttributeForeign ON Metadata_ObjectAttributeForeign.DBTableColumnID = Metadata_DBTableColumnForeign.ID AND Metadata_ObjectAttributeForeign.ObjectID = Metadata_ComplexObjectAttribute.ObjectID
WHERE Metadata_ComplexObjectAttribute.ObjectID = @ObjectID

SELECT
	Name,
	FriendlyName,
	Description,
	Enabled,
	UIFullText,
	UIBriefText
FROM Metadata_ObjectAction
WHERE Metadata_ObjectAction.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.ID,
	Metadata_ObjectMethod.Name,
	Metadata_ObjectMethod.FriendlyName,
	Metadata_DBStoredProcedure.Name AS DBStoredProcedureName,
	Metadata_ObjectMethod.ReturnsXMLErrorList
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.Name AS ObjectMethodName,
	Metadata_DBStoredProcedureParameter.Name,
	Metadata_DBStoredProcedureParameter.IsOutput,
	Metadata_DBStoredProcedureParameter.MaxLength,
	Metadata_DBStoredProcedureParameter.Precision, 
	Metadata_DBStoredProcedureParameter.Scale,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.Name AS ObjectAttributeName,
	Metadata_DBStoredProcedureParameterBinding.AlternativeName
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure
		ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
	INNER JOIN Metadata_DBStoredProcedureParameter
		ON Metadata_DBStoredProcedureParameter.DBStoredProcedureID = Metadata_DBStoredProcedure.ID
	LEFT JOIN Metadata_DBStoredProcedureParameterBinding
		ON Metadata_DBStoredProcedureParameterBinding.DBStoredProcedureParameterID = Metadata_DBStoredProcedureParameter.ID 
	LEFT JOIN Metadata_ObjectAttribute
		ON Metadata_ObjectAttribute.ID = Metadata_DBStoredProcedureParameterBinding.ObjectAttributeID
	LEFT JOIN Metadata_DataType
		ON Metadata_DataType.ID = Metadata_DBStoredProcedureParameter.DataTypeID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadPlainObjectListViewDescriptor]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadPlainObjectListViewDescriptor]
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128)
AS
DECLARE @ObjectID INT

SELECT @ObjectID = Metadata_Object.ID FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name

IF NOT EXISTS 
(
	SELECT ID FROM Metadata_PlainObjectListView WHERE ID = @ObjectID
)
BEGIN
	RAISERROR('Представления списка простых объектов с таким именем в таком пространстве имен не существует!', 11, 1)
	RETURN
END

SELECT
	Metadata_Object.ID AS ID,
	Metadata_Object.ObjectTypeID AS ObjectTypeID,
	Metadata_Namespace.Name AS NamespaceName,
	Metadata_Object.Name AS Name,
	Metadata_Object.FriendlyName AS FriendlyName,
	Metadata_Object.Description AS Description,
	UIListCaption,
	EntityNamespace.Name AS [EntityNamespaceName],
	EntityObject.Name AS [EntityName],
	ObjectActionLoadList.Name AS [ObjectActionNameLoadList],
	ObjectMethodLoadList.Name AS [ObjectMethodNameLoadList]
FROM Metadata_PlainObjectListView
	INNER JOIN Metadata_Object ON Metadata_Object.ID = Metadata_PlainObjectListView.ID
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
	INNER JOIN Metadata_PlainObject Entity ON Entity.ID = Metadata_PlainObjectListView.EntityID
	INNER JOIN Metadata_Object EntityObject ON EntityObject.ID = Entity.ID
	INNER JOIN Metadata_Namespace EntityNamespace ON EntityNamespace.ID = EntityObject.NamespaceID
	LEFT JOIN Metadata_ObjectAction ObjectActionLoadList ON ObjectActionLoadList.ID = Metadata_PlainObjectListView.ObjectActionIDLoadList
	LEFT JOIN Metadata_ObjectMethod ObjectMethodLoadList ON ObjectMethodLoadList.ID = Metadata_PlainObjectListView.ObjectMethodIDLoadList
WHERE Metadata_PlainObjectListView.ID = @ObjectID

SELECT
	Metadata_ObjectAttribute.Name,
	Metadata_ObjectAttribute.FriendlyName,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.DateTimeFormatID,
	Metadata_ObjectAttribute.IsNullable,
	Metadata_ObjectAttribute.IsReadOnly,
	Metadata_ObjectAttribute.IsExternal,
	Metadata_ObjectAttribute.IsUserVisible,
	Metadata_ObjectAttribute.IsPrimaryKeyMember,
	Metadata_ObjectAttribute.MaxLength,
	Metadata_ObjectAttribute.Precision,
	Metadata_ObjectAttribute.Scale,
	Metadata_ObjectAttribute.UILabel,
	Metadata_ObjectAttribute.UIPreferredWidth,
	Metadata_ObjectAttribute.UIPreferredIndex,
	Metadata_ObjectAttribute.UIAllowMultilineText
FROM Metadata_ObjectAttribute
	INNER JOIN Metadata_DataType ON Metadata_DataType.ID = Metadata_ObjectAttribute.DataTypeID
WHERE Metadata_ObjectAttribute.ObjectID = @ObjectID

SELECT
	ID,
	Name,
	FriendlyName,
	Description,
	UILabel,
	UIPreferredIndex
FROM Metadata_ComplexObjectAttribute
WHERE ObjectID = @ObjectID

SELECT
	Metadata_ComplexObjectAttribute.Name AS ComplexObjectAttributeName,
	Metadata_DBTableColumnPrimary.Name AS PrimaryKeyAttributeName,
	Metadata_ObjectAttributeForeign.Name AS ForeignKeyAttributeName
FROM Metadata_ComplexObjectAttribute 
	INNER JOIN Metadata_DBForeignKey ON Metadata_ComplexObjectAttribute.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBForeignKeyColumn ON Metadata_DBForeignKeyColumn.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnPrimary ON Metadata_DBTableColumnPrimary.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDPrimaryKey
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnForeign ON Metadata_DBTableColumnForeign.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDForeignKey
	INNER JOIN Metadata_ObjectAttribute Metadata_ObjectAttributeForeign ON Metadata_ObjectAttributeForeign.DBTableColumnID = Metadata_DBTableColumnForeign.ID AND Metadata_ObjectAttributeForeign.ObjectID = Metadata_ComplexObjectAttribute.ObjectID
WHERE Metadata_ComplexObjectAttribute.ObjectID = @ObjectID

SELECT
	Name,
	FriendlyName,
	Description,
	Enabled,
	UIFullText,
	UIBriefText
FROM Metadata_ObjectAction
WHERE Metadata_ObjectAction.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.ID,
	Metadata_ObjectMethod.Name,
	Metadata_ObjectMethod.FriendlyName,
	Metadata_DBStoredProcedure.Name AS DBStoredProcedureName,
	Metadata_ObjectMethod.ReturnsXMLErrorList
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.Name AS ObjectMethodName,
	Metadata_DBStoredProcedureParameter.Name,
	Metadata_DBStoredProcedureParameter.IsOutput,
	Metadata_DBStoredProcedureParameter.MaxLength,
	Metadata_DBStoredProcedureParameter.Precision, 
	Metadata_DBStoredProcedureParameter.Scale,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.Name AS ObjectAttributeName,
	Metadata_DBStoredProcedureParameterBinding.AlternativeName
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure
		ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
	INNER JOIN Metadata_DBStoredProcedureParameter
		ON Metadata_DBStoredProcedureParameter.DBStoredProcedureID = Metadata_DBStoredProcedure.ID
	LEFT JOIN Metadata_DBStoredProcedureParameterBinding
		ON Metadata_DBStoredProcedureParameterBinding.DBStoredProcedureParameterID = Metadata_DBStoredProcedureParameter.ID 
	LEFT JOIN Metadata_ObjectAttribute
		ON Metadata_ObjectAttribute.ID = Metadata_DBStoredProcedureParameterBinding.ObjectAttributeID
	LEFT JOIN Metadata_DataType
		ON Metadata_DataType.ID = Metadata_DBStoredProcedureParameter.DataTypeID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadMultiversionDocumentListViewDescriptor]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadMultiversionDocumentListViewDescriptor]
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128)
AS
DECLARE @ObjectID INT

SELECT @ObjectID = Metadata_Object.ID FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name

IF NOT EXISTS 
(
	SELECT ID FROM Metadata_MultiversionDocumentListView WHERE ID = @ObjectID
)
BEGIN
	RAISERROR('Представления списка документов с таким именем в таком пространстве имен не существует!', 11, 1)
	RETURN
END

SELECT 
	Metadata_Object.ID AS ID,
	Metadata_Object.ObjectTypeID AS ObjectTypeID,
	Metadata_Namespace.Name AS NamespaceName,
	Metadata_Object.Name AS Name,
	Metadata_Object.FriendlyName AS FriendlyName,
	Metadata_Object.Description AS Description,
	UIListCaption,
	MultiversionDocumentNamespace.Name AS [MultiversionDocumentNamespaceName],
	MultiversionDocumentObject.Name AS [MultiversionDocumentName],
	ObjectActionLoadList.Name AS [ObjectActionNameLoadList],
	ObjectMethodLoadList.Name AS [ObjectMethodNameLoadList]
FROM Metadata_MultiversionDocumentListView
	INNER JOIN Metadata_Object ON Metadata_Object.ID = Metadata_MultiversionDocumentListView.ID
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
	INNER JOIN Metadata_MultiversionDocument MultiversionDocument ON MultiversionDocument.ID = Metadata_MultiversionDocumentListView.MultiversionDocumentID
	INNER JOIN Metadata_Object MultiversionDocumentObject ON MultiversionDocumentObject.ID = MultiversionDocument.ID
	INNER JOIN Metadata_Namespace MultiversionDocumentNamespace ON MultiversionDocumentNamespace.ID = MultiversionDocumentObject.NamespaceID
	LEFT JOIN Metadata_ObjectAction ObjectActionLoadList ON ObjectActionLoadList.ID = Metadata_MultiversionDocumentListView.ObjectActionIDLoadList
	LEFT JOIN Metadata_ObjectMethod ObjectMethodLoadList ON ObjectMethodLoadList.ID = Metadata_MultiversionDocumentListView.ObjectMethodIDLoadList
WHERE Metadata_MultiversionDocumentListView.ID = @ObjectID

SELECT
	Metadata_ObjectAttribute.Name,
	Metadata_ObjectAttribute.FriendlyName,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.DateTimeFormatID,
	Metadata_ObjectAttribute.IsNullable,
	Metadata_ObjectAttribute.IsReadOnly,
	Metadata_ObjectAttribute.IsExternal,
	Metadata_ObjectAttribute.IsUserVisible,
	Metadata_ObjectAttribute.IsPrimaryKeyMember,
	Metadata_ObjectAttribute.MaxLength,
	Metadata_ObjectAttribute.Precision,
	Metadata_ObjectAttribute.Scale,
	Metadata_ObjectAttribute.UILabel,
	Metadata_ObjectAttribute.UIPreferredWidth,
	Metadata_ObjectAttribute.UIPreferredIndex,
	Metadata_ObjectAttribute.UIAllowMultilineText
FROM Metadata_ObjectAttribute
	INNER JOIN Metadata_DataType ON Metadata_DataType.ID = Metadata_ObjectAttribute.DataTypeID
WHERE Metadata_ObjectAttribute.ObjectID = @ObjectID

SELECT
	ID,
	Name,
	FriendlyName,
	Description,
	UILabel,
	UIPreferredIndex
FROM Metadata_ComplexObjectAttribute
WHERE ObjectID = @ObjectID

SELECT
	Metadata_ComplexObjectAttribute.Name AS ComplexObjectAttributeName,
	Metadata_DBTableColumnPrimary.Name AS PrimaryKeyAttributeName,
	Metadata_ObjectAttributeForeign.Name AS ForeignKeyAttributeName
FROM Metadata_ComplexObjectAttribute 
	INNER JOIN Metadata_DBForeignKey ON Metadata_ComplexObjectAttribute.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBForeignKeyColumn ON Metadata_DBForeignKeyColumn.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnPrimary ON Metadata_DBTableColumnPrimary.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDPrimaryKey
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnForeign ON Metadata_DBTableColumnForeign.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDForeignKey
	INNER JOIN Metadata_ObjectAttribute Metadata_ObjectAttributeForeign ON Metadata_ObjectAttributeForeign.DBTableColumnID = Metadata_DBTableColumnForeign.ID AND Metadata_ObjectAttributeForeign.ObjectID = Metadata_ComplexObjectAttribute.ObjectID
WHERE Metadata_ComplexObjectAttribute.ObjectID = @ObjectID

SELECT
	Name,
	FriendlyName,
	Description,
	Enabled,
	UIFullText,
	UIBriefText
FROM Metadata_ObjectAction
WHERE Metadata_ObjectAction.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.ID,
	Metadata_ObjectMethod.Name,
	Metadata_ObjectMethod.FriendlyName,
	Metadata_DBStoredProcedure.Name AS DBStoredProcedureName,
	Metadata_ObjectMethod.ReturnsXMLErrorList
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.Name AS ObjectMethodName,
	Metadata_DBStoredProcedureParameter.Name,
	Metadata_DBStoredProcedureParameter.IsOutput,
	Metadata_DBStoredProcedureParameter.MaxLength,
	Metadata_DBStoredProcedureParameter.Precision, 
	Metadata_DBStoredProcedureParameter.Scale,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.Name AS ObjectAttributeName,
	Metadata_DBStoredProcedureParameterBinding.AlternativeName
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure
		ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
	INNER JOIN Metadata_DBStoredProcedureParameter
		ON Metadata_DBStoredProcedureParameter.DBStoredProcedureID = Metadata_DBStoredProcedure.ID
	LEFT JOIN Metadata_DBStoredProcedureParameterBinding
		ON Metadata_DBStoredProcedureParameterBinding.DBStoredProcedureParameterID = Metadata_DBStoredProcedureParameter.ID 
	LEFT JOIN Metadata_ObjectAttribute
		ON Metadata_ObjectAttribute.ID = Metadata_DBStoredProcedureParameterBinding.ObjectAttributeID
	LEFT JOIN Metadata_DataType
		ON Metadata_DataType.ID = Metadata_DBStoredProcedureParameter.DataTypeID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadStoredQueryDescriptor]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadStoredQueryDescriptor]
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128)
AS
DECLARE @ObjectID INT

SELECT @ObjectID = Metadata_Object.ID FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name

IF NOT EXISTS 
(
	SELECT ID FROM Metadata_StoredQuery WHERE ID = @ObjectID
)
BEGIN
	RAISERROR('Сохраненного запроса с таким именем в таком пространстве имен не существует!', 11, 1)
	RETURN
END

SELECT
	Metadata_Object.ID AS ID,
	Metadata_Object.ObjectTypeID AS ObjectTypeID,
	Metadata_Namespace.Name AS NamespaceName,
	Metadata_Object.Name AS Name,
	Metadata_Object.FriendlyName AS FriendlyName,
	Metadata_Object.Description AS Description,
	UIListCaption,
	ObjectActionLoadList.Name AS [ObjectActionNameLoadList],
	ObjectMethodLoadList.Name AS [ObjectMethodNameLoadList]
FROM Metadata_StoredQuery
	INNER JOIN Metadata_Object ON Metadata_Object.ID = Metadata_StoredQuery.ID
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
	LEFT JOIN Metadata_ObjectAction ObjectActionLoadList ON ObjectActionLoadList.ID = Metadata_StoredQuery.ObjectActionIDLoadList
	LEFT JOIN Metadata_ObjectMethod ObjectMethodLoadList ON ObjectMethodLoadList.ID = Metadata_StoredQuery.ObjectMethodIDLoadList
WHERE Metadata_StoredQuery.ID = @ObjectID

SELECT
	Metadata_ObjectAttribute.Name,
	Metadata_ObjectAttribute.FriendlyName,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.DateTimeFormatID,
	Metadata_ObjectAttribute.IsNullable,
	Metadata_ObjectAttribute.IsReadOnly,
	Metadata_ObjectAttribute.IsExternal,
	Metadata_ObjectAttribute.IsUserVisible,
	Metadata_ObjectAttribute.IsPrimaryKeyMember,
	Metadata_ObjectAttribute.MaxLength,
	Metadata_ObjectAttribute.Precision,
	Metadata_ObjectAttribute.Scale,
	Metadata_ObjectAttribute.UILabel,
	Metadata_ObjectAttribute.UIPreferredWidth,
	Metadata_ObjectAttribute.UIPreferredIndex,
	Metadata_ObjectAttribute.UIAllowMultilineText
FROM Metadata_ObjectAttribute
	INNER JOIN Metadata_DataType ON Metadata_DataType.ID = Metadata_ObjectAttribute.DataTypeID
WHERE Metadata_ObjectAttribute.ObjectID = @ObjectID

SELECT
	ID,
	Name,
	FriendlyName,
	Description,
	UILabel,
	UIPreferredIndex
FROM Metadata_ComplexObjectAttribute
WHERE ObjectID = @ObjectID

SELECT
	Metadata_ComplexObjectAttribute.Name AS ComplexObjectAttributeName,
	Metadata_DBTableColumnPrimary.Name AS PrimaryKeyAttributeName,
	Metadata_ObjectAttributeForeign.Name AS ForeignKeyAttributeName
FROM Metadata_ComplexObjectAttribute 
	INNER JOIN Metadata_DBForeignKey ON Metadata_ComplexObjectAttribute.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBForeignKeyColumn ON Metadata_DBForeignKeyColumn.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnPrimary ON Metadata_DBTableColumnPrimary.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDPrimaryKey
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnForeign ON Metadata_DBTableColumnForeign.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDForeignKey
	INNER JOIN Metadata_ObjectAttribute Metadata_ObjectAttributeForeign ON Metadata_ObjectAttributeForeign.DBTableColumnID = Metadata_DBTableColumnForeign.ID AND Metadata_ObjectAttributeForeign.ObjectID = Metadata_ComplexObjectAttribute.ObjectID
WHERE Metadata_ComplexObjectAttribute.ObjectID = @ObjectID

SELECT
	Name,
	FriendlyName,
	Description,
	Enabled,
	UIFullText,
	UIBriefText
FROM Metadata_ObjectAction
WHERE Metadata_ObjectAction.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.ID,
	Metadata_ObjectMethod.Name,
	Metadata_ObjectMethod.FriendlyName,
	Metadata_DBStoredProcedure.Name AS DBStoredProcedureName,
	Metadata_ObjectMethod.ReturnsXMLErrorList
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.Name AS ObjectMethodName,
	Metadata_DBStoredProcedureParameter.Name,
	Metadata_DBStoredProcedureParameter.IsOutput,
	Metadata_DBStoredProcedureParameter.MaxLength,
	Metadata_DBStoredProcedureParameter.Precision, 
	Metadata_DBStoredProcedureParameter.Scale,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.Name AS ObjectAttributeName,
	Metadata_DBStoredProcedureParameterBinding.AlternativeName
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure
		ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
	INNER JOIN Metadata_DBStoredProcedureParameter
		ON Metadata_DBStoredProcedureParameter.DBStoredProcedureID = Metadata_DBStoredProcedure.ID
	LEFT JOIN Metadata_DBStoredProcedureParameterBinding
		ON Metadata_DBStoredProcedureParameterBinding.DBStoredProcedureParameterID = Metadata_DBStoredProcedureParameter.ID 
	LEFT JOIN Metadata_ObjectAttribute
		ON Metadata_ObjectAttribute.ID = Metadata_DBStoredProcedureParameterBinding.ObjectAttributeID
	LEFT JOIN Metadata_DataType
		ON Metadata_DataType.ID = Metadata_DBStoredProcedureParameter.DataTypeID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_LoadMultiversionDocumentDescriptor]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_LoadMultiversionDocumentDescriptor]
(
	@Namespace NVARCHAR(128),
	@Name NVARCHAR(128)
)
AS
DECLARE @ObjectID INT

SELECT @ObjectID = Metadata_Object.ID FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Namespace.Name = @Namespace AND Metadata_Object.Name = @Name

IF NOT EXISTS 
(
	SELECT ID FROM Metadata_MultiversionDocument WHERE ID = @ObjectID
)
BEGIN
	RAISERROR('Документа с таким именем в таком пространстве имен не существует!', 11, 1)
	RETURN
END

SELECT
	Metadata_Object.ID AS ID,
	Metadata_Object.ObjectTypeID AS ObjectTypeID,
	Metadata_Namespace.Name AS NamespaceName,
	Metadata_Object.Name AS Name,
	Metadata_Object.FriendlyName AS FriendlyName,
	Metadata_Object.Description AS Description,
	ObjectActionCreate.Name AS ObjectActionNameCreate,
	ObjectActionStartEdit.Name AS ObjectActionNameStartEdit,
	ObjectActionConfirmEdit.Name AS ObjectActionNameConfirmEdit,
	ObjectActionCancel.Name AS ObjectActionNameCancelEdit,
	ObjectActionDelete.Name AS ObjectActionNameDelete,
	ObjectActionCommit.Name AS ObjectActionNameCommit,
	ObjectActionRollback.Name AS ObjectActionNameRollback,
	ObjectActionPick.Name AS ObjectActionNamePick,
	ObjectMethodCreate.Name AS ObjectMethodNameCreate,
	ObjectMethodStartEdit.Name AS ObjectMethodNameStartEdit,
	ObjectMethodUpdateVersion.Name AS ObjectMethodNameUpdateVersion,
	ObjectMethodConfirmEdit.Name AS ObjectMethodNameConfirmEdit,
	ObjectMethodCancelEdit.Name AS ObjectMethodNameCancelEdit,
	ObjectMethodDelete.Name AS ObjectMethodNameDelete,
	ObjectMethodLoad.Name AS ObjectMethodNameLoad,
	ObjectMethodCommit.Name AS ObjectMethodNameCommit,
	ObjectMethodRollback.Name AS ObjectMethodNameRollback
FROM Metadata_MultiversionDocument
	INNER JOIN Metadata_Object ON Metadata_Object.ID = Metadata_MultiversionDocument.ID
	INNER JOIN Metadata_Namespace ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
	LEFT JOIN Metadata_ObjectAction ObjectActionCreate ON ObjectActionCreate.ID = Metadata_MultiversionDocument.ObjectActionIDCreate
	LEFT JOIN Metadata_ObjectAction ObjectActionStartEdit ON ObjectActionStartEdit.ID = Metadata_MultiversionDocument.ObjectActionIDStartEdit
	LEFT JOIN Metadata_ObjectAction ObjectActionConfirmEdit ON ObjectActionConfirmEdit.ID = Metadata_MultiversionDocument.ObjectActionIDConfirmEdit
	LEFT JOIN Metadata_ObjectAction ObjectActionCancel ON ObjectActionCancel.ID = Metadata_MultiversionDocument.ObjectActionIDCancelEdit
	LEFT JOIN Metadata_ObjectAction ObjectActionDelete ON ObjectActionDelete.ID = Metadata_MultiversionDocument.ObjectActionIDDelete
	LEFT JOIN Metadata_ObjectAction ObjectActionCommit ON ObjectActionCommit.ID = Metadata_MultiversionDocument.ObjectActionIDCommit
	LEFT JOIN Metadata_ObjectAction ObjectActionRollback ON ObjectActionRollback.ID = Metadata_MultiversionDocument.ObjectActionIDRollback
	LEFT JOIN Metadata_ObjectAction ObjectActionPick ON ObjectActionPick.ID = Metadata_MultiversionDocument.ObjectActionIDPick
	LEFT JOIN Metadata_ObjectMethod ObjectMethodCreate ON ObjectMethodCreate.ID = Metadata_MultiversionDocument.ObjectMethodIDCreate
	LEFT JOIN Metadata_ObjectMethod ObjectMethodStartEdit ON ObjectMethodStartEdit.ID = Metadata_MultiversionDocument.ObjectMethodIDStartEdit
	LEFT JOIN Metadata_ObjectMethod ObjectMethodUpdateVersion ON ObjectMethodUpdateVersion.ID = Metadata_MultiversionDocument.ObjectMethodIDUpdateVersion
	LEFT JOIN Metadata_ObjectMethod ObjectMethodConfirmEdit ON ObjectMethodConfirmEdit.ID = Metadata_MultiversionDocument.ObjectMethodIDConfirmEdit
	LEFT JOIN Metadata_ObjectMethod ObjectMethodCancelEdit ON ObjectMethodCancelEdit.ID = Metadata_MultiversionDocument.ObjectMethodIDCancelEdit
	LEFT JOIN Metadata_ObjectMethod ObjectMethodDelete ON ObjectMethodDelete.ID = Metadata_MultiversionDocument.ObjectMethodIDDelete
	LEFT JOIN Metadata_ObjectMethod ObjectMethodLoad ON ObjectMethodLoad.ID = Metadata_MultiversionDocument.ObjectMethodIDLoad
	LEFT JOIN Metadata_ObjectMethod ObjectMethodCommit ON ObjectMethodCommit.ID = Metadata_MultiversionDocument.ObjectMethodIDCommit
	LEFT JOIN Metadata_ObjectMethod ObjectMethodRollback ON ObjectMethodRollback.ID = Metadata_MultiversionDocument.ObjectMethodIDRollback
WHERE Metadata_MultiversionDocument.ID = @ObjectID

SELECT
	Metadata_ObjectAttribute.Name,
	Metadata_ObjectAttribute.FriendlyName,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.DateTimeFormatID,
	Metadata_ObjectAttribute.IsNullable,
	Metadata_ObjectAttribute.IsReadOnly,
	Metadata_ObjectAttribute.IsExternal,
	Metadata_ObjectAttribute.IsUserVisible,
	Metadata_ObjectAttribute.IsPrimaryKeyMember,
	Metadata_ObjectAttribute.MaxLength,
	Metadata_ObjectAttribute.Precision,
	Metadata_ObjectAttribute.Scale,
	Metadata_ObjectAttribute.UILabel,
	Metadata_ObjectAttribute.UIPreferredWidth,
	Metadata_ObjectAttribute.UIPreferredIndex,
	Metadata_ObjectAttribute.UIAllowMultilineText
FROM Metadata_ObjectAttribute
	INNER JOIN Metadata_DataType ON Metadata_DataType.ID = Metadata_ObjectAttribute.DataTypeID
WHERE Metadata_ObjectAttribute.ObjectID = @ObjectID

SELECT
	ID,
	Name,
	FriendlyName,
	Description,
	UILabel,
	UIPreferredIndex
FROM Metadata_ComplexObjectAttribute
WHERE ObjectID = @ObjectID

SELECT
	Metadata_ComplexObjectAttribute.Name AS ComplexObjectAttributeName,
	Metadata_DBTableColumnPrimary.Name AS PrimaryKeyAttributeName,
	Metadata_ObjectAttributeForeign.Name AS ForeignKeyAttributeName
FROM Metadata_ComplexObjectAttribute 
	INNER JOIN Metadata_DBForeignKey ON Metadata_ComplexObjectAttribute.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBForeignKeyColumn ON Metadata_DBForeignKeyColumn.DBForeignKeyID = Metadata_DBForeignKey.ID
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnPrimary ON Metadata_DBTableColumnPrimary.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDPrimaryKey
	INNER JOIN Metadata_DBTableColumn Metadata_DBTableColumnForeign ON Metadata_DBTableColumnForeign.ID = Metadata_DBForeignKeyColumn.DBTableColumnIDForeignKey
	INNER JOIN Metadata_ObjectAttribute Metadata_ObjectAttributeForeign ON Metadata_ObjectAttributeForeign.DBTableColumnID = Metadata_DBTableColumnForeign.ID AND Metadata_ObjectAttributeForeign.ObjectID = Metadata_ComplexObjectAttribute.ObjectID
WHERE Metadata_ComplexObjectAttribute.ObjectID = @ObjectID

SELECT
	Name,
	FriendlyName,
	Description,
	Enabled,
	UIFullText,
	UIBriefText
FROM Metadata_ObjectAction
WHERE Metadata_ObjectAction.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.ID,
	Metadata_ObjectMethod.Name,
	Metadata_ObjectMethod.FriendlyName,
	Metadata_DBStoredProcedure.Name AS DBStoredProcedureName,
	Metadata_ObjectMethod.ReturnsXMLErrorList
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID

SELECT
	Metadata_ObjectMethod.Name AS ObjectMethodName,
	Metadata_DBStoredProcedureParameter.Name,
	Metadata_DBStoredProcedureParameter.IsOutput,
	Metadata_DBStoredProcedureParameter.MaxLength,
	Metadata_DBStoredProcedureParameter.Precision, 
	Metadata_DBStoredProcedureParameter.Scale,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_ObjectAttribute.Name AS ObjectAttributeName,
	Metadata_DBStoredProcedureParameterBinding.AlternativeName
FROM Metadata_ObjectMethod
	INNER JOIN Metadata_DBStoredProcedure
		ON Metadata_DBStoredProcedure.ID = Metadata_ObjectMethod.DBStoredProcedureID
	INNER JOIN Metadata_DBStoredProcedureParameter
		ON Metadata_DBStoredProcedureParameter.DBStoredProcedureID = Metadata_DBStoredProcedure.ID
	LEFT JOIN Metadata_DBStoredProcedureParameterBinding
		ON Metadata_DBStoredProcedureParameterBinding.DBStoredProcedureParameterID = Metadata_DBStoredProcedureParameter.ID 
	LEFT JOIN Metadata_ObjectAttribute
		ON Metadata_ObjectAttribute.ID = Metadata_DBStoredProcedureParameterBinding.ObjectAttributeID
	LEFT JOIN Metadata_DataType
		ON Metadata_DataType.ID = Metadata_DBStoredProcedureParameter.DataTypeID
WHERE Metadata_ObjectMethod.ObjectID = @ObjectID
GO
/****** Object:  StoredProcedure [dbo].[SelectComplexAttributeAliases]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectComplexAttributeAliases]
	@ObjectNamespace NVARCHAR(128),
	@ObjectName NVARCHAR(128)
AS
SELECT
	Metadata_ComplexObjectAttribute.Name AS Name,
	Metadata_ComplexObjectAttribute.UILabel AS UILabel
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
	INNER JOIN Metadata_ComplexObjectAttribute ON Metadata_ComplexObjectAttribute.ObjectID = Metadata_Object.ID
WHERE Metadata_Namespace.Name = @ObjectNamespace
	AND Metadata_Object.Name = @ObjectName
GO
/****** Object:  StoredProcedure [dbo].[SelectAttributeAliases]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectAttributeAliases]
	@ObjectNamespace NVARCHAR(128),
	@ObjectName NVARCHAR(128)
AS
SELECT
	Metadata_ObjectAttribute.Name AS Name,
	Metadata_ObjectAttribute.UILabel AS UILabel
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
	INNER JOIN Metadata_ObjectAttribute ON Metadata_ObjectAttribute.ObjectID = Metadata_Object.ID
WHERE Metadata_Namespace.Name = @ObjectNamespace
	AND Metadata_Object.Name = @ObjectName
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_RemoveObjectMethod]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_RemoveObjectMethod]
	@ObjectNamespace NVARCHAR(128),
	@ObjectName NVARCHAR(128),
	@Name NVARCHAR(128)
AS
DECLARE @ObjectID INT
SELECT @ObjectID = Metadata_Object.ID FROM Metadata_Object
	INNER JOIN Metadata_Namespace
		ON Metadata_Namespace.ID = Metadata_Object.NamespaceID
WHERE Metadata_Object.Name = @ObjectName AND Metadata_Namespace.Name = @ObjectNamespace

DELETE FROM Metadata_ObjectMethod WHERE ObjectID = @ObjectID AND Name = @Name
GO
/****** Object:  StoredProcedure [dbo].[SelectTableList]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectTableList]
AS
SELECT 
	Name,
	Description
FROM Metadata_DBTable
WHERE Valid = 1

SELECT
	Metadata_DBTable.Name AS TableName,
	Metadata_DBTableColumn.Name,
	Metadata_DataType.Name AS DataTypeName,
	Metadata_DBTableColumn.AllowNull,
	Metadata_DBTableColumn.IsPrimaryKeyMember,
	Metadata_DBTableColumn.IsForeignKeyMember,
	Metadata_DBTableColumn.IsIdentity,
	Metadata_DBTableColumn.MaxLength,
	Metadata_DBTableColumn.Precision,
	Metadata_DBTableColumn.Scale,
	Metadata_DBTableColumn.Description
FROM Metadata_DBTableColumn
	INNER JOIN Metadata_DataType ON Metadata_DBTableColumn.DataTypeID = Metadata_DataType.ID
	INNER JOIN Metadata_DBTable ON Metadata_DBTableColumn.DBTableID = Metadata_DBTable.ID	
WHERE Metadata_DBTableColumn.Valid = 1 AND Metadata_DBTable.Valid = 1
GO
/****** Object:  StoredProcedure [dbo].[SelectForeignKeyList]    Script Date: 09/06/2008 23:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SelectForeignKeyList]
AS
SELECT 
	Metadata_DBForeignKey.Name,
	PrimaryKeyTable.Name AS PrimaryTableName,
	ForeignKeyTable.Name AS ForeignTableName,
	Metadata_DBForeignKey.Description
FROM Metadata_DBForeignKey
	INNER JOIN Metadata_DBTable AS PrimaryKeyTable ON Metadata_DBForeignKey.DBTableIDChild = PrimaryKeyTable.ID
	INNER JOIN Metadata_DBTable AS ForeignKeyTable ON Metadata_DBForeignKey.DBTableIDParent = ForeignKeyTable.ID
WHERE Metadata_DBForeignKey.Valid = 1
	AND PrimaryKeyTable.Valid = 1
	AND ForeignKeyTable.Valid = 1

SELECT
	Metadata_DBForeignKey.Name AS ForeignKeyName,
	PrimaryKeyColumn.Name AS PrimaryKeyColumnName,
	ForeignKeyColumn.Name AS ForeignKeyColumnName
FROM Metadata_DBForeignKeyColumn
	INNER JOIN Metadata_DBTableColumn AS ForeignKeyColumn ON Metadata_DBForeignKeyColumn.DBTableColumnIDForeignKey = ForeignKeyColumn.ID
	INNER JOIN Metadata_DBTableColumn AS PrimaryKeyColumn ON Metadata_DBForeignKeyColumn.DBTableColumnIDPrimaryKey = PrimaryKeyColumn.ID
	INNER JOIN Metadata_DBForeignKey ON Metadata_DBForeignKeyColumn.DBForeignKeyID = Metadata_DBForeignKey.ID
WHERE Metadata_DBForeignKeyColumn.Valid = 1 AND Metadata_DBForeignKey.Valid = 1
GO
/****** Object:  Default [DF_SystemForeignKeys_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKey] ADD  CONSTRAINT [DF_SystemForeignKeys_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_SystemForeignKeyColumn_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn] ADD  CONSTRAINT [DF_SystemForeignKeyColumn_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_SystemStoredProcedure_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedure] ADD  CONSTRAINT [DF_SystemStoredProcedure_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_Metadata_DBStoredProcedure_Exists]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedure] ADD  CONSTRAINT [DF_Metadata_DBStoredProcedure_Exists]  DEFAULT ((1)) FOR [Existent]
GO
/****** Object:  Default [DF_MetadataStoredProcedureParameter_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameter] ADD  CONSTRAINT [DF_MetadataStoredProcedureParameter_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_SystemTable_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBTable] ADD  CONSTRAINT [DF_SystemTable_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_Metadata_DBTableColumn_IsIdentity]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBTableColumn] ADD  CONSTRAINT [DF_Metadata_DBTableColumn_IsIdentity]  DEFAULT ((0)) FOR [IsIdentity]
GO
/****** Object:  Default [DF_SystemTableColumn_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBTableColumn] ADD  CONSTRAINT [DF_SystemTableColumn_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_SystemView_valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBView] ADD  CONSTRAINT [DF_SystemView_valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_Metadata_DBViewColumn_Valid]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBViewColumn] ADD  CONSTRAINT [DF_Metadata_DBViewColumn_Valid]  DEFAULT ((1)) FOR [Valid]
GO
/****** Object:  Default [DF_Metadata_ObjectAction_Enabled]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAction] ADD  CONSTRAINT [DF_Metadata_ObjectAction_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO
/****** Object:  Default [DF__MetadataA__data___37A5467C]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute] ADD  CONSTRAINT [DF__MetadataA__data___37A5467C]  DEFAULT ((1)) FOR [DataTypeID]
GO
/****** Object:  Default [DF_MetadataAttribute_length]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute] ADD  CONSTRAINT [DF_MetadataAttribute_length]  DEFAULT ((0)) FOR [MaxLength]
GO
/****** Object:  Default [DF_MetadataAttribute_precision]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute] ADD  CONSTRAINT [DF_MetadataAttribute_precision]  DEFAULT ((0)) FOR [Precision]
GO
/****** Object:  Default [DF_MetadataAttribute_scale]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute] ADD  CONSTRAINT [DF_MetadataAttribute_scale]  DEFAULT ((0)) FOR [Scale]
GO
/****** Object:  Default [DF_MetadataAttribute_attribute_label]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute] ADD  CONSTRAINT [DF_MetadataAttribute_attribute_label]  DEFAULT (N'') FOR [UILabel]
GO
/****** Object:  Default [DF__MetadataA__colum__38996AB5]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute] ADD  CONSTRAINT [DF__MetadataA__colum__38996AB5]  DEFAULT ((100)) FOR [UIPreferredWidth]
GO
/****** Object:  Default [DF_Metadata_ObjectMethod_ReturnsXMLErrorList]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectMethod] ADD  CONSTRAINT [DF_Metadata_ObjectMethod_ReturnsXMLErrorList]  DEFAULT ((0)) FOR [ReturnsXMLErrorList]
GO
/****** Object:  Default [DF_Metadata_StoredQuery_PredefinedAttributes]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_StoredQuery] ADD  CONSTRAINT [DF_Metadata_StoredQuery_PredefinedAttributes]  DEFAULT ((0)) FOR [PredefinedAttributes]
GO
/****** Object:  ForeignKey [FK_Metadata_ComplexObjectAttribute_Metadata_DBForeignKey]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ComplexObjectAttribute]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ComplexObjectAttribute_Metadata_DBForeignKey] FOREIGN KEY([DBForeignKeyID])
REFERENCES [dbo].[Metadata_DBForeignKey] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ComplexObjectAttribute] CHECK CONSTRAINT [FK_Metadata_ComplexObjectAttribute_Metadata_DBForeignKey]
GO
/****** Object:  ForeignKey [FK_Metadata_ComplexObjectAttribute_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ComplexObjectAttribute]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ComplexObjectAttribute_Metadata_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ComplexObjectAttribute] CHECK CONSTRAINT [FK_Metadata_ComplexObjectAttribute_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_DBForeignKey_Metadata_DBTable_Child]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKey]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBForeignKey_Metadata_DBTable_Child] FOREIGN KEY([DBTableIDChild])
REFERENCES [dbo].[Metadata_DBTable] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBForeignKey] CHECK CONSTRAINT [FK_Metadata_DBForeignKey_Metadata_DBTable_Child]
GO
/****** Object:  ForeignKey [FK_Metadata_DBForeignKey_Metadata_DBTable_Parent]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKey]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBForeignKey_Metadata_DBTable_Parent] FOREIGN KEY([DBTableIDParent])
REFERENCES [dbo].[Metadata_DBTable] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBForeignKey] CHECK CONSTRAINT [FK_Metadata_DBForeignKey_Metadata_DBTable_Parent]
GO
/****** Object:  ForeignKey [FK_MetadataForeignKeyColumn_MetadataForeignKey]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn]  WITH CHECK ADD  CONSTRAINT [FK_MetadataForeignKeyColumn_MetadataForeignKey] FOREIGN KEY([DBForeignKeyID])
REFERENCES [dbo].[Metadata_DBForeignKey] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn] CHECK CONSTRAINT [FK_MetadataForeignKeyColumn_MetadataForeignKey]
GO
/****** Object:  ForeignKey [FK_MetadataForeignKeyColumn_MetadataTableColumnForeign]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn]  WITH CHECK ADD  CONSTRAINT [FK_MetadataForeignKeyColumn_MetadataTableColumnForeign] FOREIGN KEY([DBTableColumnIDForeignKey])
REFERENCES [dbo].[Metadata_DBTableColumn] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn] CHECK CONSTRAINT [FK_MetadataForeignKeyColumn_MetadataTableColumnForeign]
GO
/****** Object:  ForeignKey [FK_MetadataForeignKeyColumn_MetadataTableColumnPrimary]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn]  WITH CHECK ADD  CONSTRAINT [FK_MetadataForeignKeyColumn_MetadataTableColumnPrimary] FOREIGN KEY([DBTableColumnIDPrimaryKey])
REFERENCES [dbo].[Metadata_DBTableColumn] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBForeignKeyColumn] CHECK CONSTRAINT [FK_MetadataForeignKeyColumn_MetadataTableColumnPrimary]
GO
/****** Object:  ForeignKey [FK_Metadata_DBStoredProcedureParameter_Metadata_DataType]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameter]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBStoredProcedureParameter_Metadata_DataType] FOREIGN KEY([DataTypeID])
REFERENCES [dbo].[Metadata_DataType] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameter] CHECK CONSTRAINT [FK_Metadata_DBStoredProcedureParameter_Metadata_DataType]
GO
/****** Object:  ForeignKey [FK_Metadata_DBStoredProcedureParameter_Metadata_DBStoredProcedure]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameter]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBStoredProcedureParameter_Metadata_DBStoredProcedure] FOREIGN KEY([DBStoredProcedureID])
REFERENCES [dbo].[Metadata_DBStoredProcedure] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameter] CHECK CONSTRAINT [FK_Metadata_DBStoredProcedureParameter_Metadata_DBStoredProcedure]
GO
/****** Object:  ForeignKey [FK_Metadata_DBStoredProcedureParameterBinding_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBStoredProcedureParameterBinding_Metadata_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding] CHECK CONSTRAINT [FK_Metadata_DBStoredProcedureParameterBinding_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_DBStoredProcedureParameterBinding_Metadata_ObjectAttribute]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBStoredProcedureParameterBinding_Metadata_ObjectAttribute] FOREIGN KEY([ObjectAttributeID])
REFERENCES [dbo].[Metadata_ObjectAttribute] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding] CHECK CONSTRAINT [FK_Metadata_DBStoredProcedureParameterBinding_Metadata_ObjectAttribute]
GO
/****** Object:  ForeignKey [MetadataStoredProcedureParameter_MetadataStoredProcedureParameterBinding_FK1]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding]  WITH CHECK ADD  CONSTRAINT [MetadataStoredProcedureParameter_MetadataStoredProcedureParameterBinding_FK1] FOREIGN KEY([DBStoredProcedureParameterID])
REFERENCES [dbo].[Metadata_DBStoredProcedureParameter] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Metadata_DBStoredProcedureParameterBinding] CHECK CONSTRAINT [MetadataStoredProcedureParameter_MetadataStoredProcedureParameterBinding_FK1]
GO
/****** Object:  ForeignKey [FK_Metadata_DBTable_Metadata_DBTableColumn_DeleteFlag]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBTable]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBTable_Metadata_DBTableColumn_DeleteFlag] FOREIGN KEY([DeleteFlagDBTableColumnID])
REFERENCES [dbo].[Metadata_DBTableColumn] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBTable] CHECK CONSTRAINT [FK_Metadata_DBTable_Metadata_DBTableColumn_DeleteFlag]
GO
/****** Object:  ForeignKey [FK_Metadata_DBTableColumn_Metadata_DataType]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBTableColumn]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBTableColumn_Metadata_DataType] FOREIGN KEY([DataTypeID])
REFERENCES [dbo].[Metadata_DataType] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBTableColumn] CHECK CONSTRAINT [FK_Metadata_DBTableColumn_Metadata_DataType]
GO
/****** Object:  ForeignKey [FK_MetadataTableColumn_MetadataTable]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBTableColumn]  WITH CHECK ADD  CONSTRAINT [FK_MetadataTableColumn_MetadataTable] FOREIGN KEY([DBTableID])
REFERENCES [dbo].[Metadata_DBTable] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Metadata_DBTableColumn] CHECK CONSTRAINT [FK_MetadataTableColumn_MetadataTable]
GO
/****** Object:  ForeignKey [FK_Metadata_DBViewColumn_Metadata_DataType]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBViewColumn]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBViewColumn_Metadata_DataType] FOREIGN KEY([DataTypeID])
REFERENCES [dbo].[Metadata_DataType] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBViewColumn] CHECK CONSTRAINT [FK_Metadata_DBViewColumn_Metadata_DataType]
GO
/****** Object:  ForeignKey [FK_Metadata_DBViewColumn_Metadata_DBView]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_DBViewColumn]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_DBViewColumn_Metadata_DBView] FOREIGN KEY([DBViewID])
REFERENCES [dbo].[Metadata_DBView] ([ID])
GO
ALTER TABLE [dbo].[Metadata_DBViewColumn] CHECK CONSTRAINT [FK_Metadata_DBViewColumn_Metadata_DBView]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_DBTable]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_DBTable] FOREIGN KEY([DBTableID])
REFERENCES [dbo].[Metadata_DBTable] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_DBTable]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_DBView]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_DBView] FOREIGN KEY([DBViewID])
REFERENCES [dbo].[Metadata_DBView] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_DBView]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_Object] FOREIGN KEY([ID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_CancelEdit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_CancelEdit] FOREIGN KEY([ObjectActionIDCancelEdit])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_CancelEdit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Commit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Commit] FOREIGN KEY([ObjectActionIDCommit])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Commit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_ConfirmEdit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_ConfirmEdit] FOREIGN KEY([ObjectActionIDConfirmEdit])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_ConfirmEdit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Create]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Create] FOREIGN KEY([ObjectActionIDCreate])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Create]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Delete]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Delete] FOREIGN KEY([ObjectActionIDDelete])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Delete]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Pick]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Pick] FOREIGN KEY([ObjectActionIDPick])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Pick]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Rollback]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Rollback] FOREIGN KEY([ObjectActionIDRollback])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_Rollback]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_StartEdit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_StartEdit] FOREIGN KEY([ObjectActionIDStartEdit])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectAction_StartEdit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_CancelEdit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_CancelEdit] FOREIGN KEY([ObjectMethodIDCancelEdit])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_CancelEdit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Commit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Commit] FOREIGN KEY([ObjectMethodIDCommit])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Commit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_ConfirmEdit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_ConfirmEdit] FOREIGN KEY([ObjectMethodIDConfirmEdit])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_ConfirmEdit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Create]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Create] FOREIGN KEY([ObjectMethodIDCreate])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Create]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Delete]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Delete] FOREIGN KEY([ObjectMethodIDDelete])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Delete]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Load]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Load] FOREIGN KEY([ObjectMethodIDLoad])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Load]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Rollback]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Rollback] FOREIGN KEY([ObjectMethodIDRollback])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_Rollback]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_StartEdit]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_StartEdit] FOREIGN KEY([ObjectMethodIDStartEdit])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_StartEdit]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_UpdateVersion]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocument]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_UpdateVersion] FOREIGN KEY([ObjectMethodIDUpdateVersion])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocument] CHECK CONSTRAINT [FK_Metadata_MultiversionDocument_Metadata_ObjectMethod_UpdateVersion]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocumentListView_Metadata_DBView]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_DBView] FOREIGN KEY([DBViewID])
REFERENCES [dbo].[Metadata_DBView] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView] CHECK CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_DBView]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocumentListView_Metadata_MultiversionDocument1]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_MultiversionDocument1] FOREIGN KEY([MultiversionDocumentID])
REFERENCES [dbo].[Metadata_MultiversionDocument] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView] CHECK CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_MultiversionDocument1]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocumentListView_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_Object] FOREIGN KEY([ID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView] CHECK CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocumentListView_Metadata_ObjectAction_LoadList]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_ObjectAction_LoadList] FOREIGN KEY([ObjectActionIDLoadList])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView] CHECK CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_ObjectAction_LoadList]
GO
/****** Object:  ForeignKey [FK_Metadata_MultiversionDocumentListView_Metadata_ObjectMethod_LoadList]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_ObjectMethod_LoadList] FOREIGN KEY([ObjectMethodIDLoadList])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_MultiversionDocumentListView] CHECK CONSTRAINT [FK_Metadata_MultiversionDocumentListView_Metadata_ObjectMethod_LoadList]
GO
/****** Object:  ForeignKey [FK_Metadata_Object_Metadata_Namespace]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_Object]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_Object_Metadata_Namespace] FOREIGN KEY([NamespaceID])
REFERENCES [dbo].[Metadata_Namespace] ([ID])
GO
ALTER TABLE [dbo].[Metadata_Object] CHECK CONSTRAINT [FK_Metadata_Object_Metadata_Namespace]
GO
/****** Object:  ForeignKey [FK_Metadata_Object_Metadata_ObjectType]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_Object]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_Object_Metadata_ObjectType] FOREIGN KEY([ObjectTypeID])
REFERENCES [dbo].[Metadata_ObjectType] ([ID])
GO
ALTER TABLE [dbo].[Metadata_Object] CHECK CONSTRAINT [FK_Metadata_Object_Metadata_ObjectType]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectAction_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAction]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectAction_Metadata_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectAction] CHECK CONSTRAINT [FK_Metadata_ObjectAction_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectAttribute_Metadata_DataType]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_DataType] FOREIGN KEY([DataTypeID])
REFERENCES [dbo].[Metadata_DataType] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectAttribute] CHECK CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_DataType]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectAttribute_Metadata_DateTimeFormat]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_DateTimeFormat] FOREIGN KEY([DateTimeFormatID])
REFERENCES [dbo].[Metadata_DateTimeFormat] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectAttribute] CHECK CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_DateTimeFormat]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectAttribute_Metadata_DBTableColumn]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_DBTableColumn] FOREIGN KEY([DBTableColumnID])
REFERENCES [dbo].[Metadata_DBTableColumn] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectAttribute] CHECK CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_DBTableColumn]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectAttribute_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectAttribute]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectAttribute] CHECK CONSTRAINT [FK_Metadata_ObjectAttribute_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectMethod_Metadata_DBStoredProcedure]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectMethod]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectMethod_Metadata_DBStoredProcedure] FOREIGN KEY([DBStoredProcedureID])
REFERENCES [dbo].[Metadata_DBStoredProcedure] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectMethod] CHECK CONSTRAINT [FK_Metadata_ObjectMethod_Metadata_DBStoredProcedure]
GO
/****** Object:  ForeignKey [FK_Metadata_ObjectMethod_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_ObjectMethod]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ObjectMethod_Metadata_Object] FOREIGN KEY([ObjectID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_ObjectMethod] CHECK CONSTRAINT [FK_Metadata_ObjectMethod_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_DBForeignKey_ForeignKeyToParentObject]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBForeignKey_ForeignKeyToParentObject] FOREIGN KEY([DBForeignKeyToParentObjectID])
REFERENCES [dbo].[Metadata_DBForeignKey] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBForeignKey_ForeignKeyToParentObject]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_DBTable]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBTable] FOREIGN KEY([DBTableID])
REFERENCES [dbo].[Metadata_DBTable] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBTable]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_DBTableColumn]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBTableColumn] FOREIGN KEY([LogicalDeletionDBTableColumnID])
REFERENCES [dbo].[Metadata_DBTableColumn] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBTableColumn]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_DBView]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBView] FOREIGN KEY([DBViewID])
REFERENCES [dbo].[Metadata_DBView] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_DBView]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_Object] FOREIGN KEY([ID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_Object_ParentObject]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_Object_ParentObject] FOREIGN KEY([ParentObjectID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_Object_ParentObject]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectAction_Delete]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Delete] FOREIGN KEY([ObjectActionIDDelete])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Delete]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectAction_Insert]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Insert] FOREIGN KEY([ObjectActionIDInsert])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Insert]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectAction_Pick]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Pick] FOREIGN KEY([ObjectActionIDPick])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Pick]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectAction_Update]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Update] FOREIGN KEY([ObjectActionIDUpdate])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_Update]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectAction_View]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_View] FOREIGN KEY([ObjectActionIDView])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectAction_View]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectMethod_CopyByParentObject]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_CopyByParentObject] FOREIGN KEY([ObjectMethodIDCopyByParentObject])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_CopyByParentObject]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectMethod_Delete]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Delete] FOREIGN KEY([ObjectMethodIDDelete])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Delete]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectMethod_DeleteByParentObject]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_DeleteByParentObject] FOREIGN KEY([ObjectMethodIDDeleteByParentObject])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_DeleteByParentObject]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectMethod_Insert]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Insert] FOREIGN KEY([ObjectMethodIDInsert])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Insert]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectMethod_Load]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Load] FOREIGN KEY([ObjectMethodIDLoad])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Load]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObject_Metadata_ObjectMethod_Update]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObject]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Update] FOREIGN KEY([ObjectMethodIDUpdate])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObject] CHECK CONSTRAINT [FK_Metadata_PlainObject_Metadata_ObjectMethod_Update]
GO
/****** Object:  ForeignKey [FK_Metadata_ListView_Metadata_DBView]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObjectListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_ListView_Metadata_DBView] FOREIGN KEY([DBViewID])
REFERENCES [dbo].[Metadata_DBView] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObjectListView] CHECK CONSTRAINT [FK_Metadata_ListView_Metadata_DBView]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObjectListView_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObjectListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_Object] FOREIGN KEY([ID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObjectListView] CHECK CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObjectListView_Metadata_ObjectAction_LoadList]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObjectListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_ObjectAction_LoadList] FOREIGN KEY([ObjectActionIDLoadList])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObjectListView] CHECK CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_ObjectAction_LoadList]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObjectListView_Metadata_ObjectMethod]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObjectListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_ObjectMethod] FOREIGN KEY([ObjectMethodIDLoadList])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObjectListView] CHECK CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_ObjectMethod]
GO
/****** Object:  ForeignKey [FK_Metadata_PlainObjectListView_Metadata_PlainObject]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_PlainObjectListView]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_PlainObject] FOREIGN KEY([EntityID])
REFERENCES [dbo].[Metadata_PlainObject] ([ID])
GO
ALTER TABLE [dbo].[Metadata_PlainObjectListView] CHECK CONSTRAINT [FK_Metadata_PlainObjectListView_Metadata_PlainObject]
GO
/****** Object:  ForeignKey [FK_Metadata_StoredQuery_Metadata_Object]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_StoredQuery]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_StoredQuery_Metadata_Object] FOREIGN KEY([ID])
REFERENCES [dbo].[Metadata_Object] ([ID])
GO
ALTER TABLE [dbo].[Metadata_StoredQuery] CHECK CONSTRAINT [FK_Metadata_StoredQuery_Metadata_Object]
GO
/****** Object:  ForeignKey [FK_Metadata_StoredQuery_Metadata_ObjectAction]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_StoredQuery]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_StoredQuery_Metadata_ObjectAction] FOREIGN KEY([ObjectActionIDLoadList])
REFERENCES [dbo].[Metadata_ObjectAction] ([ID])
GO
ALTER TABLE [dbo].[Metadata_StoredQuery] CHECK CONSTRAINT [FK_Metadata_StoredQuery_Metadata_ObjectAction]
GO
/****** Object:  ForeignKey [FK_Metadata_StoredQuery_Metadata_ObjectMethod]    Script Date: 09/06/2008 23:53:41 ******/
ALTER TABLE [dbo].[Metadata_StoredQuery]  WITH CHECK ADD  CONSTRAINT [FK_Metadata_StoredQuery_Metadata_ObjectMethod] FOREIGN KEY([ObjectMethodIDLoadList])
REFERENCES [dbo].[Metadata_ObjectMethod] ([ID])
GO
ALTER TABLE [dbo].[Metadata_StoredQuery] CHECK CONSTRAINT [FK_Metadata_StoredQuery_Metadata_ObjectMethod]
GO

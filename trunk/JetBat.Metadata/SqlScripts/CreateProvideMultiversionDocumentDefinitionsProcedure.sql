USE [{0}]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE ProvideMultiversionDocumentDefinitions
AS
SET XACT_ABORT ON
BEGIN TRANSACTION

INSERT INTO [{1}].dbo.MultiversionDocument_DocumentDefinition
(
	ID,
	Namespace,
	[Name]
)
SELECT
	Metadata_Object.ID,
	Metadata_Namespace.[Name],
	Metadata_Object.[Name]
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
WHERE
	Metadata_Object.ObjectTypeID = 3/*Multiversion Document*/
	AND NOT EXISTS (SELECT 0 FROM [{1}].dbo.MultiversionDocument_DocumentDefinition WHERE [{1}].dbo.MultiversionDocument_DocumentDefinition.ID = Metadata_Object.ID)
	
UPDATE [{1}].dbo.MultiversionDocument_DocumentDefinition SET
	[{1}].dbo.MultiversionDocument_DocumentDefinition.ID = Metadata_Object.ID
FROM [{1}].dbo.MultiversionDocument_DocumentDefinition
	INNER JOIN Metadata_Object
		ON [{1}].dbo.MultiversionDocument_DocumentDefinition.ID != Metadata_Object.ID	
		AND Metadata_Object.[Name] = [{1}].dbo.MultiversionDocument_DocumentDefinition.[Name]
		AND Metadata_Object.ObjectTypeID = 3/*Multiversion Document*/
	INNER JOIN Metadata_Namespace
		ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
		AND Metadata_Namespace.Name = [{1}].dbo.MultiversionDocument_DocumentDefinition.Namespace

COMMIT TRANSACTION

GO
USE [{0}]
GO
/****** Object:  StoredProcedure [dbo].[MetadataEngine_UpdateMetadata]    Script Date: 07/03/2008 22:35:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MetadataEngine_UpdateMetadata]
AS
SET XACT_ABORT ON
BEGIN TRANSACTION

--Inserting and updating tables and views
INSERT INTO Metadata_DBTable (Name)
SELECT name FROM [{1}].sys.tables WHERE NOT EXISTS (SELECT 0 FROM Metadata_DBTable WHERE Metadata_DBTable.Name = [{1}].sys.tables.name)
UPDATE Metadata_DBTable SET Valid = 0
WHERE NOT EXISTS (SELECT * FROM [{1}].sys.tables WHERE [{1}].sys.tables.name = Metadata_DBTable.Name)
INSERT INTO Metadata_DBView (Name)
SELECT name FROM [{1}].sys.views WHERE NOT EXISTS (SELECT 0 FROM Metadata_DBView WHERE Metadata_DBView.Name = [{1}].sys.views.name)
UPDATE Metadata_DBView SET valid = 0
WHERE NOT EXISTS (SELECT * FROM [{1}].sys.views WHERE [{1}].sys.views.name = Metadata_DbView.Name)

UPDATE Metadata_DBStoredProcedure SET Valid = 0, Existent = 0
WHERE NOT EXISTS (SELECT * FROM [{1}].sys.procedures WHERE [{1}].sys.procedures.name = Metadata_DBStoredProcedure.Name)

INSERT INTO Metadata_DBTableColumn
(
	DBTableID,
	Name,
	DataTypeID,
	AllowNull,
	IsPrimaryKeyMember,
	IsForeignKeyMember,
	IsIdentity,
	MaxLength,
	Precision,
	Scale
)
SELECT
	(SELECT ID FROM Metadata_DBTable JOIN [{1}].sys.objects ON Metadata_DBTable.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.columns.object_id) [ID],
	[{1}].sys.columns.name,
	(
		SELECT Metadata_DataType.ID FROM [{1}].sys.types 
			INNER JOIN Metadata_DataType ON Metadata_DataType.Name = [{1}].sys.types.name
		WHERE [{1}].sys.columns.user_type_id = [{1}].sys.types.user_type_id AND [{1}].sys.columns.system_type_id = [{1}].sys.types.system_type_id
	),
	[{1}].sys.columns.is_nullable,
	CASE WHEN EXISTS
	(
		SELECT 0
		FROM [{1}].sys.index_columns
			INNER JOIN [{1}].sys.indexes
				ON [{1}].sys.indexes.object_id = [{1}].sys.index_columns.object_id
				AND [{1}].sys.indexes.index_id = [{1}].sys.index_columns.index_id
			WHERE [{1}].sys.indexes.is_primary_key = 1
				AND [{1}].sys.index_columns.object_id = [{1}].sys.columns.object_id
				AND [{1}].sys.index_columns.column_id = [{1}].sys.columns.column_id
	) THEN 1 ELSE 0 END,
	CASE WHEN EXISTS
	(
		SELECT 0 FROM [{1}].sys.foreign_key_columns WHERE [{1}].sys.foreign_key_columns.parent_object_id = [{1}].sys.columns.object_id AND [{1}].sys.foreign_key_columns.parent_column_id = [{1}].sys.columns.column_id
	) THEN 1 ELSE 0 END,
	[{1}].sys.columns.is_identity,
	[{1}].sys.columns.max_length,
	[{1}].sys.columns.precision,
	[{1}].sys.columns.scale
FROM [{1}].sys.columns
WHERE EXISTS
	(
		SELECT ID FROM Metadata_DBTable
			INNER JOIN [{1}].sys.objects ON Metadata_DBTable.Name = [{1}].sys.objects.name
		WHERE [{1}].sys.objects.object_id = [{1}].sys.columns.object_id
	)
	AND	NOT EXISTS
	(
		SELECT 0 FROM Metadata_DBTableColumn 
		WHERE DBTableID =
			(
				SELECT ID FROM Metadata_DBTable
					INNER JOIN [{1}].sys.objects ON Metadata_DBTable.Name = [{1}].sys.objects.name
				WHERE [{1}].sys.objects.object_id = [{1}].sys.columns.object_id
			) AND
			Metadata_DBTableColumn.Name = [{1}].sys.columns.name
	)
ORDER BY name

INSERT INTO Metadata_DBViewColumn
(
	DBViewID,
	Name,
	DataTypeID,
	MaxLength,
	Precision,
	Scale
)
SELECT
	(SELECT ID FROM Metadata_DBView JOIN [{1}].sys.objects ON Metadata_DBView.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.columns.object_id) [ID],
	[{1}].sys.columns.name,
	(
		SELECT Metadata_DataType.ID FROM [{1}].sys.types 
			INNER JOIN Metadata_DataType ON Metadata_DataType.Name = [{1}].sys.types.name
		WHERE [{1}].sys.columns.user_type_id = [{1}].sys.types.user_type_id AND [{1}].sys.columns.system_type_id = [{1}].sys.types.system_type_id
	),
	[{1}].sys.columns.max_length,
	[{1}].sys.columns.precision,
	[{1}].sys.columns.scale
FROM [{1}].sys.columns 
WHERE EXISTS
	(
		SELECT ID FROM Metadata_DBView
			INNER JOIN [{1}].sys.objects ON Metadata_DBView.Name = [{1}].sys.objects.name
		WHERE [{1}].sys.objects.object_id = [{1}].sys.columns.object_id
	)
	AND	NOT EXISTS
	(
		SELECT 0 FROM Metadata_DBViewColumn 
		WHERE DBViewID =
			(
				SELECT ID FROM Metadata_DBView
					INNER JOIN [{1}].sys.objects ON Metadata_DBView.Name = [{1}].sys.objects.name
				WHERE [{1}].sys.objects.object_id = [{1}].sys.columns.object_id
			) AND
			Metadata_DBViewColumn.Name = [{1}].sys.columns.name
	)
ORDER BY name

INSERT INTO Metadata_DBForeignKey (Name, DBTableIDParent, DBTableIDChild)
SELECT name,
	(SELECT ID FROM Metadata_DBTable JOIN [{1}].sys.objects ON Metadata_DBTable.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.foreign_keys.parent_object_id) [parent_object_id],
	(SELECT ID FROM Metadata_DBTable JOIN [{1}].sys.objects ON Metadata_DBTable.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.foreign_keys.referenced_object_id) [child_object_id]
	FROM [{1}].sys.foreign_keys
	WHERE NOT EXISTS (SELECT * FROM Metadata_DBForeignKey WHERE Metadata_DBForeignKey.Name = [{1}].sys.foreign_keys.name)

INSERT INTO Metadata_DBForeignKeyColumn (DBForeignKeyID, DBTableColumnIDPrimaryKey, DBTableColumnIDForeignKey)
SELECT
	Metadata_DBForeignKey.ID,
	ReferencedDBColumn.ID,
	ParentDBColumn.ID
FROM [{1}].sys.foreign_keys
	JOIN Metadata_DBForeignKey
		ON Metadata_DBForeignKey.Name = [{1}].sys.foreign_keys.Name
	JOIN [{1}].sys.foreign_key_columns
		ON [{1}].sys.foreign_key_columns.constraint_object_id = [{1}].sys.foreign_keys.object_id
	JOIN [{1}].sys.columns referenced_column
		ON referenced_column.object_id = [{1}].sys.foreign_key_columns.referenced_object_id AND referenced_column.column_id = [{1}].sys.foreign_key_columns.referenced_column_id
	JOIN [{1}].sys.columns parent_column
		ON parent_column.object_id = [{1}].sys.foreign_key_columns.parent_object_id AND parent_column.column_id = [{1}].sys.foreign_key_columns.parent_column_id
	JOIN Metadata_DBTableColumn ReferencedDBColumn
		ON ReferencedDBColumn.Name = referenced_column.name AND ReferencedDBColumn.DBTableID = Metadata_DBForeignKey.DBTableIDChild
	JOIN Metadata_DBTableColumn ParentDBColumn
		ON ParentDBColumn.Name = parent_column.name AND ParentDBColumn.DBTableID = Metadata_DBForeignKey.DBTableIDParent
WHERE NOT EXISTS 
(
	SELECT 0 FROM Metadata_DBForeignKeyColumn 
	WHERE DBForeignKeyID = Metadata_DBForeignKey.ID AND 
		DBTableColumnIDPrimaryKey = ReferencedDBColumn.ID AND
		DBTableColumnIDForeignKey = ParentDBColumn.ID
)

INSERT INTO Metadata_DBStoredProcedure (Name)
SELECT name FROM [{1}].sys.procedures WHERE [{1}].sys.procedures.name NOT LIKE N'sp_%' AND NOT EXISTS (SELECT * FROM Metadata_DBStoredProcedure WHERE Metadata_DBStoredProcedure.Name = [{1}].sys.procedures.name)
UPDATE Metadata_DBStoredProcedure SET Existent = 1
WHERE EXISTS (SELECT * FROM [{1}].sys.procedures WHERE [{1}].sys.procedures.name = Metadata_DBStoredProcedure.Name)

INSERT INTO Metadata_DBStoredProcedureParameter (DBStoredProcedureID, Name, DataTypeID, IsOutput, MaxLength, Precision, Scale)
SELECT (SELECT ID FROM Metadata_DBStoredProcedure JOIN [{1}].sys.objects ON Metadata_DBStoredProcedure.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.parameters.object_id) [DBStoredProcedureID], 
	name,
	(SELECT ID FROM Metadata_DataType WHERE Name = (SELECT name FROM [{1}].sys.types WHERE [{1}].sys.types.system_type_id = [{1}].sys.parameters.system_type_id AND [{1}].sys.types.user_type_id = [{1}].sys.parameters.user_type_id)) [data_type_id],
	is_output,
	max_length,
	precision,
	scale
	FROM [{1}].sys.parameters
	WHERE (SELECT name FROM [{1}].sys.procedures WHERE [{1}].sys.procedures.object_id = [{1}].sys.parameters.object_id) NOT LIKE N'sp_%'
	AND (SELECT ID FROM Metadata_DBStoredProcedure JOIN [{1}].sys.objects ON Metadata_DBStoredProcedure.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.parameters.object_id) IS NOT NULL
	AND NOT EXISTS (SELECT * FROM Metadata_DBStoredProcedureParameter WHERE DBStoredProcedureID = (SELECT id FROM Metadata_DBStoredProcedure JOIN [{1}].sys.objects ON Metadata_DBStoredProcedure.Name = [{1}].sys.objects.name WHERE [{1}].sys.objects.object_id = [{1}].sys.parameters.object_id) AND Name = name)
	AND EXISTS (SELECT ID FROM Metadata_DataType WHERE Metadata_DataType.Name = (SELECT name FROM [{1}].sys.types WHERE [{1}].sys.types.system_type_id = [{1}].sys.parameters.system_type_id AND [{1}].sys.types.user_type_id = [{1}].sys.parameters.user_type_id))
	ORDER BY DBStoredProcedureID, name

COMMIT TRANSACTION

GO
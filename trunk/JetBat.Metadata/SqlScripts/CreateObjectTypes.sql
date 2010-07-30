USE [{0}]
GO
SET XACT_ABORT ON
BEGIN TRAN
INSERT INTO Metadata_ObjectType (ID, [Name], FriendlyName, Description)
VALUES (1, 'Entity', 'Сущность', NULL)
INSERT INTO Metadata_ObjectType (ID, [Name], FriendlyName, Description)
VALUES (2, 'ListView', 'Предстваление списка', NULL)
INSERT INTO Metadata_ObjectType (ID, [Name], FriendlyName, Description)
VALUES (3, 'MultiversionDocument', 'Мультиверсионный документ', NULL)
INSERT INTO Metadata_ObjectType (ID, [Name], FriendlyName, Description)
VALUES (4, 'StoredQuery', 'Сохраненный запрос', NULL)
INSERT INTO Metadata_ObjectType (ID, [Name], FriendlyName, Description)
VALUES (5, 'MultiversionDocumentListView', 'Представление списка многоверсионных документов', NULL)
COMMIT TRAN
GO
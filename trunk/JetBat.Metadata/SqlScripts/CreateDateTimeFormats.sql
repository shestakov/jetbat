USE [{0}]
GO
SET XACT_ABORT ON
BEGIN TRAN
INSERT INTO Metadata_DateTimeFormat (ID, [Name], FriendlyName, Description)
VALUES (1, 'Краткая дата, краткое вермя', 'Краткая дата, краткое время', NULL)
INSERT INTO Metadata_DateTimeFormat (ID, [Name], FriendlyName, Description)
VALUES (2, 'Краткая дата, без времени', 'Краткая дата, без времени', NULL)
INSERT INTO Metadata_DateTimeFormat (ID, [Name], FriendlyName, Description)
VALUES (3, 'Без даты, краткое время', 'Без даты, краткое время', NULL)
INSERT INTO Metadata_DateTimeFormat (ID, [Name], FriendlyName, Description)
VALUES (4, 'Без даты, полное время', 'Без даты, полное время', NULL)
COMMIT TRAN
GO
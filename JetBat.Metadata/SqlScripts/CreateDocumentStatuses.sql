USE [{0}]
IF NOT EXISTS (SELECT 0 FROM dbo.MultiversionDocument_DocumentState WHERE ID = 0)
	INSERT INTO dbo.MultiversionDocument_DocumentState (ID, Name, FriendlyName)	VALUES (0, 'создается', 'создается')
IF NOT EXISTS (SELECT 0 FROM dbo.MultiversionDocument_DocumentState WHERE ID = 1)
	INSERT INTO dbo.MultiversionDocument_DocumentState (ID, Name, FriendlyName) VALUES (1, 'черновик', 'черновик')
IF NOT EXISTS (SELECT 0 FROM dbo.MultiversionDocument_DocumentState WHERE ID = 2)
	INSERT INTO dbo.MultiversionDocument_DocumentState (ID, Name, FriendlyName) VALUES (2, 'проведен', 'проведен')
IF NOT EXISTS (SELECT 0 FROM dbo.MultiversionDocument_DocumentState WHERE ID = 100)
	INSERT INTO dbo.MultiversionDocument_DocumentState (ID, Name, FriendlyName) VALUES (100, 'удален', 'удален')

IF NOT EXISTS (SELECT 0 FROM dbo.MultiversionDocument_DocumentVersionState WHERE ID = 0)
	INSERT INTO dbo.MultiversionDocument_DocumentVersionState (ID, Name, FriendlyName) VALUES (0, 'редактируется', 'редактируется')
IF NOT EXISTS (SELECT 0 FROM dbo.MultiversionDocument_DocumentVersionState WHERE ID = 1)
	INSERT INTO dbo.MultiversionDocument_DocumentVersionState (ID, Name, FriendlyName) VALUES (1, 'сохранена', 'сохранена')
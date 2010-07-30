EXEC sp_configure 'clr enabled' , '1'
GO
reconfigure;
GO

--Assembly swiftshot.sqlserverpasswordutility, version=0.0.0.0, culture=neutral, publickeytoken=null, processorarchitecture=msil
CREATE ASSEMBLY [SqlServerPasswordUtility]
AUTHORIZATION [dbo]
FROM 0x4d5a90000300000004000000ffff0000b800000000000000400000000000000000000000000000000000000000000000000000000000000000000000800000000e1fba0e00b409cd21b8014ccd21546869732070726f6772616d2063616e6e6f742062652072756e20696e20444f53206d6f64652e0d0d0a2400000000000000\
504500004c01030085f2034c0000000000000000e00002210b010800000a00000006000000000000ce290000002000000040000000004000002000000002000004000000000000000400000000000000008000000002000000000000030040850000100000100000000010000010000000000000100000000000000000000000\
802900004b00000000400000d803000000000000000000000000000000000000006000000c000000d02800001c0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000200000080000000000000000000000082000004800000000000000000000002e74657874000000\
d409000000200000000a000000020000000000000000000000000000200000602e72737263000000d80300000040000000040000000c0000000000000000000000000000400000402e72656c6f6300000c0000000060000000020000001000000000000000000000000000004000004200000000000000000000000000000000\
b0290000000000004800000002000500e8200000e80700000100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000133002002000000001000011001f108d130000010a731100000a066f1200000a0006281300000a0b2b00072a13300500\
560000000200001100281400000a026f1500000a0a03281600000a0b078e69068e69588d130000010c07160816078e69281700000a00061608078e69068e69281700000a00731800000a0d09086f1900000a281300000a13042b0011042a1e02281a00000a2a000042534a4201000100000000000c00000076322e302e353037\
32370000000005006c00000078020000237e0000e4020000b803000023537472696e6773000000009c0600000800000023555300a4060000100000002347554944000000b40600003401000023426c6f620000000000000002000001471502000900000000fa013300160000010000001b000000020000000300000002000000\
1a0000000f00000002000000010000000200000000000a00010000000000060051004a000600980086000600af0086000600cc0086000600eb00860006000401860006001d01860006003801860006005301860006008b016c0106009f016c010600ad0186000600c60186000600f601e3013b000a0200000600390219020600\
590219020a00c102a6020600d6024a000600f802db0206001103db02060030034a00060053034703060079034a00060080034a0006009003db0206009c03db0200000000010000000000010001000100100031000000050001000100502000000000960058000a0001007c2000000000960065000e000100de20000000008618\
720014000300000001007800000002008100110072001800190072001800210072001800290072001800310072001800390072001800410072001800490072001800510072001d00590072001800610072001800690072001800710072002200810072002800890072001400910072001400a10072001400a90027034500b100\
38034b00b9005c035700b90027035c00b10068036200c10086036800d10072001400d900aa037300090072001400200083002d002e003300b4002e000b0086002e001300ae002e001b00ae002e002300ae002e002b0086002e005300cc002e003b00ae002e004b00ae002e006b0003012e007b0015012e006300f6002e007300\
0c01400083002d0051007a0004800000010000000000000000000000000077020000020000000000000000000000010041000000000002000000000000000000000001009a02000000000000003c4d6f64756c653e00537769667473686f742e53716c53657276657250617373776f72645574696c6974792e646c6c00506173\
73776f72644861736853514c006d73636f726c69620053797374656d004f626a6563740047656e657261746553616c74004861736850617373776f7264002e63746f720070617373776f72640073616c740053797374656d2e5265666c656374696f6e00417373656d626c795469746c6541747472696275746500417373656d\
626c794465736372697074696f6e41747472696275746500417373656d626c79436f6e66696775726174696f6e41747472696275746500417373656d626c79436f6d70616e7941747472696275746500417373656d626c7950726f6475637441747472696275746500417373656d626c79436f70797269676874417474726962\
75746500417373656d626c7954726164656d61726b41747472696275746500417373656d626c7943756c747572654174747269627574650053797374656d2e52756e74696d652e496e7465726f70536572766963657300436f6d56697369626c65417474726962757465004775696441747472696275746500417373656d626c\
7956657273696f6e41747472696275746500417373656d626c7946696c6556657273696f6e4174747269627574650053797374656d2e446961676e6f73746963730044656275676761626c6541747472696275746500446562756767696e674d6f6465730053797374656d2e52756e74696d652e436f6d70696c657253657276\
6963657300436f6d70696c6174696f6e52656c61786174696f6e734174747269627574650052756e74696d65436f6d7061746962696c69747941747472696275746500537769667473686f742e53716c53657276657250617373776f72645574696c6974790053797374656d2e44617461004d6963726f736f66742e53716c53\
65727665722e5365727665720053716c46756e6374696f6e41747472696275746500427974650053797374656d2e53656375726974792e43727970746f67726170687900524e4743727970746f5365727669636550726f76696465720052616e646f6d4e756d62657247656e657261746f7200476574427974657300436f6e76\
65727400546f426173653634537472696e670053797374656d2e5465787400456e636f64696e67006765745f556e69636f64650046726f6d426173653634537472696e670042756666657200417272617900426c6f636b436f707900534841314d616e616765640048617368416c676f726974686d00436f6d70757465486173\
6800000000032000000000006dcf3dd720ce6b46a7c7928a7dace1130008b77a5c561934e0890300000e0500020e0e0e03200001042001010e042001010205200101113d0420010108170100010054020f497344657465726d696e697374696301052001011d050500010e1d050507021d050e040000125d0520011d050e0500\
011d050e0a000501126508126508080620011d051d050b07051d051d051d0512690e27010022537769667473686f742e53716c53657276657250617373776f72645574696c697479000005010000000017010012436f7079726967687420c2a920203230313000002901002464613366396532372d646536612d346265632d38\
6364622d35613737343362313065313200000c010007312e302e302e3000000801000701000000000801000800000000001e01000100540216577261704e6f6e457863657074696f6e5468726f7773010000000085f2034c000000000200000094000000ec280000ec0a000052534453570aa1f710479c49b738b2df317514a6\
01000000443a5c576f726b5c73766e5c537769667473686f745c4170706c69636174696f6e5c7472756e6b5c537769667473686f742e53716c53657276657250617373776f72645574696c6974795c6f626a5c44656275675c537769667473686f742e53716c53657276657250617373776f72645574696c6974792e70646200\
a82900000000000000000000be290000002000000000000000000000000000000000000000000000b02900000000000000005f436f72446c6c4d61696e006d73636f7265652e646c6c0000000000ff25002040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\
00000000000000000000000000000100100000001800008000000000000000000000000000000100010000003000008000000000000000000000000000000100000000004800000058400000800300000000000000000000800334000000560053005f00560045005200530049004f004e005f0049004e0046004f0000000000\
bd04effe00000100000001000000000000000100000000003f000000000000000400000002000000000000000000000000000000440000000100560061007200460069006c00650049006e0066006f00000000002400040000005400720061006e0073006c006100740069006f006e00000000000000b004e002000001005300\
7400720069006e006700460069006c00650049006e0066006f000000bc0200000100300030003000300030003400620030000000700023000100460069006c0065004400650073006300720069007000740069006f006e000000000053007700690066007400730068006f0074002e00530071006c0053006500720076006500\
7200500061007300730077006f00720064005500740069006c0069007400790000000000300008000100460069006c006500560065007200730069006f006e000000000031002e0030002e0030002e003000000070002700010049006e007400650072006e0061006c004e0061006d0065000000530077006900660074007300\
68006f0074002e00530071006c00530065007200760065007200500061007300730077006f00720064005500740069006c006900740079002e0064006c006c00000000004800120001004c006500670061006c0043006f007000790072006900670068007400000043006f0070007900720069006700680074002000a9002000\
2000320030003100300000007800270001004f0072006900670069006e0061006c00460069006c0065006e0061006d006500000053007700690066007400730068006f0074002e00530071006c00530065007200760065007200500061007300730077006f00720064005500740069006c006900740079002e0064006c006c00\
00000000680023000100500072006f0064007500630074004e0061006d0065000000000053007700690066007400730068006f0074002e00530071006c00530065007200760065007200500061007300730077006f00720064005500740069006c0069007400790000000000340008000100500072006f006400750063007400\
560065007200730069006f006e00000031002e0030002e0030002e003000000038000800010041007300730065006d0062006c0079002000560065007200730069006f006e00000031002e0030002e0030002e003000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\
002000000c000000d03900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\
0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\
0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\
0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000

WITH PERMISSION_SET=SAFE
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION dbo.GenerateSalt()
RETURNS nvarchar(128)
WITH EXECUTE AS CALLER
EXTERNAL NAME [SqlServerPasswordUtility].[PasswordHashSQL].[GenerateSalt]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[HashPassword] (@password nvarchar(128), @salt nvarchar(128))
RETURNS nvarchar(128)
WITH EXECUTE AS CALLER
EXTERNAL NAME [SqlServerPasswordUtility].[PasswordHashSQL].[HashPassword]
GO

/****** Object:  Table [dbo].[MealCalc_GoodWriteOffDocumentDetail]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodWriteOffDocumentDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentVersionID] [int] NOT NULL,
	[SequenceNumber] [int] NOT NULL,
	[GoodIncomeID] [int] NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_MealCalc_GoodWriteOffDocumentDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodWriteOffDocumentDetail_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodWriteOffDocumentDetail_View]
AS
SELECT
	dbo.MealCalc_GoodWriteOffDocumentDetail.ID,
	dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID,
	dbo.MealCalc_GoodWriteOffDocumentDetail.SequenceNumber,
	dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID,
	dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity
FROM MealCalc_GoodWriteOffDocumentDetail
GO
/****** Object:  Table [dbo].[MealCalc_GoodWriteOffDocument]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodWriteOffDocument](
	[ID] [int] NOT NULL,
	[CalcDayID] [int] NULL,
	[WriteOffReasonID] [int] NULL,
	[DocumentDateTime] [datetime] NULL,
	[DocumentNumber] [nvarchar](30) NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_MealCalc_GoodWriteOffDocument] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodWriteOffDocument_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodWriteOffDocument_View]
AS
SELECT
	dbo.MealCalc_GoodWriteOffDocument.ID,
	dbo.MealCalc_GoodWriteOffDocument.CalcDayID,
	dbo.MealCalc_GoodWriteOffDocument.WriteOffReasonID,
	dbo.MealCalc_GoodWriteOffDocument.DocumentDateTime,
	dbo.MealCalc_GoodWriteOffDocument.DocumentNumber,
	dbo.MealCalc_GoodWriteOffDocument.Comment
FROM MealCalc_GoodWriteOffDocument
GO
/****** Object:  Table [dbo].[MealCalc_GoodPackingUnit]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodPackingUnit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GoodID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[KgPerPackingUnit] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_MealCalc_MeasureUnit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodPackingUnit_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodPackingUnit_View]
AS
SELECT
	ID,
	GoodID,
	[Name],
	KgPerPackingUnit
FROM
	dbo.MealCalc_GoodPackingUnit
GO
/****** Object:  Table [dbo].[MealCalc_GoodIncomeDocumentDetail]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentVersionID] [int] NOT NULL,
	[GoodID] [int] NULL,
	[GoodPackingUnitID] [int] NULL,
	[GoodCommodityName] [nvarchar](200) NOT NULL,
	[Price] [decimal](12, 2) NULL,
	[Quantity] [decimal](18, 3) NULL,
	[Comment] [nvarchar](200) NULL,
	[OrderNumber] [int] NOT NULL,
 CONSTRAINT [PK_MealCalc_GoodIncomeGood] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodIncomeDocumentDetail_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncomeDocumentDetail_View]
AS
SELECT
	ID,
	DocumentVersionID,
	GoodID,
	GoodPackingUnitID,
	GoodCommodityName,
	Price,
	Quantity,
	Comment,
	OrderNumber
FROM dbo.MealCalc_GoodIncomeDocumentDetail
GO
/****** Object:  Table [dbo].[MealCalc_GoodIncomeDocument]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodIncomeDocument](
	[ID] [int] NOT NULL,
	[SupplierID] [int] NULL,
	[IncomeCalcDayID] [int] NULL,
	[DocumentDateTime] [datetime] NULL,
	[InvoiceNumber] [nvarchar](30) NULL,
 CONSTRAINT [PK_MealCalc_GoodIncome] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodIncomeDocument_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncomeDocument_View]
AS
SELECT
	ID,
	SupplierID,
	IncomeCalcDayID,
	DocumentDateTime,
	InvoiceNumber
FROM MealCalc_GoodIncomeDocument
GO
/****** Object:  Table [dbo].[MealCalc_GoodIncome]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodIncome](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDayID] [int] NOT NULL,
	[GoodID] [int] NOT NULL,
	[GoodIncomeDocumentDetailID] [int] NOT NULL,
	[GoodCommodityName] [nvarchar](500) NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[QuantityWrittenOff] [decimal](18, 3) NOT NULL,
	[QuantityReserved] [decimal](18, 3) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_GoodIncome_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodIncome_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncome_View]
AS
SELECT
	dbo.MealCalc_GoodIncome.ID,
	dbo.MealCalc_GoodIncome.CalcDayID,
	dbo.MealCalc_GoodIncome.GoodID,
	dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID,
	dbo.MealCalc_GoodIncome.GoodCommodityName,
	dbo.MealCalc_GoodIncome.Price,
	dbo.MealCalc_GoodIncome.Quantity,
	dbo.MealCalc_GoodIncome.Comment,
	dbo.MealCalc_GoodIncome.QuantityWrittenOff,
	dbo.MealCalc_GoodIncome.QuantityReserved
FROM dbo.MealCalc_GoodIncome
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDayMenuGoodSpendID] [int] NOT NULL,
	[GoodIncomeID] [int] NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_MealCalc_CalcDayMenuGoodSpendWriteOff] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_Good](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DefaultGoodPackingUnitID] [int] NULL,
	[GoodCategoryID] [int] NOT NULL,
	[Fat] [decimal](18, 3) NULL,
	[Proteins] [decimal](18, 3) NULL,
	[Carbohydrates] [decimal](18, 3) NULL,
	[FoodValue] [decimal](18, 3) NULL,
 CONSTRAINT [PK_MealCalc_Good] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UI_MealCalc_Good] ON [dbo].[MealCalc_Good] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodWriteOffDocumentDetail_ListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodWriteOffDocumentDetail_ListView]
AS
SELECT
	dbo.MealCalc_GoodWriteOffDocumentDetail.ID,
	dbo.MealCalc_GoodWriteOffDocumentDetail.DocumentVersionID,
	dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID,
	dbo.MealCalc_GoodWriteOffDocumentDetail.SequenceNumber,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_GoodIncome.GoodCommodityName AS GoodCommodityName,
	dbo.MealCalc_GoodIncome.Price AS GoodIncomePrice,
	dbo.MealCalc_GoodWriteOffDocumentDetail.Quantity
FROM MealCalc_GoodWriteOffDocumentDetail
	INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_GoodWriteOffDocumentDetail.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	INNER JOIN dbo.MealCalc_Good ON dbo.MealCalc_GoodIncome.GoodID = dbo.MealCalc_Good.ID
GO
/****** Object:  Table [dbo].[MealCalc_GoodSpendDocumentDetail]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodSpendDocumentDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentVersionID] [int] NOT NULL,
	[SequenceNumber] [int] NOT NULL,
	[GoodID] [int] NOT NULL,
	[GoodCommodityName] [nvarchar](500) NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_MealCalc_GoodSpendDocumentDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodSpendDocumentDetail_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodSpendDocumentDetail_View]
AS
SELECT
	dbo.MealCalc_GoodSpendDocumentDetail.ID,
	dbo.MealCalc_GoodSpendDocumentDetail.DocumentVersionID,
	dbo.MealCalc_GoodSpendDocumentDetail.GoodID,
	dbo.MealCalc_GoodSpendDocumentDetail.SequenceNumber,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_GoodSpendDocumentDetail.GoodCommodityName,
	dbo.MealCalc_GoodSpendDocumentDetail.Price,
	dbo.MealCalc_GoodSpendDocumentDetail.Quantity
FROM dbo.MealCalc_GoodSpendDocumentDetail
	LEFT JOIN MealCalc_Good ON dbo.MealCalc_GoodSpendDocumentDetail.GoodID = dbo.MealCalc_Good.ID
GO
/****** Object:  View [dbo].[MealCalc_GoodIncomeDocumentDetail_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncomeDocumentDetail_ListView]
AS
SELECT
	OrderNumber,
	MealCalc_Good.Name [GoodName],
	dbo.MealCalc_GoodPackingUnit.[Name] AS [GoodPackingUnitName],
	Quantity,
	Price,
	(Quantity * Price) AS [Total],
	Comment,
	MealCalc_GoodIncomeDocumentDetail.ID,
	DocumentVersionID
FROM dbo.MealCalc_GoodIncomeDocumentDetail
	LEFT JOIN dbo.MealCalc_Good
		ON MealCalc_Good.ID = MealCalc_GoodIncomeDocumentDetail.GoodID
	LEFT JOIN dbo.MealCalc_GoodPackingUnit
		ON dbo.MealCalc_GoodIncomeDocumentDetail.GoodPackingUnitID = dbo.MealCalc_GoodPackingUnit.ID
GO
/****** Object:  View [dbo].[MealCalc_Good_PickListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Good_PickListView]
AS
SELECT
	dbo.MealCalc_Good.ID,
	dbo.MealCalc_Good.Name,
	dbo.MealCalc_Good.GoodCategoryID
FROM dbo.MealCalc_Good
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayMenuGoodSpend]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDayMenuID] [int] NOT NULL,
	[CalcDayMealDishID] [int] NULL,
	[GoodID] [int] NOT NULL,
	[QuantityPlan] [decimal](18, 3) NULL,
	[QuantityFact] [decimal](18, 3) NULL,
 CONSTRAINT [PK_MealCalc_CalcDayMenuGoodSpend] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenuGoodSpend_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenuGoodSpend_View]
AS
SELECT
	dbo.MealCalc_CalcDayMenuGoodSpend.ID,
	dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID,
	dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID,
	dbo.MealCalc_CalcDayMenuGoodSpend.GoodID,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_CalcDayMenuGoodSpend.QuantityPlan,
	dbo.MealCalc_CalcDayMenuGoodSpend.QuantityFact
FROM MealCalc_CalcDayMenuGoodSpend
	INNER JOIN MealCalc_Good ON dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_Good.ID
GO
/****** Object:  Table [dbo].[MealCalc_DishGood]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_DishGood](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DishID] [int] NOT NULL,
	[GoodID] [int] NOT NULL,
	[Quantity] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_MealCalc_DishGood] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_DishGood_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_DishGood_View]
AS
SELECT     ID, DishID, GoodID, Quantity
FROM         dbo.MealCalc_DishGood
GO
/****** Object:  Table [dbo].[MealCalc_AccountingPeriodGoodReportGood]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_AccountingPeriodGoodReportGood](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentVersionID] [int] NOT NULL,
	[GoodID] [int] NULL,
	[Price] [decimal](18, 3) NULL,
	[BalanceStart] [decimal](18, 3) NULL,
	[BalanceEnd] [decimal](18, 3) NULL,
	[OrderNumber] [int] NOT NULL,
 CONSTRAINT [PK_MealCalc_AccountingPeriodGoodReportGood] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_Menu]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_GoodCategory]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_MealCalc_GoodCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [UI_MealCalc_GoodCategory] ON [dbo].[MealCalc_GoodCategory] 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_Good_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Good_View]
AS
SELECT
	dbo.MealCalc_GoodCategory.Name AS GoodCategory_Name,
	dbo.MealCalc_Good.Name,	
	dbo.MealCalc_Good.ID,
	dbo.MealCalc_Good.GoodCategoryID,
	dbo.MealCalc_Good.DefaultGoodPackingUnitID,
	dbo.MealCalc_Good.Fat,
	dbo.MealCalc_Good.Proteins,
	dbo.MealCalc_Good.Carbohydrates,
	dbo.MealCalc_Good.FoodValue
FROM dbo.MealCalc_Good 
	LEFT JOIN dbo.MealCalc_GoodCategory
		ON dbo.MealCalc_GoodCategory.ID = dbo.MealCalc_Good.GoodCategoryID
GO
/****** Object:  View [dbo].[MealCalc_DishGood_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_DishGood_ListView]
AS
SELECT     dbo.MealCalc_DishGood.ID, dbo.MealCalc_DishGood.DishID, dbo.MealCalc_Good.Name AS Good_Name, dbo.MealCalc_DishGood.Quantity, 
                      dbo.MealCalc_GoodCategory.Name AS GoodCategory_Name
FROM         dbo.MealCalc_GoodCategory RIGHT OUTER JOIN
                      dbo.MealCalc_Good ON dbo.MealCalc_GoodCategory.ID = dbo.MealCalc_Good.GoodCategoryID RIGHT OUTER JOIN
                      dbo.MealCalc_DishGood ON dbo.MealCalc_Good.ID = dbo.MealCalc_DishGood.GoodID
GO
/****** Object:  Table [dbo].[MealCalc_MenuTemplate]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_MenuTemplate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](2000) NULL,
 CONSTRAINT [PK_MealCalc_MenuTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_Organization]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_Organization](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_Organization] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_Supplier]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_Supplier](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_Supplier] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_WriteOffReason]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_WriteOffReason](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MealCalc_WriteOffReason] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodWriteOffDocument_ListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodWriteOffDocument_ListView]
AS
SELECT
	dbo.MultiversionDocument_Document.ID,
	dbo.MultiversionDocument_Document.DocumentStateID,
	dbo.MealCalc_GoodWriteOffDocument.CalcDayID,
	dbo.MealCalc_GoodWriteOffDocument.WriteOffReasonID,
	MultiversionDocument_DocumentState.FriendlyName AS StateName,
	dbo.MealCalc_GoodWriteOffDocument.DocumentDateTime,
	dbo.MealCalc_GoodWriteOffDocument.DocumentNumber,
	dbo.MealCalc_WriteOffReason.Name AS WriteOffReasonName,
	dbo.MealCalc_GoodWriteOffDocument.Comment
FROM dbo.MultiversionDocument_Document
	INNER JOIN dbo.MultiversionDocument_DocumentState ON dbo.MultiversionDocument_DocumentState.ID = dbo.MultiversionDocument_Document.DocumentStateID
	INNER JOIN dbo.MealCalc_GoodWriteOffDocument ON dbo.MultiversionDocument_Document.CurrentVersionID = dbo.MealCalc_GoodWriteOffDocument.ID
	LEFT JOIN dbo.MealCalc_WriteOffReason ON dbo.MealCalc_GoodWriteOffDocument.WriteOffReasonID = dbo.MealCalc_WriteOffReason.ID
GO
/****** Object:  Table [dbo].[MealCalc_DishCategory]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_DishCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IndexNumber] [int] NOT NULL,
 CONSTRAINT [PK_MealCalc_DishCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayMenuStatus]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayMenuStatus](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MealCalc_CalcDayMenuStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_AccountingPeriodGoodReport]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_AccountingPeriodGoodReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentVersionID] [int] NOT NULL,
	[AccountingPeriodID] [int] NULL,
	[ReportNumber] [nvarchar](30) NULL,
	[ReportDateTime] [datetime] NULL,
	[ReportAuthorName] [nvarchar](50) NULL,
	[ReportAuthorTableNumber] [nvarchar](20) NULL,
 CONSTRAINT [PK_MealCalc_AccountingPeriodGoodReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_AccountingPeriodGoodBalance]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_AccountingPeriodGoodBalance](
	[AccountingPeriodID] [int] NOT NULL,
	[GoodID] [int] NOT NULL,
	[Price] [decimal](18, 3) NOT NULL,
	[Balance] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_MealCalc_GoodBalance] PRIMARY KEY CLUSTERED 
(
	[AccountingPeriodID] ASC,
	[GoodID] ASC,
	[Price] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_AccountingPeriod]    Script Date: 11/30/2009 00:43:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_AccountingPeriod](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[Closed] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_AccountingPeriod] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_CalcDay]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDay](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDate] [datetime] NOT NULL,
	[Comment] [nvarchar](200) NULL,
 CONSTRAINT [PK_MealCalc_CalcDay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodIncomeDocument_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncomeDocument_ListView]
AS
SELECT
	MultiversionDocument_DocumentState.FriendlyName AS StateName,
	dbo.MultiversionDocument_Document.ID,
	dbo.MultiversionDocument_Document.DocumentStateID,
	dbo.MealCalc_GoodIncomeDocument.SupplierID,
	dbo.MealCalc_CalcDay.CalcDate AS IncomeCalcDayDate,
	dbo.MealCalc_Supplier.Name AS SupplierName,
	dbo.MealCalc_GoodIncomeDocument.DocumentDateTime,
	dbo.MealCalc_GoodIncomeDocument.InvoiceNumber
FROM dbo.MultiversionDocument_Document
	JOIN dbo.MultiversionDocument_DocumentVersion 
		ON dbo.MultiversionDocument_Document.CurrentVersionID = dbo.MultiversionDocument_DocumentVersion.ID 
	JOIN dbo.MealCalc_GoodIncomeDocument
		ON dbo.MultiversionDocument_DocumentVersion.ID = dbo.MealCalc_GoodIncomeDocument.ID
	LEFT JOIN dbo.MealCalc_Supplier
		ON dbo.MealCalc_Supplier.ID = dbo.MealCalc_GoodIncomeDocument.SupplierID
	LEFT JOIN dbo.MultiversionDocument_DocumentState
		ON dbo.MultiversionDocument_DocumentState.ID = dbo.MultiversionDocument_Document.DocumentStateID
	LEFT JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID = dbo.MealCalc_CalcDay.ID
WHERE MultiversionDocument_Document.DocumentStateID != 0 AND MultiversionDocument_Document.DocumentStateID != 100
GO
/****** Object:  View [dbo].[MealCalc_GoodIncome_PickListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncome_PickListView]
AS
SELECT
	dbo.MealCalc_GoodIncome.ID,
	dbo.MealCalc_GoodIncome.CalcDayID,
	dbo.MealCalc_GoodIncome.GoodID,
	dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID,
	dbo.MealCalc_GoodIncome.GoodCommodityName,
	dbo.MealCalc_GoodIncome.Price,
	dbo.MealCalc_GoodIncome.Quantity - dbo.MealCalc_GoodIncome.QuantityWrittenOff - dbo.MealCalc_GoodIncome.QuantityReserved AS QuantityLeft,
	dbo.MealCalc_CalcDay.CalcDate AS SupplyDate,
	dbo.MealCalc_Supplier.Name AS SupplierName
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID = dbo.MealCalc_GoodIncomeDocumentDetail.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
	INNER JOIN dbo.MealCalc_Supplier ON dbo.MealCalc_GoodIncomeDocument.SupplierID = dbo.MealCalc_Supplier.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID = dbo.MealCalc_CalcDay.ID
--WHERE dbo.MealCalc_GoodIncome.Quantity > dbo.MealCalc_GoodIncome.QuantityWrittenOff + dbo.MealCalc_GoodIncome.QuantityReserved
WHERE dbo.MealCalc_GoodIncome.Deleted = 0
GO
/****** Object:  View [dbo].[MealCalc_GoodIncome_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodIncome_ListView]
AS
SELECT
	dbo.MealCalc_GoodIncome.ID,
	dbo.MealCalc_GoodIncome.CalcDayID,
	dbo.MealCalc_GoodIncome.GoodID,
	dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID,
	dbo.MealCalc_GoodIncome.GoodCommodityName,
	dbo.MealCalc_GoodIncome.Price,
	dbo.MealCalc_GoodIncome.Quantity,
	dbo.MealCalc_CalcDay.CalcDate AS SupplyDate,
	dbo.MealCalc_Supplier.Name AS SupplierName,
	dbo.MealCalc_GoodIncome.Comment,
	dbo.MealCalc_GoodIncome.QuantityWrittenOff,
	dbo.MealCalc_GoodIncome.QuantityReserved
FROM dbo.MealCalc_GoodIncome
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID = dbo.MealCalc_GoodIncomeDocumentDetail.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
	INNER JOIN dbo.MealCalc_Supplier ON dbo.MealCalc_GoodIncomeDocument.SupplierID = dbo.MealCalc_Supplier.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID = dbo.MealCalc_CalcDay.ID
--WHERE dbo.MealCalc_GoodIncome.Quantity > dbo.MealCalc_GoodIncome.QuantityWrittenOff + dbo.MealCalc_GoodIncome.QuantityReserved
WHERE dbo.MealCalc_GoodIncome.Deleted = 0
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayGoodBalanceChange]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayGoodBalanceChange](
	[ID] [int] NOT NULL,
	[CalcDayID] [int] NOT NULL,
	[GoodID] [int] NOT NULL,
	[GoodCommodityName] [nvarchar](500) NOT NULL,
	[Price] [decimal](12, 2) NOT NULL,
	[QuantityIncome] [decimal](18, 3) NOT NULL,
	[QuantityReserved] [decimal](18, 3) NOT NULL,
	[QuantityWrittenOff] [decimal](18, 3) NOT NULL,
 CONSTRAINT [PK_MealCalc_CalcDayGoodBalance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayMenu]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayMenu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDayID] [int] NOT NULL,
	[MenuID] [int] NOT NULL,
	[Comment] [nvarchar](2000) NULL,
	[CalcDayMenuStatusID] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_CalcDayMenu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_GoodSpendDocument]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_GoodSpendDocument](
	[ID] [int] NOT NULL,
	[CalcDayMenuID] [int] NULL,
	[DocumentDateTime] [datetime] NULL,
	[DocumentNumber] [nvarchar](30) NULL,
	[Comment] [nvarchar](1000) NULL,
 CONSTRAINT [PK_MealCalc_GoodSpendDocument] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodSpendDocument_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodSpendDocument_View]
AS
SELECT
	dbo.MealCalc_GoodSpendDocument.ID,
	dbo.MealCalc_GoodSpendDocument.CalcDayMenuID,
	dbo.MealCalc_GoodSpendDocument.DocumentDateTime,
	dbo.MealCalc_GoodSpendDocument.DocumentNumber,
	dbo.MealCalc_GoodSpendDocument.Comment,
	dbo.MealCalc_CalcDay.CalcDate AS CalcDayDate,
	dbo.MealCalc_Menu.Name AS MenuName
FROM dbo.MealCalc_GoodSpendDocument
	LEFT JOIN dbo.MealCalc_CalcDayMenu ON dbo.MealCalc_GoodSpendDocument.CalcDayMenuID = dbo.MealCalc_CalcDayMenu.ID
	INNER JOIN dbo.MealCalc_Menu ON dbo.MealCalc_CalcDayMenu.MenuID = dbo.MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
GO
/****** Object:  View [dbo].[MealCalc_GoodSpendDocument_ListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodSpendDocument_ListView]
AS
SELECT
	dbo.MultiversionDocument_Document.ID,
	dbo.MultiversionDocument_Document.DocumentStateID,
	MultiversionDocument_DocumentState.FriendlyName AS StateName,
	dbo.MealCalc_CalcDay.CalcDate AS CalcDayDate,
	dbo.MealCalc_Menu.Name AS CalcDayMenuName,
	dbo.MealCalc_GoodSpendDocument.DocumentDateTime,
	dbo.MealCalc_GoodSpendDocument.DocumentNumber
FROM dbo.MultiversionDocument_Document
	JOIN dbo.MultiversionDocument_DocumentVersion 
		ON dbo.MultiversionDocument_Document.CurrentVersionID = dbo.MultiversionDocument_DocumentVersion.ID 
	JOIN dbo.MealCalc_GoodSpendDocument
		ON dbo.MultiversionDocument_DocumentVersion.ID = dbo.MealCalc_GoodSpendDocument.ID
	JOIN dbo.MultiversionDocument_DocumentState
		ON dbo.MultiversionDocument_DocumentState.ID = dbo.MultiversionDocument_Document.DocumentStateID
	LEFT JOIN dbo.MealCalc_CalcDayMenu ON dbo.MealCalc_CalcDayMenu.ID = dbo.MealCalc_GoodSpendDocument.CalcDayMenuID
	LEFT JOIN MealCalc_Menu ON dbo.MealCalc_CalcDayMenu.MenuID = dbo.MealCalc_Menu.ID
	LEFT JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_CalcDayMenu.CalcDayID = dbo.MealCalc_CalcDay.ID
WHERE MultiversionDocument_Document.DocumentStateID != 0 AND MultiversionDocument_Document.DocumentStateID != 100
GO
/****** Object:  Table [dbo].[MealCalc_Dish]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_Dish](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DishCategoryID] [int] NOT NULL,
	[ReceiptCode] [nvarchar](20) NULL,
	[PortionCount] [int] NULL,
	[WorkOut] [nvarchar](50) NULL,
	[PortionWeight] [int] NULL,
 CONSTRAINT [PK_MealCalc_Dish] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_GoodCategory_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_GoodCategory_View]
AS
SELECT     ID, Name
FROM         dbo.MealCalc_GoodCategory
GO
/****** Object:  View [dbo].[MealCalc_DishCategory_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_DishCategory_View]
AS
SELECT     ID, Name, IndexNumber
FROM         dbo.MealCalc_DishCategory
GO
/****** Object:  Table [dbo].[MealCalc_MealTime]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_MealTime](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[OrderIndex] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_MealCalc_MealTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_WriteOffReason_View]    Script Date: 11/30/2009 00:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_WriteOffReason_View]
AS
SELECT
	dbo.MealCalc_WriteOffReason.ID,
	dbo.MealCalc_WriteOffReason.Name
FROM MealCalc_WriteOffReason
GO
/****** Object:  View [dbo].[MealCalc_Supplier_View]    Script Date: 11/30/2009 00:43:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Supplier_View]
AS
SELECT     ID, Name, Comment, Active
FROM         dbo.MealCalc_Supplier
GO
/****** Object:  View [dbo].[MealCalc_Supplier_PickListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Supplier_PickListView]
AS
SELECT     ID, Name, Comment
FROM         dbo.MealCalc_Supplier
WHERE     (Active = 1)
GO
/****** Object:  View [dbo].[MealCalc_Organization_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Organization_View]
AS
SELECT
	ID,
	Name,
	Active
FROM dbo.MealCalc_Organization
GO
/****** Object:  View [dbo].[MealCalc_Organization_PickListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Organization_PickListView]
AS
SELECT
	ID,
	Name
FROM dbo.MealCalc_Organization
WHERE Active = 1
GO
/****** Object:  View [dbo].[MealCalc_Organization_ListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Organization_ListView]
AS
SELECT
	ID,
	Name,
	Active
FROM dbo.MealCalc_Organization
GO
/****** Object:  Table [dbo].[MealCalc_MenuTemplateMealTime]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_MenuTemplateMealTime](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuTemplateID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrderIndex] [int] NOT NULL,
 CONSTRAINT [PK_MealCalc_MenuTemplateMealTime] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_MenuTemplate_ListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MenuTemplate_ListView]
AS
SELECT
	dbo.MealCalc_MenuTemplate.ID,
	dbo.MealCalc_MenuTemplate.Name,
	dbo.MealCalc_MenuTemplate.Description
FROM dbo.MealCalc_MenuTemplate
GO
/****** Object:  View [dbo].[MealCalc_CalcDay_View]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDay_View]
AS
SELECT 
	ID,
	CalcDate,
	Comment
FROM dbo.MealCalc_CalcDay
GO
/****** Object:  View [dbo].[MealCalc_CalcDay_PickListView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDay_PickListView]
AS
SELECT
	ID,
	CalcDate,
	Comment
FROM dbo.MealCalc_CalcDay
GO
/****** Object:  View [dbo].[MealCalc_Menu_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Menu_View]
AS
SELECT
	dbo.MealCalc_Menu.ID,
	dbo.MealCalc_Menu.Name,
	dbo.MealCalc_Menu.Description,
	dbo.MealCalc_Menu.Active
FROM dbo.MealCalc_Menu
GO
/****** Object:  View [dbo].[MealCalc_Menu_PickListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Menu_PickListView]
AS
SELECT
	dbo.MealCalc_Menu.ID,
	dbo.MealCalc_Menu.Name
FROM dbo.MealCalc_Menu
WHERE dbo.MealCalc_Menu.Active = 1
GO
/****** Object:  View [dbo].[MealCalc_Menu_ActiveListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Menu_ActiveListView]
AS
SELECT
	dbo.MealCalc_Menu.ID,
	dbo.MealCalc_Menu.Name,
	dbo.MealCalc_Menu.Description
FROM dbo.MealCalc_Menu
WHERE dbo.MealCalc_Menu.Active = 1
GO

/****** Object:  View [dbo].[MealCalc_MealTime_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MealTime_View]
AS
SELECT
	ID,
	MenuID,
	Name,
	OrderIndex,
	Active
FROM dbo.MealCalc_MealTime
GO
/****** Object:  View [dbo].[MealCalc_MealTime_PickListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MealTime_PickListView]
AS
SELECT
	ID,
	MenuID,
	Name + '(' + CAST(OrderIndex AS NVARCHAR(3)) + ')' AS Name
FROM dbo.MealCalc_MealTime
WHERE Active = 1
GO
/****** Object:  View [dbo].[MealCalc_MealTime_ActiveListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MealTime_ActiveListView]
AS
SELECT
	ID,
	MenuID,
	Name,
	OrderIndex
FROM dbo.MealCalc_MealTime
WHERE Active = 1
GO
/****** Object:  Table [dbo].[MealCalc_MenuTemplateMealTimeDish]    Script Date: 11/30/2009 00:43:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_MenuTemplateMealTimeDish](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MenuTemplateMealTimeID] [int] NOT NULL,
	[DishID] [int] NOT NULL,
 CONSTRAINT [PK_MealCalc_MenuTemplateMealTimeDish] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_MenuTemplate_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MenuTemplate_View]
AS
SELECT
	dbo.MealCalc_MenuTemplate.ID,
	dbo.MealCalc_MenuTemplate.Name,
	dbo.MealCalc_MenuTemplate.Description,
	(
		SELECT ISNULL(SUM(dbo.MealCalc_Good.Fat), 0) FROM dbo.MealCalc_MenuTemplateMealTime
		JOIN dbo.MealCalc_MenuTemplateMealTimeDish ON dbo.MealCalc_MenuTemplateMealTime.ID = dbo.MealCalc_MenuTemplateMealTimeDish.MenuTemplateMealTimeID
		JOIN dbo.MealCalc_Dish ON dbo.MealCalc_MenuTemplateMealTimeDish.DishID = dbo.MealCalc_Dish.ID
		JOIN dbo.MealCalc_DishGood ON dbo.MealCalc_Dish.ID = dbo.MealCalc_DishGood.DishID
		JOIN dbo.MealCalc_Good ON dbo.MealCalc_DishGood.GoodID = dbo.MealCalc_Good.ID
		WHERE dbo.MealCalc_MenuTemplateMealTime.MenuTemplateID = dbo.MealCalc_MenuTemplate.ID
	) TotalFat,
	(
		SELECT ISNULL(SUM(dbo.MealCalc_Good.Proteins), 0) FROM dbo.MealCalc_MenuTemplateMealTime
		JOIN dbo.MealCalc_MenuTemplateMealTimeDish ON dbo.MealCalc_MenuTemplateMealTime.ID = dbo.MealCalc_MenuTemplateMealTimeDish.MenuTemplateMealTimeID
		JOIN dbo.MealCalc_Dish ON dbo.MealCalc_MenuTemplateMealTimeDish.DishID = dbo.MealCalc_Dish.ID
		JOIN dbo.MealCalc_DishGood ON dbo.MealCalc_Dish.ID = dbo.MealCalc_DishGood.DishID
		JOIN dbo.MealCalc_Good ON dbo.MealCalc_DishGood.GoodID = dbo.MealCalc_Good.ID
		WHERE dbo.MealCalc_MenuTemplateMealTime.MenuTemplateID = dbo.MealCalc_MenuTemplate.ID
	) TotalProteins,
	(
		SELECT ISNULL(SUM(dbo.MealCalc_Good.Carbohydrates), 0) FROM dbo.MealCalc_MenuTemplateMealTime
		JOIN dbo.MealCalc_MenuTemplateMealTimeDish ON dbo.MealCalc_MenuTemplateMealTime.ID = dbo.MealCalc_MenuTemplateMealTimeDish.MenuTemplateMealTimeID
		JOIN dbo.MealCalc_Dish ON dbo.MealCalc_MenuTemplateMealTimeDish.DishID = dbo.MealCalc_Dish.ID
		JOIN dbo.MealCalc_DishGood ON dbo.MealCalc_Dish.ID = dbo.MealCalc_DishGood.DishID
		JOIN dbo.MealCalc_Good ON dbo.MealCalc_DishGood.GoodID = dbo.MealCalc_Good.ID
		WHERE dbo.MealCalc_MenuTemplateMealTime.MenuTemplateID = dbo.MealCalc_MenuTemplate.ID
	) TotalCarbohydrates,
	(
		SELECT ISNULL(SUM(dbo.MealCalc_Good.FoodValue), 0) FROM dbo.MealCalc_MenuTemplateMealTime
		JOIN dbo.MealCalc_MenuTemplateMealTimeDish ON dbo.MealCalc_MenuTemplateMealTime.ID = dbo.MealCalc_MenuTemplateMealTimeDish.MenuTemplateMealTimeID
		JOIN dbo.MealCalc_Dish ON dbo.MealCalc_MenuTemplateMealTimeDish.DishID = dbo.MealCalc_Dish.ID
		JOIN dbo.MealCalc_DishGood ON dbo.MealCalc_Dish.ID = dbo.MealCalc_DishGood.DishID
		JOIN dbo.MealCalc_Good ON dbo.MealCalc_DishGood.GoodID = dbo.MealCalc_Good.ID
		WHERE dbo.MealCalc_MenuTemplateMealTime.MenuTemplateID = dbo.MealCalc_MenuTemplate.ID
	) TotalFoodValue
FROM dbo.MealCalc_MenuTemplate
GO
/****** Object:  View [dbo].[MealCalc_MenuTemplateMealTime_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MenuTemplateMealTime_View]
AS
SELECT
	ID,
	MenuTemplateID,
	Name,
	OrderIndex
FROM dbo.MealCalc_MenuTemplateMealTime
GO
/****** Object:  View [dbo].[MealCalc_MenuTemplateMealTime_PickListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MenuTemplateMealTime_PickListView]
AS
SELECT
	ID,
	MenuTemplateID,
	Name + '(' + CAST(OrderIndex AS NVARCHAR(3)) + ')' AS Name
FROM dbo.MealCalc_MenuTemplateMealTime
GO
/****** Object:  View [dbo].[MealCalc_Dish_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Dish_View]
AS
SELECT     ID, Name, DishCategoryID, ReceiptCode, PortionCount, WorkOut, PortionWeight
FROM         dbo.MealCalc_Dish
GO
/****** Object:  View [dbo].[MealCalc_Dish_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_Dish_ListView]
AS
SELECT
	ID,
	DishCategoryID,
	Name + ' [' + ReceiptCode + ']' AS [Name]
FROM dbo.MealCalc_Dish
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenu_View]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenu_View]
AS
SELECT
	dbo.MealCalc_CalcDayMenu.ID,
	dbo.MealCalc_CalcDayMenu.CalcDayID,
	dbo.MealCalc_CalcDayMenu.MenuID,
	dbo.MealCalc_CalcDayMenu.Comment,
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID,
	dbo.MealCalc_CalcDayMenu.Deleted,
	dbo.MealCalc_CalcDayMenuStatus.Name [CalcDayMenuStatusName]
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN dbo.MealCalc_CalcDayMenuStatus ON dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = dbo.MealCalc_CalcDayMenuStatus.ID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenu_ModifiedListView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenu_ModifiedListView]
AS
SELECT
	dbo.MealCalc_CalcDayMenu.ID,
	dbo.MealCalc_CalcDayMenu.CalcDayID,
	dbo.MealCalc_CalcDayMenu.MenuID,
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID,
	dbo.MealCalc_CalcDayMenu.Deleted,
	dbo.MealCalc_Menu.Name [MenuName]
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN MealCalc_Menu ON MealCalc_CalcDayMenu.MenuID = MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDayMenuStatus ON dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = dbo.MealCalc_CalcDayMenuStatus.ID
WHERE dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 1
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenu_ListWriteOffCalculatedView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenu_ListWriteOffCalculatedView]
AS
SELECT
	dbo.MealCalc_CalcDayMenu.ID,
	dbo.MealCalc_CalcDayMenu.CalcDayID,
	dbo.MealCalc_CalcDayMenu.MenuID,
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID,
	dbo.MealCalc_CalcDayMenu.Deleted,
	dbo.MealCalc_Menu.Name [MenuName]
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN MealCalc_Menu ON MealCalc_CalcDayMenu.MenuID = MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDayMenuStatus ON dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = dbo.MealCalc_CalcDayMenuStatus.ID
WHERE dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 3
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenu_ListView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenu_ListView]
AS
SELECT
	dbo.MealCalc_CalcDayMenu.ID,
	dbo.MealCalc_CalcDayMenu.CalcDayID,
	dbo.MealCalc_CalcDayMenu.MenuID,
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID,
	dbo.MealCalc_CalcDayMenu.Deleted,
	dbo.MealCalc_Menu.Name [MenuName]
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN MealCalc_Menu ON MealCalc_CalcDayMenu.MenuID = MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDayMenuStatus ON dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = dbo.MealCalc_CalcDayMenuStatus.ID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenu_CompletedListView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenu_CompletedListView]
AS
SELECT
	dbo.MealCalc_CalcDayMenu.ID,
	dbo.MealCalc_CalcDayMenu.CalcDayID,
	dbo.MealCalc_CalcDayMenu.MenuID,
	dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID,
	dbo.MealCalc_CalcDayMenu.Deleted,
	dbo.MealCalc_Menu.Name [MenuName]
FROM dbo.MealCalc_CalcDayMenu
	INNER JOIN MealCalc_Menu ON MealCalc_CalcDayMenu.MenuID = MealCalc_Menu.ID
	INNER JOIN dbo.MealCalc_CalcDayMenuStatus ON dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = dbo.MealCalc_CalcDayMenuStatus.ID
WHERE dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 2 OR dbo.MealCalc_CalcDayMenu.CalcDayMenuStatusID = 3
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayMeal]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayMeal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDayMenuID] [int] NOT NULL,
	[MealTimeID] [int] NOT NULL,
	[Comment] [nvarchar](2000) NULL,
 CONSTRAINT [PK_MealCalc_CalcDayMeal] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MealCalc_CalcDayMealDish]    Script Date: 11/30/2009 00:43:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealCalc_CalcDayMealDish](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CalcDayMealID] [int] NOT NULL,
	[DishID] [int] NOT NULL,
	[PortionCount] [int] NOT NULL,
	[Comment] [nvarchar](200) NULL,
 CONSTRAINT [PK_MealCalc_CalcDayMealDish] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_View]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_View]
AS
SELECT
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.ID,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID,
	dbo.MealCalc_CalcDayMeal.CalcDayMenuID AS CalcDayMenuID,
	dbo.MealCalc_MealTime.[Name] AS MealTimeName,
	dbo.MealCalc_Dish.[Name] AS DishName,
	dbo.MealCalc_Good.[Name] AS GoodName,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity AS Quantity,
	dbo.MealCalc_GoodIncome.GoodCommodityName AS GoodCommodityName,
	dbo.MealCalc_GoodIncome.Price,
	dbo.MealCalc_Supplier.Name AS SupplierName
FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
	INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID = dbo.MealCalc_GoodIncomeDocumentDetail.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
	INNER JOIN dbo.MealCalc_Supplier ON dbo.MealCalc_GoodIncomeDocument.SupplierID = dbo.MealCalc_Supplier.ID
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID
	INNER JOIN dbo.MealCalc_Good ON dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_Good.ID
	INNER JOIN dbo.MealCalc_CalcDayMealDish ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = dbo.MealCalc_CalcDayMealDish.ID
	INNER JOIN dbo.MealCalc_CalcDayMeal ON dbo.MealCalc_CalcDayMealDish.CalcDayMealID = dbo.MealCalc_CalcDayMeal.ID
	INNER JOIN dbo.MealCalc_MealTime ON dbo.MealCalc_CalcDayMeal.MealTimeID = dbo.MealCalc_MealTime.ID
	INNER JOIN dbo.MealCalc_Dish ON dbo.MealCalc_CalcDayMealDish.DishID = dbo.MealCalc_Dish.ID
--WHERE dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID = 33
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff_ListView]
AS
SELECT
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.ID AS ID,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID AS CalcDayMenuGoodSpendID,
	dbo.MealCalc_CalcDayMeal.CalcDayMenuID AS CalcDayMenuID,
	dbo.MealCalc_GoodIncome.GoodCommodityName AS GoodCommodityName,
	dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.Quantity AS Quantity,
	dbo.MealCalc_GoodIncome.Price,
	dbo.MealCalc_CalcDay.CalcDate AS SupplyDate,
	dbo.MealCalc_Supplier.Name AS SupplierName
FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff
	INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocumentDetail ON dbo.MealCalc_GoodIncome.GoodIncomeDocumentDetailID = dbo.MealCalc_GoodIncomeDocumentDetail.ID
	INNER JOIN dbo.MealCalc_GoodIncomeDocument ON dbo.MealCalc_GoodIncomeDocumentDetail.DocumentVersionID = dbo.MealCalc_GoodIncomeDocument.ID
	INNER JOIN dbo.MealCalc_CalcDay ON dbo.MealCalc_GoodIncomeDocument.IncomeCalcDayID = dbo.MealCalc_CalcDay.ID
	INNER JOIN dbo.MealCalc_Supplier ON dbo.MealCalc_GoodIncomeDocument.SupplierID = dbo.MealCalc_Supplier.ID
	INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID
	INNER JOIN dbo.MealCalc_Good ON dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_Good.ID
	INNER JOIN dbo.MealCalc_CalcDayMealDish ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = dbo.MealCalc_CalcDayMealDish.ID
	INNER JOIN dbo.MealCalc_CalcDayMeal ON dbo.MealCalc_CalcDayMealDish.CalcDayMealID = dbo.MealCalc_CalcDayMeal.ID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenuGoodSpend_ListView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenuGoodSpend_ListView]
AS
SELECT
	dbo.MealCalc_CalcDayMenuGoodSpend.ID,
	dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID,
	dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID,
	dbo.MealCalc_CalcDayMenuGoodSpend.GoodID,
	dbo.MealCalc_MealTime.Name AS MealTimeName,
	dbo.MealCalc_Dish.Name AS DishName,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_CalcDayMenuGoodSpend.QuantityPlan,
	dbo.MealCalc_CalcDayMenuGoodSpend.QuantityFact,
	ISNULL((SELECT SUM(Quantity) FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID), 0) AS QuantityReserved
FROM MealCalc_CalcDayMenuGoodSpend
	INNER JOIN MealCalc_Good ON dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_Good.ID
	LEFT JOIN dbo.MealCalc_CalcDayMealDish ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = dbo.MealCalc_CalcDayMealDish.ID
	LEFT JOIN dbo.MealCalc_CalcDayMeal ON dbo.MealCalc_CalcDayMealDish.CalcDayMealID = dbo.MealCalc_CalcDayMeal.ID
	LEFT JOIN dbo.MealCalc_MealTime ON dbo.MealCalc_CalcDayMeal.MealTimeID = dbo.MealCalc_MealTime.ID
	LEFT JOIN dbo.MealCalc_Dish ON dbo.MealCalc_CalcDayMealDish.DishID = dbo.MealCalc_Dish.ID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMenuGoodSpend_ListByCalcDayMealView]    Script Date: 11/30/2009 00:43:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMenuGoodSpend_ListByCalcDayMealView]
AS
SELECT
	dbo.MealCalc_CalcDayMenuGoodSpend.ID,
	dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMenuID,
	dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID,
	dbo.MealCalc_CalcDayMenuGoodSpend.GoodID,
	dbo.MealCalc_Good.Name AS GoodName,
	dbo.MealCalc_CalcDayMenuGoodSpend.QuantityPlan,
	dbo.MealCalc_CalcDayMenuGoodSpend.QuantityFact,
	ISNULL((SELECT SUM(Quantity) FROM dbo.MealCalc_CalcDayMenuGoodSpendWriteOff WHERE dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID = dbo.MealCalc_CalcDayMenuGoodSpend.ID), 0) AS QuantityReserved
FROM MealCalc_CalcDayMenuGoodSpend
	INNER JOIN MealCalc_Good ON dbo.MealCalc_CalcDayMenuGoodSpend.GoodID = dbo.MealCalc_Good.ID
	LEFT JOIN dbo.MealCalc_CalcDayMealDish ON dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID = dbo.MealCalc_CalcDayMealDish.ID
	LEFT JOIN dbo.MealCalc_CalcDayMeal ON dbo.MealCalc_CalcDayMealDish.CalcDayMealID = dbo.MealCalc_CalcDayMeal.ID
	LEFT JOIN dbo.MealCalc_MealTime ON dbo.MealCalc_CalcDayMeal.MealTimeID = dbo.MealCalc_MealTime.ID
	LEFT JOIN dbo.MealCalc_Dish ON dbo.MealCalc_CalcDayMealDish.DishID = dbo.MealCalc_Dish.ID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMealDish_ListView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMealDish_ListView]
AS
SELECT
	dbo.MealCalc_CalcDayMealDish.ID,
	dbo.MealCalc_CalcDayMealDish.CalcDayMealID,
	dbo.MealCalc_CalcDayMealDish.DishID,
	dbo.MealCalc_DishCategory.Name AS DishCategoty_Name,
	dbo.MealCalc_Dish.Name AS Dish_Name,
	dbo.MealCalc_CalcDayMealDish.PortionCount AS Dish_PortionCount,
	dbo.MealCalc_Dish.WorkOut AS Dish_WorkOut,
	dbo.MealCalc_Dish.PortionWeight,
	CAST
	(
		(
			SELECT
				SUM(dbo.MealCalc_GoodIncome.Price)
			FROM dbo.MealCalc_CalcDayMealDish CDMD
				INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON CDMD.ID = dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID
				INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
				INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
			WHERE CDMD.ID = MealCalc_CalcDayMealDish.ID
		) / dbo.MealCalc_CalcDayMealDish.PortionCount
		AS DECIMAL(18, 2)
	) AS PortionPrice
FROM dbo.MealCalc_DishCategory
	RIGHT OUTER JOIN dbo.MealCalc_Dish ON dbo.MealCalc_DishCategory.ID = dbo.MealCalc_Dish.DishCategoryID
	RIGHT OUTER JOIN dbo.MealCalc_CalcDayMealDish ON dbo.MealCalc_Dish.ID = dbo.MealCalc_CalcDayMealDish.DishID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMealDish_ListByCalcDayMenuView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMealDish_ListByCalcDayMenuView]
AS
SELECT
	dbo.MealCalc_CalcDayMealDish.ID,
	dbo.MealCalc_CalcDayMeal.CalcDayMenuID AS CalcDayMenuID,
	dbo.MealCalc_CalcDayMealDish.CalcDayMealID,
	dbo.MealCalc_CalcDayMealDish.DishID,
	dbo.MealCalc_DishCategory.Name AS DishCategoty_Name,
	dbo.MealCalc_Dish.Name AS Dish_Name,
	dbo.MealCalc_CalcDayMealDish.PortionCount AS Dish_PortionCount,
	dbo.MealCalc_Dish.WorkOut AS Dish_WorkOut,
	dbo.MealCalc_Dish.PortionWeight,
	CAST
	(
		(
			SELECT
				SUM(dbo.MealCalc_GoodIncome.Price)
			FROM dbo.MealCalc_CalcDayMealDish CDMD
				INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpend ON CDMD.ID = dbo.MealCalc_CalcDayMenuGoodSpend.CalcDayMealDishID
				INNER JOIN dbo.MealCalc_CalcDayMenuGoodSpendWriteOff ON dbo.MealCalc_CalcDayMenuGoodSpend.ID = dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.CalcDayMenuGoodSpendID
				INNER JOIN dbo.MealCalc_GoodIncome ON dbo.MealCalc_CalcDayMenuGoodSpendWriteOff.GoodIncomeID = dbo.MealCalc_GoodIncome.ID
			WHERE CDMD.ID = MealCalc_CalcDayMealDish.ID
		) / dbo.MealCalc_CalcDayMealDish.PortionCount
		AS DECIMAL(18, 2)
	) AS PortionPrice
FROM dbo.MealCalc_CalcDayMealDish
	INNER JOIN dbo.MealCalc_CalcDayMeal ON dbo.MealCalc_CalcDayMealDish.CalcDayMealID = dbo.MealCalc_CalcDayMeal.ID
	INNER JOIN dbo.MealCalc_Dish ON dbo.MealCalc_Dish.ID = dbo.MealCalc_CalcDayMealDish.DishID
	INNER JOIN dbo.MealCalc_DishCategory ON dbo.MealCalc_DishCategory.ID = dbo.MealCalc_Dish.DishCategoryID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMeal_View]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMeal_View]
AS
SELECT
	ID,
	CalcDayMenuID,
	MealTimeID,
	Comment
FROM dbo.MealCalc_CalcDayMeal
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMeal_ListView]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMeal_ListView]
AS
SELECT
	dbo.MealCalc_CalcDayMeal.ID,
	dbo.MealCalc_CalcDayMeal.CalcDayMenuID,
	dbo.MealCalc_CalcDayMeal.MealTimeID,
	dbo.MealCalc_MealTime.OrderIndex,
	dbo.MealCalc_MealTime.Name
FROM dbo.MealCalc_CalcDayMeal
	LEFT JOIN dbo.MealCalc_MealTime ON dbo.MealCalc_CalcDayMeal.MealTimeID = dbo.MealCalc_MealTime.ID
GO
/****** Object:  View [dbo].[MealCalc_MenuTemplateMealTimeDish_View]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MenuTemplateMealTimeDish_View]
AS
SELECT
	ID,
	MenuTemplateMealTimeID,
	DishID
FROM dbo.MealCalc_MenuTemplateMealTimeDish
GO
/****** Object:  View [dbo].[MealCalc_MenuTemplateMealTimeDish_ListView]    Script Date: 11/30/2009 00:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_MenuTemplateMealTimeDish_ListView]
AS
SELECT
	dbo.MealCalc_MenuTemplateMealTimeDish.ID,
	dbo.MealCalc_MenuTemplateMealTimeDish.MenuTemplateMealTimeID,
	dbo.MealCalc_MenuTemplateMealTimeDish.DishID,
	dbo.MealCalc_DishCategory.Name AS DishCategoty_Name,
	dbo.MealCalc_Dish.Name AS Dish_Name,
	dbo.MealCalc_Dish.WorkOut AS Dish_WorkOut,
	dbo.MealCalc_Dish.PortionWeight
FROM dbo.MealCalc_MenuTemplateMealTimeDish
	JOIN dbo.MealCalc_Dish ON dbo.MealCalc_Dish.ID = dbo.MealCalc_MenuTemplateMealTimeDish.DishID
	JOIN dbo.MealCalc_DishCategory ON dbo.MealCalc_DishCategory.ID = dbo.MealCalc_Dish.DishCategoryID
GO
/****** Object:  View [dbo].[MealCalc_CalcDayMealDish_View]    Script Date: 11/30/2009 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MealCalc_CalcDayMealDish_View]
AS
SELECT
	ID,
	CalcDayMealID,
	DishID,
	PortionCount,
	Comment
FROM dbo.MealCalc_CalcDayMealDish
GO
/****** Object:  Default [DF_MealCalc_AccountingPeriod_Closed]    Script Date: 11/30/2009 00:43:28 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriod] ADD  CONSTRAINT [DF_MealCalc_AccountingPeriod_Closed]  DEFAULT ((0)) FOR [Closed]
GO
/****** Object:  Default [DF_MealCalc_GoodBalance_Price]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodBalance] ADD  CONSTRAINT [DF_MealCalc_GoodBalance_Price]  DEFAULT ((0)) FOR [Price]
GO
/****** Object:  Default [DF_MealCalc_CalcDayMenu_Deleted]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenu] ADD  CONSTRAINT [DF_MealCalc_CalcDayMenu_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
/****** Object:  Default [DF_MealCalc_DishCategory_IndexNumber]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_DishCategory] ADD  CONSTRAINT [DF_MealCalc_DishCategory_IndexNumber]  DEFAULT ((0)) FOR [IndexNumber]
GO
/****** Object:  Default [DF_MealCalc_GoodIncome_QuantityWrittenOff]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncome] ADD  CONSTRAINT [DF_MealCalc_GoodIncome_QuantityWrittenOff]  DEFAULT ((0)) FOR [QuantityWrittenOff]
GO
/****** Object:  Default [DF_MealCalc_GoodIncome_QuantityReserved]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncome] ADD  CONSTRAINT [DF_MealCalc_GoodIncome_QuantityReserved]  DEFAULT ((0)) FOR [QuantityReserved]
GO
/****** Object:  Default [DF_MealCalc_GoodIncome_Deleted]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncome] ADD  CONSTRAINT [DF_MealCalc_GoodIncome_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
/****** Object:  Default [DF_MealCalc_Menu_Active]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_Menu] ADD  CONSTRAINT [DF_MealCalc_Menu_Active]  DEFAULT ((1)) FOR [Active]
GO
/****** Object:  Default [DF_MealCalc_Organization_Active]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_Organization] ADD  CONSTRAINT [DF_MealCalc_Organization_Active]  DEFAULT ((1)) FOR [Active]
GO
/****** Object:  Default [DF_MealCalc_Supplier_Active]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_Supplier] ADD  CONSTRAINT [DF_MealCalc_Supplier_Active]  DEFAULT ((1)) FOR [Active]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodBalance_MealCalc_AccountingPeriod]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodBalance]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodBalance_MealCalc_AccountingPeriod] FOREIGN KEY([AccountingPeriodID])
REFERENCES [dbo].[MealCalc_AccountingPeriod] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodBalance] CHECK CONSTRAINT [FK_MealCalc_GoodBalance_MealCalc_AccountingPeriod]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodBalance_MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodBalance]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodBalance_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodBalance] CHECK CONSTRAINT [FK_MealCalc_GoodBalance_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_AccountingPeriodGoodReport_MealCalc_AccountingPeriod]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReport]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReport_MealCalc_AccountingPeriod] FOREIGN KEY([AccountingPeriodID])
REFERENCES [dbo].[MealCalc_AccountingPeriod] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReport] CHECK CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReport_MealCalc_AccountingPeriod]
GO
/****** Object:  ForeignKey [FK_MealCalc_AccountingPeriodGoodReport_MultiversionDocument_DocumentVersion]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReport]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReport_MultiversionDocument_DocumentVersion] FOREIGN KEY([DocumentVersionID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersion] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReport] CHECK CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReport_MultiversionDocument_DocumentVersion]
GO
/****** Object:  ForeignKey [FK_MealCalc_AccountingPeriodGoodReportGood_MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReportGood]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReportGood_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReportGood] CHECK CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReportGood_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_AccountingPeriodGoodReportGood_MultiversionDocument_DocumentVersion]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReportGood]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReportGood_MultiversionDocument_DocumentVersion] FOREIGN KEY([DocumentVersionID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersion] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_AccountingPeriodGoodReportGood] CHECK CONSTRAINT [FK_MealCalc_AccountingPeriodGoodReportGood_MultiversionDocument_DocumentVersion]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayGoodBalanceChange_MealCalc_CalcDay]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayGoodBalanceChange]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayGoodBalanceChange_MealCalc_CalcDay] FOREIGN KEY([CalcDayID])
REFERENCES [dbo].[MealCalc_CalcDay] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayGoodBalanceChange] CHECK CONSTRAINT [FK_MealCalc_CalcDayGoodBalanceChange_MealCalc_CalcDay]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayGoodBalanceChange_MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayGoodBalanceChange]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayGoodBalanceChange_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayGoodBalanceChange] CHECK CONSTRAINT [FK_MealCalc_CalcDayGoodBalanceChange_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMeal_MealCalc_CalcDayMenu]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMeal]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMeal_MealCalc_CalcDayMenu] FOREIGN KEY([CalcDayMenuID])
REFERENCES [dbo].[MealCalc_CalcDayMenu] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMeal] CHECK CONSTRAINT [FK_MealCalc_CalcDayMeal_MealCalc_CalcDayMenu]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMeal_MealCalc_MealTime]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMeal]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMeal_MealCalc_MealTime] FOREIGN KEY([MealTimeID])
REFERENCES [dbo].[MealCalc_MealTime] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMeal] CHECK CONSTRAINT [FK_MealCalc_CalcDayMeal_MealCalc_MealTime]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMealDish_MealCalc_CalcDayMeal]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMealDish]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMealDish_MealCalc_CalcDayMeal] FOREIGN KEY([CalcDayMealID])
REFERENCES [dbo].[MealCalc_CalcDayMeal] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMealDish] CHECK CONSTRAINT [FK_MealCalc_CalcDayMealDish_MealCalc_CalcDayMeal]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMealDish_MealCalc_Dish]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMealDish]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMealDish_MealCalc_Dish] FOREIGN KEY([DishID])
REFERENCES [dbo].[MealCalc_Dish] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMealDish] CHECK CONSTRAINT [FK_MealCalc_CalcDayMealDish_MealCalc_Dish]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenu_MealCalc_CalcDay]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenu]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenu_MealCalc_CalcDay] FOREIGN KEY([CalcDayID])
REFERENCES [dbo].[MealCalc_CalcDay] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenu] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenu_MealCalc_CalcDay]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenu_MealCalc_CalcDayMenuStatus]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenu]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenu_MealCalc_CalcDayMenuStatus] FOREIGN KEY([CalcDayMenuStatusID])
REFERENCES [dbo].[MealCalc_CalcDayMenuStatus] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenu] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenu_MealCalc_CalcDayMenuStatus]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenu_MealCalc_Menu]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenu]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenu_MealCalc_Menu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[MealCalc_Menu] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenu] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenu_MealCalc_Menu]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_CalcDayMealDish]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_CalcDayMealDish] FOREIGN KEY([CalcDayMealDishID])
REFERENCES [dbo].[MealCalc_CalcDayMealDish] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_CalcDayMealDish]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_CalcDayMenu]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_CalcDayMenu] FOREIGN KEY([CalcDayMenuID])
REFERENCES [dbo].[MealCalc_CalcDayMenu] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_CalcDayMenu]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpend] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpend_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenuGoodSpendWriteOff_MealCalc_CalcDayMenuGoodSpend1]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpendWriteOff_MealCalc_CalcDayMenuGoodSpend1] FOREIGN KEY([CalcDayMenuGoodSpendID])
REFERENCES [dbo].[MealCalc_CalcDayMenuGoodSpend] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpendWriteOff_MealCalc_CalcDayMenuGoodSpend1]
GO
/****** Object:  ForeignKey [FK_MealCalc_CalcDayMenuGoodSpendWriteOff_MealCalc_GoodIncome]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpendWriteOff_MealCalc_GoodIncome] FOREIGN KEY([GoodIncomeID])
REFERENCES [dbo].[MealCalc_GoodIncome] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_CalcDayMenuGoodSpendWriteOff] CHECK CONSTRAINT [FK_MealCalc_CalcDayMenuGoodSpendWriteOff_MealCalc_GoodIncome]
GO
/****** Object:  ForeignKey [FK_MealCalc_Dish_MealCalc_DishCategory]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_Dish]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_Dish_MealCalc_DishCategory] FOREIGN KEY([DishCategoryID])
REFERENCES [dbo].[MealCalc_DishCategory] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_Dish] CHECK CONSTRAINT [FK_MealCalc_Dish_MealCalc_DishCategory]
GO
/****** Object:  ForeignKey [FK_MealCalc_DishGood_MealCalc_Dish]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_DishGood]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_DishGood_MealCalc_Dish] FOREIGN KEY([DishID])
REFERENCES [dbo].[MealCalc_Dish] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_DishGood] CHECK CONSTRAINT [FK_MealCalc_DishGood_MealCalc_Dish]
GO
/****** Object:  ForeignKey [FK_MealCalc_DishGood_MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_DishGood]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_DishGood_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_DishGood] CHECK CONSTRAINT [FK_MealCalc_DishGood_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_Good_MealCalc_GoodCategory]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_Good]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_Good_MealCalc_GoodCategory] FOREIGN KEY([GoodCategoryID])
REFERENCES [dbo].[MealCalc_GoodCategory] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_Good] CHECK CONSTRAINT [FK_MealCalc_Good_MealCalc_GoodCategory]
GO
/****** Object:  ForeignKey [FK_MealCalc_Good_MealCalc_GoodPackingUnit]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_Good]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_Good_MealCalc_GoodPackingUnit] FOREIGN KEY([DefaultGoodPackingUnitID])
REFERENCES [dbo].[MealCalc_GoodPackingUnit] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_Good] CHECK CONSTRAINT [FK_MealCalc_Good_MealCalc_GoodPackingUnit]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncome_MealCalc_CalcDay]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncome]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncome_MealCalc_CalcDay] FOREIGN KEY([CalcDayID])
REFERENCES [dbo].[MealCalc_CalcDay] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncome] CHECK CONSTRAINT [FK_MealCalc_GoodIncome_MealCalc_CalcDay]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncome_MealCalc_Good]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncome]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncome_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncome] CHECK CONSTRAINT [FK_MealCalc_GoodIncome_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncome_MealCalc_GoodIncomeDocumentDetail]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncome]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncome_MealCalc_GoodIncomeDocumentDetail] FOREIGN KEY([GoodIncomeDocumentDetailID])
REFERENCES [dbo].[MealCalc_GoodIncomeDocumentDetail] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncome] CHECK CONSTRAINT [FK_MealCalc_GoodIncome_MealCalc_GoodIncomeDocumentDetail]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncomeDocument_MealCalc_CalcDay]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncomeDocument_MealCalc_CalcDay] FOREIGN KEY([IncomeCalcDayID])
REFERENCES [dbo].[MealCalc_CalcDay] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocument] CHECK CONSTRAINT [FK_MealCalc_GoodIncomeDocument_MealCalc_CalcDay]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncomeDocument_MealCalc_Supplier]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncomeDocument_MealCalc_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[MealCalc_Supplier] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocument] CHECK CONSTRAINT [FK_MealCalc_GoodIncomeDocument_MealCalc_Supplier]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncomeDocument_MultiversionDocument_DocumentVersion]    Script Date: 11/30/2009 00:43:29 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncomeDocument_MultiversionDocument_DocumentVersion] FOREIGN KEY([ID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersion] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocument] CHECK CONSTRAINT [FK_MealCalc_GoodIncomeDocument_MultiversionDocument_DocumentVersion]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_Good]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_GoodIncomeDocument]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_GoodIncomeDocument] FOREIGN KEY([DocumentVersionID])
REFERENCES [dbo].[MealCalc_GoodIncomeDocument] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_GoodIncomeDocument]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_GoodPackingUnit]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_GoodPackingUnit] FOREIGN KEY([GoodPackingUnitID])
REFERENCES [dbo].[MealCalc_GoodPackingUnit] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodIncomeDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodIncomeDocumentDetail_MealCalc_GoodPackingUnit]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodPackingUnit_MealCalc_Good]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodPackingUnit]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodPackingUnit_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodPackingUnit] CHECK CONSTRAINT [FK_MealCalc_GoodPackingUnit_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodSpendDocument_MealCalc_CalcDayMenu]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodSpendDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodSpendDocument_MealCalc_CalcDayMenu] FOREIGN KEY([CalcDayMenuID])
REFERENCES [dbo].[MealCalc_CalcDayMenu] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodSpendDocument] CHECK CONSTRAINT [FK_MealCalc_GoodSpendDocument_MealCalc_CalcDayMenu]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodSpendDocument_MultiversionDocument_DocumentVersion]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodSpendDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodSpendDocument_MultiversionDocument_DocumentVersion] FOREIGN KEY([ID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersion] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodSpendDocument] CHECK CONSTRAINT [FK_MealCalc_GoodSpendDocument_MultiversionDocument_DocumentVersion]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodSpendDocumentDetail_MealCalc_Good]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodSpendDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodSpendDocumentDetail_MealCalc_Good] FOREIGN KEY([GoodID])
REFERENCES [dbo].[MealCalc_Good] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodSpendDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodSpendDocumentDetail_MealCalc_Good]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodSpendDocumentDetail_MealCalc_GoodSpendDocument]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodSpendDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodSpendDocumentDetail_MealCalc_GoodSpendDocument] FOREIGN KEY([DocumentVersionID])
REFERENCES [dbo].[MealCalc_GoodSpendDocument] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodSpendDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodSpendDocumentDetail_MealCalc_GoodSpendDocument]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodWriteOffDocument_MealCalc_CalcDay]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodWriteOffDocument_MealCalc_CalcDay] FOREIGN KEY([CalcDayID])
REFERENCES [dbo].[MealCalc_CalcDay] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocument] CHECK CONSTRAINT [FK_MealCalc_GoodWriteOffDocument_MealCalc_CalcDay]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodWriteOffDocument_MealCalc_WriteOffReason]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodWriteOffDocument_MealCalc_WriteOffReason] FOREIGN KEY([WriteOffReasonID])
REFERENCES [dbo].[MealCalc_WriteOffReason] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocument] CHECK CONSTRAINT [FK_MealCalc_GoodWriteOffDocument_MealCalc_WriteOffReason]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodWriteOffDocument_MultiversionDocument_DocumentVersion]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocument]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodWriteOffDocument_MultiversionDocument_DocumentVersion] FOREIGN KEY([ID])
REFERENCES [dbo].[MultiversionDocument_DocumentVersion] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocument] CHECK CONSTRAINT [FK_MealCalc_GoodWriteOffDocument_MultiversionDocument_DocumentVersion]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodWriteOffDocumentDetail_MealCalc_GoodIncome]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodWriteOffDocumentDetail_MealCalc_GoodIncome] FOREIGN KEY([GoodIncomeID])
REFERENCES [dbo].[MealCalc_GoodIncome] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodWriteOffDocumentDetail_MealCalc_GoodIncome]
GO
/****** Object:  ForeignKey [FK_MealCalc_GoodWriteOffDocumentDetail_MealCalc_GoodWriteOffDocument]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocumentDetail]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_GoodWriteOffDocumentDetail_MealCalc_GoodWriteOffDocument] FOREIGN KEY([DocumentVersionID])
REFERENCES [dbo].[MealCalc_GoodWriteOffDocument] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_GoodWriteOffDocumentDetail] CHECK CONSTRAINT [FK_MealCalc_GoodWriteOffDocumentDetail_MealCalc_GoodWriteOffDocument]
GO
/****** Object:  ForeignKey [FK_MealCalc_MealTime_MealCalc_Menu]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_MealTime]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_MealTime_MealCalc_Menu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[MealCalc_Menu] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_MealTime] CHECK CONSTRAINT [FK_MealCalc_MealTime_MealCalc_Menu]
GO
/****** Object:  ForeignKey [FK_MealCalc_MenuTemplateMealTime_MealCalc_MenuTemplate]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_MenuTemplateMealTime]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_MenuTemplateMealTime_MealCalc_MenuTemplate] FOREIGN KEY([MenuTemplateID])
REFERENCES [dbo].[MealCalc_MenuTemplate] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_MenuTemplateMealTime] CHECK CONSTRAINT [FK_MealCalc_MenuTemplateMealTime_MealCalc_MenuTemplate]
GO
/****** Object:  ForeignKey [FK_MealCalc_MenuTemplateMealTimeDish_MealCalc_Dish]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_MenuTemplateMealTimeDish]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_MenuTemplateMealTimeDish_MealCalc_Dish] FOREIGN KEY([DishID])
REFERENCES [dbo].[MealCalc_Dish] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_MenuTemplateMealTimeDish] CHECK CONSTRAINT [FK_MealCalc_MenuTemplateMealTimeDish_MealCalc_Dish]
GO
/****** Object:  ForeignKey [FK_MealCalc_MenuTemplateMealTimeDish_MealCalc_MenuTemplateMealTime]    Script Date: 11/30/2009 00:43:30 ******/
ALTER TABLE [dbo].[MealCalc_MenuTemplateMealTimeDish]  WITH CHECK ADD  CONSTRAINT [FK_MealCalc_MenuTemplateMealTimeDish_MealCalc_MenuTemplateMealTime] FOREIGN KEY([MenuTemplateMealTimeID])
REFERENCES [dbo].[MealCalc_MenuTemplateMealTime] ([ID])
GO
ALTER TABLE [dbo].[MealCalc_MenuTemplateMealTimeDish] CHECK CONSTRAINT [FK_MealCalc_MenuTemplateMealTimeDish_MealCalc_MenuTemplateMealTime]
GO
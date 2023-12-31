USE [bookdb]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'serviceData'
GO
ALTER TABLE [dbo].[userSetValues] DROP CONSTRAINT [FK_userSetValues_setValues]
GO
ALTER TABLE [dbo].[users] DROP CONSTRAINT [FK_users_countriesCodes]
GO
ALTER TABLE [dbo].[systems] DROP CONSTRAINT [FK_systems_users1]
GO
ALTER TABLE [dbo].[systems] DROP CONSTRAINT [FK_systems_users]
GO
ALTER TABLE [dbo].[setValues] DROP CONSTRAINT [FK_setValues_setting]
GO
ALTER TABLE [dbo].[serviceDataFiles] DROP CONSTRAINT [FK_serviceDataFiles_serviceData]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_users1]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_users]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_systems]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_passengers]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_operations]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_officeSystem]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_office]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_flights]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_exchange]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_users2]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_users1]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_users]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_systems]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_serviceData]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_paySides]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_payOp]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_passengers]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_office]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_flights]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_exchange]
GO
ALTER TABLE [dbo].[passengerFiles] DROP CONSTRAINT [FK_passengerFiles_passengers]
GO
ALTER TABLE [dbo].[operations] DROP CONSTRAINT [FK_operations_statementsTable]
GO
ALTER TABLE [dbo].[operations] DROP CONSTRAINT [FK_operations_durationsTable]
GO
ALTER TABLE [dbo].[officeSystem] DROP CONSTRAINT [FK_officeSystem_systems]
GO
ALTER TABLE [dbo].[officeSystem] DROP CONSTRAINT [FK_officeSystem_office]
GO
ALTER TABLE [dbo].[officeFiles] DROP CONSTRAINT [FK_officeFiles_office]
GO
ALTER TABLE [dbo].[office] DROP CONSTRAINT [FK_office_users1]
GO
ALTER TABLE [dbo].[office] DROP CONSTRAINT [FK_office_users]
GO
ALTER TABLE [dbo].[flights] DROP CONSTRAINT [FK_flights_toTable]
GO
ALTER TABLE [dbo].[flights] DROP CONSTRAINT [FK_flights_fromTable]
GO
ALTER TABLE [dbo].[flights] DROP CONSTRAINT [FK_flights_flightTable]
GO
ALTER TABLE [dbo].[flights] DROP CONSTRAINT [FK_flights_airlines]
GO
ALTER TABLE [dbo].[error] DROP CONSTRAINT [FK_error_users]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_users1]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_users]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [FK_customers_countriesCodes]
GO
ALTER TABLE [dbo].[cities] DROP CONSTRAINT [FK_cities_countriesCodes]
GO
ALTER TABLE [dbo].[users] DROP CONSTRAINT [DF_users_balance]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [DF_customers_balanceType]
GO
ALTER TABLE [dbo].[customers] DROP CONSTRAINT [DF_customers_balance]
GO
ALTER TABLE [dbo].[countriesCodes] DROP CONSTRAINT [DF_countriesCodes_isDefault]
GO
/****** Object:  Table [dbo].[userSetValues]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[userSetValues]') AND type in (N'U'))
DROP TABLE [dbo].[userSetValues]
GO
/****** Object:  Table [dbo].[users]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
DROP TABLE [dbo].[users]
GO
/****** Object:  Table [dbo].[toTable]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[toTable]') AND type in (N'U'))
DROP TABLE [dbo].[toTable]
GO
/****** Object:  Table [dbo].[systems]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systems]') AND type in (N'U'))
DROP TABLE [dbo].[systems]
GO
/****** Object:  Table [dbo].[statementsTable]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[statementsTable]') AND type in (N'U'))
DROP TABLE [dbo].[statementsTable]
GO
/****** Object:  Table [dbo].[setValues]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setValues]') AND type in (N'U'))
DROP TABLE [dbo].[setValues]
GO
/****** Object:  Table [dbo].[setting]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setting]') AND type in (N'U'))
DROP TABLE [dbo].[setting]
GO
/****** Object:  Table [dbo].[serviceDataFiles]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceDataFiles]') AND type in (N'U'))
DROP TABLE [dbo].[serviceDataFiles]
GO
/****** Object:  Table [dbo].[serviceData]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceData]') AND type in (N'U'))
DROP TABLE [dbo].[serviceData]
GO
/****** Object:  Table [dbo].[paySides]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[paySides]') AND type in (N'U'))
DROP TABLE [dbo].[paySides]
GO
/****** Object:  Table [dbo].[payOp]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[payOp]') AND type in (N'U'))
DROP TABLE [dbo].[payOp]
GO
/****** Object:  Table [dbo].[passengers]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[passengers]') AND type in (N'U'))
DROP TABLE [dbo].[passengers]
GO
/****** Object:  Table [dbo].[passengerFiles]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[passengerFiles]') AND type in (N'U'))
DROP TABLE [dbo].[passengerFiles]
GO
/****** Object:  Table [dbo].[operations]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[operations]') AND type in (N'U'))
DROP TABLE [dbo].[operations]
GO
/****** Object:  Table [dbo].[officeSystem]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[officeSystem]') AND type in (N'U'))
DROP TABLE [dbo].[officeSystem]
GO
/****** Object:  Table [dbo].[officeFiles]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[officeFiles]') AND type in (N'U'))
DROP TABLE [dbo].[officeFiles]
GO
/****** Object:  Table [dbo].[office]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[office]') AND type in (N'U'))
DROP TABLE [dbo].[office]
GO
/****** Object:  Table [dbo].[fromTable]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fromTable]') AND type in (N'U'))
DROP TABLE [dbo].[fromTable]
GO
/****** Object:  Table [dbo].[flightTable]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[flightTable]') AND type in (N'U'))
DROP TABLE [dbo].[flightTable]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[flights]') AND type in (N'U'))
DROP TABLE [dbo].[flights]
GO
/****** Object:  Table [dbo].[exchange]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[exchange]') AND type in (N'U'))
DROP TABLE [dbo].[exchange]
GO
/****** Object:  Table [dbo].[error]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[error]') AND type in (N'U'))
DROP TABLE [dbo].[error]
GO
/****** Object:  Table [dbo].[durationsTable]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[durationsTable]') AND type in (N'U'))
DROP TABLE [dbo].[durationsTable]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[customers]') AND type in (N'U'))
DROP TABLE [dbo].[customers]
GO
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[countriesCodes]') AND type in (N'U'))
DROP TABLE [dbo].[countriesCodes]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cities]') AND type in (N'U'))
DROP TABLE [dbo].[cities]
GO
/****** Object:  Table [dbo].[airlines]    Script Date: 17/10/2023 10:48:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[airlines]') AND type in (N'U'))
DROP TABLE [dbo].[airlines]
GO
/****** Object:  Table [dbo].[airlines]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[airlines](
	[airlineId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[notes] [nvarchar](500) NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_airlines] PRIMARY KEY CLUSTERED 
(
	[airlineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[cityId] [int] IDENTITY(1,1) NOT NULL,
	[cityCode] [nvarchar](50) NULL,
	[countryId] [int] NULL,
 CONSTRAINT [PK_cities] PRIMARY KEY CLUSTERED 
(
	[cityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countriesCodes](
	[countryId] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[currency] [nvarchar](50) NULL,
	[name] [nvarchar](100) NULL,
	[isDefault] [tinyint] NOT NULL,
	[timeZoneName] [nvarchar](100) NULL,
	[timeZoneOffset] [nvarchar](100) NULL,
 CONSTRAINT [PK_countriesCodes] PRIMARY KEY CLUSTERED 
(
	[countryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[custId] [int] IDENTITY(1,1) NOT NULL,
	[custname] [nvarchar](500) NOT NULL,
	[lastName] [nvarchar](500) NULL,
	[company] [nvarchar](500) NULL,
	[email] [nvarchar](500) NULL,
	[phone] [nvarchar](500) NULL,
	[mobile] [nvarchar](500) NULL,
	[fax] [nvarchar](500) NULL,
	[address] [nvarchar](500) NULL,
	[custlevel] [nvarchar](500) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[custCode] [nvarchar](500) NULL,
	[image] [ntext] NULL,
	[notes] [nvarchar](500) NULL,
	[balance] [decimal](20, 3) NOT NULL,
	[balanceType] [tinyint] NOT NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[isActive] [int] NULL,
	[countryId] [int] NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[custId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[durationsTable]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[durationsTable](
	[durationId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_durationsTable] PRIMARY KEY CLUSTERED 
(
	[durationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[error]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[error](
	[errorId] [int] IDENTITY(1,1) NOT NULL,
	[num] [nvarchar](200) NULL,
	[msg] [ntext] NULL,
	[stackTrace] [ntext] NULL,
	[targetSite] [ntext] NULL,
	[createDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
 CONSTRAINT [PK_error] PRIMARY KEY CLUSTERED 
(
	[errorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[exchange]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[exchange](
	[exchangeId] [int] IDENTITY(1,1) NOT NULL,
	[syValue] [decimal](20, 3) NULL,
	[createDate] [datetime2](7) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
	[createUserId] [int] NULL,
 CONSTRAINT [PK_exchange] PRIMARY KEY CLUSTERED 
(
	[exchangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[flightId] [int] IDENTITY(1,1) NOT NULL,
	[airline] [nvarchar](500) NULL,
	[notes] [nvarchar](max) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[flightTableId] [int] NULL,
	[fromTableId] [int] NULL,
	[toTableId] [int] NULL,
	[isActive] [bit] NULL,
	[balance] [decimal](20, 3) NULL,
	[commission_ratio] [decimal](20, 3) NULL,
	[systemId] [int] NULL,
	[airlineId] [int] NULL,
	[type] [int] NULL,
	[code] [nvarchar](500) NULL,
 CONSTRAINT [PK_flights] PRIMARY KEY CLUSTERED 
(
	[flightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flightTable]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flightTable](
	[flightTableId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_flightTable] PRIMARY KEY CLUSTERED 
(
	[flightTableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fromTable]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fromTable](
	[fromTableId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_fromTable] PRIMARY KEY CLUSTERED 
(
	[fromTableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[office]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[office](
	[officeId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[agentName] [nvarchar](500) NULL,
	[joinDate] [datetime2](7) NULL,
	[mobile] [nvarchar](500) NULL,
	[address] [nvarchar](max) NULL,
	[userName] [nvarchar](500) NULL,
	[passwordSY] [nvarchar](500) NULL,
	[PasswordSoto] [nvarchar](500) NULL,
	[notes] [nvarchar](max) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[isActive] [bit] NULL,
	[balance] [decimal](20, 3) NULL,
	[commission_ratio] [decimal](20, 3) NULL,
	[code] [nvarchar](500) NULL,
 CONSTRAINT [PK_office] PRIMARY KEY CLUSTERED 
(
	[officeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[officeFiles]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[officeFiles](
	[fileId] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](500) NULL,
	[extention] [nvarchar](500) NULL,
	[folderName] [nvarchar](500) NULL,
	[notes] [nvarchar](500) NULL,
	[isActive] [bit] NULL,
	[officeId] [int] NULL,
 CONSTRAINT [PK_officeFiles] PRIMARY KEY CLUSTERED 
(
	[fileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[officeSystem]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[officeSystem](
	[osId] [int] IDENTITY(1,1) NOT NULL,
	[officeId] [int] NULL,
	[systemId] [int] NULL,
	[office_commission] [decimal](20, 3) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_officeSystem] PRIMARY KEY CLUSTERED 
(
	[osId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[operations]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operations](
	[operationId] [int] IDENTITY(1,1) NOT NULL,
	[operation] [nvarchar](500) NULL,
	[notes] [nvarchar](max) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[opStatementId] [int] NULL,
	[durationId] [int] NULL,
	[isActive] [bit] NULL,
 CONSTRAINT [PK_operations] PRIMARY KEY CLUSTERED 
(
	[operationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[passengerFiles]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[passengerFiles](
	[fileId] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](500) NULL,
	[extention] [nvarchar](500) NULL,
	[folderName] [nvarchar](500) NULL,
	[notes] [nvarchar](500) NULL,
	[isActive] [bit] NULL,
	[passengerId] [int] NULL,
 CONSTRAINT [PK_passengerFiles] PRIMARY KEY CLUSTERED 
(
	[fileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[passengers]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[passengers](
	[passengerId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[lastName] [nvarchar](500) NULL,
	[father] [nvarchar](500) NULL,
	[mother] [nvarchar](500) NULL,
	[notes] [nvarchar](max) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[isActive] [bit] NULL,
	[balance] [decimal](20, 3) NULL,
	[code] [nvarchar](500) NULL,
 CONSTRAINT [PK_passengers] PRIMARY KEY CLUSTERED 
(
	[passengerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payOp]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payOp](
	[payOpId] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](500) NULL,
	[cash] [decimal](20, 3) NULL,
	[opType] [nvarchar](500) NULL,
	[side] [nvarchar](500) NULL,
	[serviceId] [int] NULL,
	[opStatus] [nvarchar](500) NULL,
	[opDate] [datetime2](7) NULL,
	[notes] [nvarchar](max) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[officeId] [int] NULL,
	[passengerId] [int] NULL,
	[userId] [int] NULL,
	[recipient] [nvarchar](500) NULL,
	[recivedFrom] [nvarchar](500) NULL,
	[paysideId] [int] NULL,
	[flightId] [int] NULL,
	[opName] [nvarchar](500) NULL,
	[systemId] [int] NULL,
	[systemType] [nvarchar](50) NULL,
	[currency] [nvarchar](50) NULL,
	[syValue] [decimal](20, 3) NULL,
	[exchangeId] [int] NULL,
	[fromSide] [nvarchar](50) NULL,
	[processType] [nvarchar](50) NULL,
	[sourceId] [int] NULL,
	[paid] [decimal](20, 3) NULL,
	[isPaid] [bit] NULL,
	[deserved] [decimal](20, 3) NULL,
	[purpose] [nvarchar](500) NULL,
	[paidCurrency] [nvarchar](50) NULL,
	[syCash] [decimal](20, 3) NULL,
 CONSTRAINT [PK_payOptb] PRIMARY KEY CLUSTERED 
(
	[payOpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paySides]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paySides](
	[paysideId] [int] IDENTITY(1,1) NOT NULL,
	[side] [nvarchar](100) NULL,
	[sideAr] [nvarchar](100) NULL,
	[notes] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[code] [nvarchar](100) NULL,
	[balance] [decimal](20, 3) NULL,
 CONSTRAINT [PK_paySide] PRIMARY KEY CLUSTERED 
(
	[paysideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[serviceData]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[serviceData](
	[serviceId] [int] IDENTITY(1,1) NOT NULL,
	[serviceNum] [nvarchar](500) NULL,
	[type] [nvarchar](500) NULL,
	[passenger] [nvarchar](500) NULL,
	[ticketNum] [nvarchar](500) NULL,
	[airline] [nvarchar](500) NULL,
	[officeId] [int] NULL,
	[serviceDate] [datetime2](7) NULL,
	[pnr] [nvarchar](500) NULL,
	[ticketvalueSP] [decimal](20, 3) NULL,
	[ticketvalueDollar] [decimal](20, 3) NULL,
	[total] [decimal](20, 3) NULL,
	[priceBeforTax] [decimal](20, 3) NULL,
	[commitionRatio] [decimal](20, 3) NULL,
	[commitionValue] [decimal](20, 3) NULL,
	[cost] [decimal](20, 3) NULL,
	[saleValue] [decimal](20, 3) NULL,
	[paid] [decimal](20, 3) NULL,
	[profit] [decimal](20, 3) NULL,
	[notes] [nvarchar](max) NULL,
	[state] [nvarchar](500) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[passengerId] [int] NULL,
	[flightId] [int] NULL,
	[operationId] [int] NULL,
	[systemType] [nvarchar](50) NULL,
	[isActive] [bit] NULL,
	[system_commission_value] [decimal](20, 3) NULL,
	[system_commission_ratio] [decimal](20, 3) NULL,
	[office_commission_value] [decimal](20, 3) NULL,
	[office_commission_ratio] [decimal](20, 3) NULL,
	[company_commission_value] [decimal](20, 3) NULL,
	[company_commission_ratio] [decimal](20, 3) NULL,
	[totalnet] [decimal](20, 3) NULL,
	[passengerPaid] [decimal](20, 3) NULL,
	[passengerUnpaid] [decimal](20, 3) NULL,
	[officePaid] [decimal](20, 3) NULL,
	[officeUnpaid] [decimal](20, 3) NULL,
	[airlinePaid] [decimal](20, 3) NULL,
	[airlineUnpaid] [decimal](20, 3) NULL,
	[systemPaid] [decimal](20, 3) NULL,
	[systemUnpaid] [decimal](20, 3) NULL,
	[exchangeId] [int] NULL,
	[osId] [int] NULL,
	[systemId] [int] NULL,
	[syValue] [decimal](20, 3) NULL,
	[tax_ratio] [decimal](20, 3) NULL,
	[tax_value] [decimal](20, 3) NULL,
	[currency] [nvarchar](50) NULL,
	[totalSY] [decimal](20, 3) NULL,
	[priceBeforTaxSY] [decimal](20, 3) NULL,
	[profitSY] [decimal](20, 3) NULL,
	[tax_valueSY] [decimal](20, 3) NULL,
 CONSTRAINT [PK_serviceData] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[serviceDataFiles]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[serviceDataFiles](
	[fileId] [int] IDENTITY(1,1) NOT NULL,
	[fileName] [nvarchar](500) NULL,
	[extention] [nvarchar](500) NULL,
	[folderName] [nvarchar](500) NULL,
	[notes] [nvarchar](500) NULL,
	[isActive] [bit] NULL,
	[serviceId] [int] NULL,
 CONSTRAINT [PK_serviceDataFiles] PRIMARY KEY CLUSTERED 
(
	[fileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[setting]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[setting](
	[settingId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[notes] [ntext] NULL,
 CONSTRAINT [PK_setting] PRIMARY KEY CLUSTERED 
(
	[settingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[setValues]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[setValues](
	[valId] [int] IDENTITY(1,1) NOT NULL,
	[value] [ntext] NULL,
	[isDefault] [int] NOT NULL,
	[isSystem] [int] NOT NULL,
	[notes] [ntext] NULL,
	[settingId] [int] NULL,
 CONSTRAINT [PK_setValues] PRIMARY KEY CLUSTERED 
(
	[valId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[statementsTable]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[statementsTable](
	[opStatementId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_statementsTable] PRIMARY KEY CLUSTERED 
(
	[opStatementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[systems]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[systems](
	[systemId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[notes] [nvarchar](500) NULL,
	[isActive] [bit] NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[company_commission] [decimal](20, 3) NULL,
	[balance] [decimal](20, 3) NULL,
	[code] [nvarchar](500) NULL,
 CONSTRAINT [PK_systems] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[toTable]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[toTable](
	[toTableId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[isActive] [bit] NULL,
	[notes] [nvarchar](500) NULL,
 CONSTRAINT [PK_toTable] PRIMARY KEY CLUSTERED 
(
	[toTableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NOT NULL,
	[AccountName] [nvarchar](500) NOT NULL,
	[lastName] [nvarchar](500) NULL,
	[company] [nvarchar](500) NULL,
	[email] [nvarchar](500) NULL,
	[phone] [nvarchar](500) NULL,
	[mobile] [nvarchar](500) NULL,
	[fax] [nvarchar](500) NULL,
	[address] [nvarchar](500) NULL,
	[agentLevel] [nvarchar](500) NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[password] [nvarchar](500) NULL,
	[type] [nvarchar](10) NULL,
	[image] [ntext] NULL,
	[notes] [nvarchar](500) NULL,
	[balance] [decimal](20, 3) NOT NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
	[isActive] [bit] NULL,
	[code] [nvarchar](500) NULL,
	[isAdmin] [bit] NULL,
	[groupId] [int] NULL,
	[balanceType] [tinyint] NULL,
	[job] [nvarchar](100) NULL,
	[isOnline] [tinyint] NULL,
	[countryId] [int] NULL,
 CONSTRAINT [PK_agents] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userSetValues]    Script Date: 17/10/2023 10:48:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userSetValues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[valId] [int] NULL,
	[note] [ntext] NULL,
	[createDate] [datetime2](7) NULL,
	[updateDate] [datetime2](7) NULL,
	[createUserId] [int] NULL,
	[updateUserId] [int] NULL,
 CONSTRAINT [PK_userSetValues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[airlines] ON 

INSERT [dbo].[airlines] ([airlineId], [name], [notes], [isActive]) VALUES (4, N'السورية للطيران', N'', 1)
INSERT [dbo].[airlines] ([airlineId], [name], [notes], [isActive]) VALUES (5, N'اجنحة الشام', N'', 1)
INSERT [dbo].[airlines] ([airlineId], [name], [notes], [isActive]) VALUES (6, N'ليلي', N'', 1)
SET IDENTITY_INSERT [dbo].[airlines] OFF
GO
SET IDENTITY_INSERT [dbo].[cities] ON 

INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (1, N'1', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (2, N'2', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (3, N'3', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (4, N'4', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (5, N'6', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (6, N'7', 2)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (7, N'2', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (8, N'3', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (9, N'4', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (10, N'6', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (11, N'7', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (12, N'9', 4)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (13, N'1', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (14, N'21', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (15, N'23', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (16, N'24', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (17, N'25', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (18, N'30', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (19, N'32', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (20, N'33', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (21, N'36', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (22, N'37', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (23, N'40', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (24, N'42', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (25, N'50', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (26, N'53', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (27, N'60', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (28, N'62', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (29, N'66', 7)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (30, N'1', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (31, N'4', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (32, N'5', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (33, N'6', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (34, N'7', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (35, N'8', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (36, N'9', 8)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (37, N'11', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (38, N'21', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (39, N'22', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (40, N'23', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (41, N'31', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (42, N'33', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (43, N'34', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (44, N'41', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (45, N'43', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (46, N'51', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (47, N'52', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (48, N'14', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (49, N'15', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (50, N'16', 9)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (51, N'1', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (52, N'2', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (53, N'3', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (54, N'4', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (55, N'5', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (56, N'6', 10)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (57, N'2', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (58, N'5', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (59, N'6', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (60, N'8', 11)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (61, N'21', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (62, N'24', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (63, N'25', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (64, N'26', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (65, N'27', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (66, N'29', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (67, N'31', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (68, N'32', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (69, N'33', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (70, N'34', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (71, N'35', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (72, N'36', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (73, N'37', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (74, N'38', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (75, N'41', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (76, N'43', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (77, N'45', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (78, N'46', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (79, N'48', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (80, N'49', 12)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (81, N'2', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (82, N'3', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (83, N'40', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (84, N'45', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (85, N'48', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (86, N'50', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (87, N'55', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (88, N'62', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (89, N'64', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (90, N'66', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (91, N'68', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (92, N'82', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (93, N'84', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (94, N'86', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (95, N'88', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (96, N'93', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (97, N'95', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (98, N'96', 13)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (99, N'97', 13)
GO
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (100, N'71', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (101, N'72', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (103, N'73', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (104, N'74', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (105, N'75', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (106, N'77', 14)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (107, N'21', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (108, N'51', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (109, N'57', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (110, N'61', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (111, N'87', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (112, N'521', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (113, N'652', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (114, N'727', 17)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (115, N'212', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (116, N'216', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (117, N'222', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (118, N'224', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (119, N'232', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (120, N'236', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (121, N'242', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (122, N'246', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (123, N'256', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (124, N'258', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (125, N'266', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (126, N'272', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (127, N'274', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (128, N'276', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (129, N'284', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (130, N'312', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (131, N'322', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (132, N'332', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (134, N'338', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (135, N'342', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (136, N'346', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (137, N'352', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (138, N'358', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (139, N'362', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (140, N'364', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (141, N'366', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (142, N'382', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (143, N'412', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (144, N'414', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (145, N'416', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (146, N'422', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (147, N'424', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (148, N'432', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (149, N'442', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (150, N'452', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (151, N'462', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (152, N'472', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (153, N'482', 19)
INSERT [dbo].[cities] ([cityId], [cityCode], [countryId]) VALUES (154, N'488', 19)
SET IDENTITY_INSERT [dbo].[cities] OFF
GO
SET IDENTITY_INSERT [dbo].[countriesCodes] ON 

INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (1, N'+965', N'KWD', N'Kuwait', 1, N'Arab Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (2, N'+966', N'SAR', N'Saudi Arabia', 0, N'Arab Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (3, N'+968', N'OMR', N'Oman', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (4, N'+971', N'AED', N'United Arab Emirates', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (5, N'+974', N'QAR', N'Qatar', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (6, N'+973', N'BHD', N'Bahrain', 0, N'Arabian Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (7, N'+964', N'IQD', N'Iraq', 0, N'Arabic Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (8, N'+961', N'LBP', N'Lebanon', 0, N'Middle East Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (9, N'+963', N'SYP', N'Syria', 0, N'Syria Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (10, N'+967', N'YER', N'Yemen', 0, N'Arab Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (11, N'+962', N'JOD', N'Jordan', 0, N'Jordan Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (12, N'+213', N'DZD', N'Algeria', 0, N'W. Central Africa Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (13, N'+20', N'EGP', N'Egypt', 0, N'Egypt Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (14, N'+216', N'TND', N'Tunisia', 0, N'W. Central Africa Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (15, N'+249', N'SDG', N'Sudan', 0, N'Sudan Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (16, N'+212', N'MAD', N'Morocco', 0, N'Morocco Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (17, N'+218', N'LYD', N'Libya', 0, N'Libya Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (18, N'+252', N'SOS', N'Somalia', 0, N'Libya Standard Time', NULL)
INSERT [dbo].[countriesCodes] ([countryId], [code], [currency], [name], [isDefault], [timeZoneName], [timeZoneOffset]) VALUES (19, N'+90', N'TRY', N'Turkey', 0, N'Turkey Standard Time', NULL)
SET IDENTITY_INSERT [dbo].[countriesCodes] OFF
GO
SET IDENTITY_INSERT [dbo].[exchange] ON 

INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (25, CAST(11500.000 AS Decimal(20, 3)), CAST(N'2023-10-01T18:09:11.5904335' AS DateTime2), 1, NULL, 2)
INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (26, CAST(12000.000 AS Decimal(20, 3)), CAST(N'2023-10-14T18:45:04.0969719' AS DateTime2), 1, NULL, 2)
INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (27, CAST(11500.000 AS Decimal(20, 3)), CAST(N'2023-10-14T18:48:01.1594164' AS DateTime2), 1, NULL, 2)
INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (28, CAST(12000.000 AS Decimal(20, 3)), CAST(N'2023-10-14T18:48:18.5746671' AS DateTime2), 1, NULL, 2)
INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (29, CAST(13000.000 AS Decimal(20, 3)), CAST(N'2023-10-15T07:49:25.1348274' AS DateTime2), 1, NULL, 2)
INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (30, CAST(12000.000 AS Decimal(20, 3)), CAST(N'2023-10-17T22:16:05.3846631' AS DateTime2), 1, NULL, 2)
INSERT [dbo].[exchange] ([exchangeId], [syValue], [createDate], [isActive], [notes], [createUserId]) VALUES (31, CAST(13000.000 AS Decimal(20, 3)), CAST(N'2023-10-17T22:23:18.0333499' AS DateTime2), 1, NULL, 2)
SET IDENTITY_INSERT [dbo].[exchange] OFF
GO
SET IDENTITY_INSERT [dbo].[flights] ON 

INSERT [dbo].[flights] ([flightId], [airline], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [flightTableId], [fromTableId], [toTableId], [isActive], [balance], [commission_ratio], [systemId], [airlineId], [type], [code]) VALUES (1026, NULL, N'', CAST(N'2023-10-01T18:18:28.1972434' AS DateTime2), CAST(N'2023-10-01T18:18:28.1972434' AS DateTime2), 2, 2, 16, NULL, NULL, 1, NULL, NULL, NULL, 4, 1, N'1')
SET IDENTITY_INSERT [dbo].[flights] OFF
GO
SET IDENTITY_INSERT [dbo].[flightTable] ON 

INSERT [dbo].[flightTable] ([flightTableId], [name], [isActive], [notes]) VALUES (16, N'دمشق-بيروت', 1, N'')
SET IDENTITY_INSERT [dbo].[flightTable] OFF
GO
SET IDENTITY_INSERT [dbo].[office] ON 

INSERT [dbo].[office] ([officeId], [name], [agentName], [joinDate], [mobile], [address], [userName], [passwordSY], [PasswordSoto], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [commission_ratio], [code]) VALUES (13, N'السلام', NULL, NULL, N'', NULL, N'Ahmad', N'', N'', N'', CAST(N'2023-10-01T18:12:14.9064513' AS DateTime2), CAST(N'2023-10-01T18:12:14.9064513' AS DateTime2), 2, 2, 1, CAST(0.000 AS Decimal(20, 3)), NULL, N'1')
SET IDENTITY_INSERT [dbo].[office] OFF
GO
SET IDENTITY_INSERT [dbo].[officeSystem] ON 

INSERT [dbo].[officeSystem] ([osId], [officeId], [systemId], [office_commission], [isActive], [notes]) VALUES (38, 13, 9, CAST(3.000 AS Decimal(20, 3)), 1, NULL)
INSERT [dbo].[officeSystem] ([osId], [officeId], [systemId], [office_commission], [isActive], [notes]) VALUES (39, 13, 10, CAST(0.000 AS Decimal(20, 3)), 1, NULL)
SET IDENTITY_INSERT [dbo].[officeSystem] OFF
GO
SET IDENTITY_INSERT [dbo].[passengers] ON 

INSERT [dbo].[passengers] ([passengerId], [name], [lastName], [father], [mother], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [code]) VALUES (29, N'محمد', N'محمود', N'', N'', N'', CAST(N'2023-10-01T18:14:01.3035534' AS DateTime2), CAST(N'2023-10-01T18:14:01.3035534' AS DateTime2), NULL, NULL, 1, CAST(0.000 AS Decimal(20, 3)), N'1')
INSERT [dbo].[passengers] ([passengerId], [name], [lastName], [father], [mother], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [code]) VALUES (30, N'hh', N'gg', N'', N'', N'', CAST(N'2023-10-10T02:33:36.3127709' AS DateTime2), CAST(N'2023-10-10T02:33:36.3127709' AS DateTime2), 2, 2, 1, CAST(0.000 AS Decimal(20, 3)), N'2')
SET IDENTITY_INSERT [dbo].[passengers] OFF
GO
SET IDENTITY_INSERT [dbo].[paySides] ON 

INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (1, N'passenger', N'مسافر', N'd', 1, N'passenger', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (2, N'office', N'مكتب', N'p d', 1, N'office', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (3, N'system', N'نظام الحجز', N'd', 1, N'system', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (4, N'soto', N'نظام السوتو', N'p', 1, N'soto', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (5, N'syr', N'نظام السوري', N'p', 1, N'syr', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (1001, N'other', N'جهة أخرى', N'p d', 1, N'other', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (1002, N'paysys', N'انظمة الدفع', N'n', 0, N'paysys', CAST(0.000 AS Decimal(20, 3)))
SET IDENTITY_INSERT [dbo].[paySides] OFF
GO
SET IDENTITY_INSERT [dbo].[serviceData] ON 

INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1075, N'SY-000001', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-01T18:20:34.4683965' AS DateTime2), CAST(N'2023-10-17T22:24:34.0118353' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 31, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1076, N'SY-000002', NULL, N'محمد محمود', N'595959', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(115000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T14:09:03.5767734' AS DateTime2), CAST(N'2023-10-14T14:14:19.8755451' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 25, 38, 9, CAST(11500.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(115000.000 AS Decimal(20, 3)), CAST(103500.000 AS Decimal(20, 3)), NULL, CAST(11500.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1077, N'SY-000003', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(115000.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T14:13:46.2963053' AS DateTime2), CAST(N'2023-10-14T17:44:26.1257673' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 25, NULL, 9, CAST(11500.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(115000.000 AS Decimal(20, 3)), CAST(103500.000 AS Decimal(20, 3)), CAST(6210.000 AS Decimal(20, 3)), CAST(11500.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1078, N'SY-000004', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T14:25:46.6116222' AS DateTime2), CAST(N'2023-10-15T18:49:06.2031344' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1079, N'SY-000005', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T17:43:57.6362121' AS DateTime2), CAST(N'2023-10-15T17:28:07.3366479' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1080, N'SY-000006', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T17:44:02.4531647' AS DateTime2), CAST(N'2023-10-15T17:27:24.4239110' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1081, N'SY-000007', NULL, N'محمد محمود', N'595959', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(120000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T17:44:05.8766594' AS DateTime2), CAST(N'2023-10-17T22:24:41.3450470' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 31, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1082, N'SY-000008', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T17:44:08.1236288' AS DateTime2), CAST(N'2023-10-15T07:35:14.4121857' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 28, 38, 9, CAST(12000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(120000.000 AS Decimal(20, 3)), CAST(108000.000 AS Decimal(20, 3)), CAST(3240.000 AS Decimal(20, 3)), CAST(12000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1083, N'SOTO-000001', NULL, N'محمد محمود', N'ewgterg', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(115000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T17:45:54.9882430' AS DateTime2), CAST(N'2023-10-14T18:29:40.7790070' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 25, 38, 9, CAST(11500.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(115000.000 AS Decimal(20, 3)), CAST(103500.000 AS Decimal(20, 3)), CAST(3105.000 AS Decimal(20, 3)), CAST(11500.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1084, N'SOTO-000002', NULL, NULL, N'ewgterg', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(115000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-14T18:29:37.8524753' AS DateTime2), CAST(N'2023-10-14T18:29:37.8524753' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 25, 38, 9, CAST(11500.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(115000.000 AS Decimal(20, 3)), CAST(103500.000 AS Decimal(20, 3)), CAST(3105.000 AS Decimal(20, 3)), CAST(11500.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1085, N'SY-000009', NULL, N'محمد محمود', N'595959', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(120000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T02:58:12.4250436' AS DateTime2), CAST(N'2023-10-15T07:37:45.0185486' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 28, 38, 9, CAST(12000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(120000.000 AS Decimal(20, 3)), CAST(108000.000 AS Decimal(20, 3)), CAST(3240.000 AS Decimal(20, 3)), CAST(12000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1086, N'SY-000010', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T02:58:14.8452486' AS DateTime2), CAST(N'2023-10-15T07:37:39.0360910' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 28, 38, 9, CAST(12000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(120000.000 AS Decimal(20, 3)), CAST(108000.000 AS Decimal(20, 3)), CAST(3240.000 AS Decimal(20, 3)), CAST(12000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1087, N'SY-000011', NULL, N'محمد محمود', N'595959', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(120000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T02:58:17.1598176' AS DateTime2), CAST(N'2023-10-15T07:35:46.6381961' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 28, 38, 9, CAST(12000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(120000.000 AS Decimal(20, 3)), CAST(108000.000 AS Decimal(20, 3)), CAST(3240.000 AS Decimal(20, 3)), CAST(12000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1088, N'SOTO-000003', NULL, N'محمد محمود', N'ewgterg', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(130000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T11:37:19.2587009' AS DateTime2), CAST(N'2023-10-15T11:37:33.4533286' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1089, N'SOTO-000004', NULL, NULL, N'ewgterg', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(130000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T11:37:42.1462996' AS DateTime2), CAST(N'2023-10-15T11:37:42.1462996' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1090, N'SOTO-000005', NULL, N'محمد محمود', N'ewgterg', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(130000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T11:37:44.5538356' AS DateTime2), CAST(N'2023-10-15T11:37:58.3695510' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1091, N'SOTO-000006', NULL, N'محمد محمود', N'ewgterg', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(130000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T11:38:02.4470330' AS DateTime2), CAST(N'2023-10-15T11:38:07.2341567' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1092, N'SOTO-000007', NULL, N'محمد محمود', N'ewgterg', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(130000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T11:38:11.1590981' AS DateTime2), CAST(N'2023-10-15T11:38:14.1506742' AS DateTime2), 2, 2, 29, 1026, NULL, N'soto', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1093, N'SY-000012', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(9.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T17:25:16.8266193' AS DateTime2), CAST(N'2023-10-15T17:26:20.9360126' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1094, N'SY-000013', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:07:02.1711454' AS DateTime2), CAST(N'2023-10-15T18:07:11.5293156' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1095, N'SY-000014', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(9.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:47:20.6142207' AS DateTime2), CAST(N'2023-10-15T18:48:00.8395528' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1096, N'SY-000015', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(9.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:48:10.0179043' AS DateTime2), CAST(N'2023-10-15T18:48:36.2766692' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1097, N'SY-000016', NULL, N'محمد محمود', N'94984', N'السورية للطيران / دمشق-بيروت / ذهاب', NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:49:11.6993325' AS DateTime2), CAST(N'2023-10-15T18:49:19.6528448' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1098, N'SY-000017', NULL, NULL, N'94984', NULL, NULL, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:49:16.1567950' AS DateTime2), CAST(N'2023-10-15T18:49:16.1567950' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.460 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, NULL, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(7020.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1099, N'SY-000018', NULL, NULL, N'يبلاايبلا', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:54:07.0513638' AS DateTime2), CAST(N'2023-10-15T18:54:07.0513638' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1100, N'SY-000019', NULL, NULL, N'يبلاايبلا', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:54:26.1966293' AS DateTime2), CAST(N'2023-10-15T18:54:26.1966293' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1101, N'SY-000020', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(9.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:54:38.8149961' AS DateTime2), CAST(N'2023-10-15T18:54:44.0785328' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1102, N'SY-000021', NULL, NULL, N'يبلاايبلا', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(9.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:54:51.8629899' AS DateTime2), CAST(N'2023-10-15T18:54:51.8629899' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1103, N'SY-000022', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:54:58.0610748' AS DateTime2), CAST(N'2023-10-15T23:13:52.0978573' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1104, N'SY-000023', NULL, NULL, N'يبلاايبلا', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:55:21.7956053' AS DateTime2), CAST(N'2023-10-15T18:55:21.7956053' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1105, N'SY-000024', NULL, N'محمد محمود', N'يبلاايبلا', N'السورية للطيران / دمشق-بيروت / ذهاب', 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(100000.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-15T18:55:28.0536233' AS DateTime2), CAST(N'2023-10-15T18:55:34.5375614' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 29, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'syp', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId], [osId], [systemId], [syValue], [tax_ratio], [tax_value], [currency], [totalSY], [priceBeforTaxSY], [profitSY], [tax_valueSY]) VALUES (1106, N'SY-000025', NULL, NULL, N'يبلاايبلا', NULL, 13, NULL, NULL, NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(9.000 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), NULL, NULL, CAST(10.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), N'', N'draft', CAST(N'2023-10-17T22:24:45.1780030' AS DateTime2), CAST(N'2023-10-17T22:24:45.1780030' AS DateTime2), 2, 2, 29, 1026, NULL, N'syr', 1, NULL, NULL, CAST(0.270 AS Decimal(20, 3)), CAST(3.000 AS Decimal(20, 3)), CAST(0.540 AS Decimal(20, 3)), CAST(6.000 AS Decimal(20, 3)), CAST(8.190 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(10.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.270 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL, 31, 38, 9, CAST(13000.000 AS Decimal(20, 3)), NULL, CAST(1.000 AS Decimal(20, 3)), N'usd', CAST(130000.000 AS Decimal(20, 3)), CAST(117000.000 AS Decimal(20, 3)), CAST(3510.000 AS Decimal(20, 3)), CAST(13000.000 AS Decimal(20, 3)))
SET IDENTITY_INSERT [dbo].[serviceData] OFF
GO
SET IDENTITY_INSERT [dbo].[setting] ON 

INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (1, N'com_name', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (2, N'com_address', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (3, N'com_email', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (4, N'com_mobile', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (5, N'com_phone', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (6, N'com_fax', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (7, N'com_logo', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (8, N'region', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (9, N'language', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (10, N'currency', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (11, N'tax', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (12, N'storage_cost', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (13, N'pur_order_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (14, N'dateForm', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (15, N'sale_email_temp', N'emailtemp')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (16, N'sale_order_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (17, N'quotation_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (18, N'required_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (19, N'sale_copy_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (20, N'pur_copy_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (21, N'print_on_save_sale', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (22, N'print_on_save_pur', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (23, N'email_on_save_sale', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (24, N'email_on_save_pur', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (25, N'menuIsOpen', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (26, N'report_lang', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (27, N'rep_copy_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (28, N'user_path', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (29, N'accuracy', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (30, N'pur_email_temp', N'emailtemp-no')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (31, N'Pur_inv_avg_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (32, N'Allow_print_inv_count', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (33, N'item_cost', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (34, N'sale_note', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (35, N'upgrade_email_temp', N'emailtemp')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (36, N'syr_commission', N'')
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (37, N'soto_commission', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (38, N'office_syr_commission', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (39, N'office_soto_commission', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (40, N'company_syr_commission', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (41, N'company_soto_commission', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (42, N'docPapersize', NULL)
INSERT [dbo].[setting] ([settingId], [name], [notes]) VALUES (43, N'rep_printer_name', NULL)
SET IDENTITY_INSERT [dbo].[setting] OFF
GO
SET IDENTITY_INSERT [dbo].[setValues] ON 

INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (1, N'en', 0, 0, NULL, 9)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (2, N'ar', 0, 0, NULL, 9)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (12, N'0', 1, 1, NULL, 11)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (13, N'0', 1, 1, NULL, 12)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (14, N'3204215c19f25609034d24451f7e03d7.png', 1, 1, N'', 7)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (15, N'purchase order', 0, 1, N'title', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (16, N'this is ', 0, 1, N'text1', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (17, N'', 0, 1, N'text2', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (18, N'', 0, 1, N'link1text', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (19, N'', 0, 1, N'link2text', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (20, N'', 0, 0, N'link3text', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (22, N'', 0, 1, N'link1url', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (23, N'', 0, 1, N'link2url', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (24, N'', 0, 1, N'link3url', 13)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (25, N'ShortDatePattern', 1, 1, NULL, 14)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (26, N'Sales email', 0, 1, N'title', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (27, N'', 0, 1, N'text1', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (28, N'', 0, 1, N'text2', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (29, N'', 0, 1, N'link1text', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (30, N'', 0, 1, N'link2text', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (31, N'', 0, 0, N'link3text', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (32, N'', 0, 1, N'link1url', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (33, N'', 0, 1, N'link2url', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (34, N'', 0, 1, N'link3url', 15)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (39, N'Sales Order', 0, 1, N'title', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (40, N'', 0, 0, N'text1', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (41, N'', 0, 1, N'text2', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (42, N'', 0, 1, N'link1text', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (43, N'', 0, 1, N'link2text', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (44, N'', 0, 1, N'link3text', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (45, N'', 0, 1, N'link1url', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (46, N'', 0, 1, N'link2url', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (47, N'', 0, 1, N'link3url', 16)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (48, N'Quotaion', 0, 1, N'title', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (49, N'', 0, 0, N'text1', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (50, N'', 0, 1, N'text2', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (51, N'', 0, 1, N'link1text', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (52, N'', 0, 1, N'link2text', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (53, N'', 0, 0, N'link3text', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (54, N'', 0, 1, N'link1url', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (55, N'', 0, 1, N'link2url', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (56, N'', 0, 1, N'link3url', 17)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (58, N'JellyFish', 1, 1, NULL, 1)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (59, N'سوريا', 1, 1, NULL, 2)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (60, N'admin@info.com', 1, 1, NULL, 3)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (61, N'+963-934444444', 1, 1, NULL, 4)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (62, N'+963-43-888888888', 1, 1, NULL, 5)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (63, N'+963-33-321321322', 1, 1, NULL, 6)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (64, N'Req', 0, 1, N'title', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (65, N'', 0, 0, N'text1', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (66, N'', 0, 1, N'text2', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (67, N'', 0, 1, N'link1text', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (68, N'', 0, 1, N'link2text', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (69, N'', 0, 0, N'link3text', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (70, N'', 0, 1, N'link1url', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (71, N'', 0, 1, N'link2url', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (72, N'', 0, 1, N'link3url', 18)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (73, N'1', 0, 1, N'print', 19)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (74, N'1', 0, 1, N'print', 20)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (75, N'0', 0, 1, N'print', 21)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (76, N'0', 0, 1, N'print', 22)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (77, N'0', 0, 1, N'print', 23)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (78, N'0', 0, 1, N'print', 24)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (79, N'open', 0, 0, NULL, 25)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (80, N'close', 0, 0, NULL, 25)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (81, N'en', 1, 1, NULL, 26)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (82, N'ar', 0, 1, NULL, 26)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (83, N'1', 1, 1, N'print', 27)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (84, N'first', 0, 0, N'0', 28)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (85, N'second', 0, 0, N'0', 28)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (87, N'2', 1, 1, N'0', 29)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (88, N'Purchase', 0, 1, N'title', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (89, N'', 0, 0, N'text1', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (90, N'', 0, 1, N'text2', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (91, N'', 0, 1, N'link1text', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (92, N'', 0, 1, N'link2text', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (93, N'', 0, 1, N'link3text', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (94, N'', 0, 1, N'link1url', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (95, N'', 0, 1, N'link2url', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (96, N'', 0, 1, N'link3url', 30)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (109, N'5', 1, 1, N'0', 31)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (110, N'5', 1, 1, N'print', 32)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (111, N'0', 1, 1, N'0', 33)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (112, N'', 1, 1, N'sale_note', 34)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (113, N'Booking email', 0, 1, N'title', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (114, N'', 0, 1, N'text1', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (115, N'', 0, 1, N'text2', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (116, N'', 0, 1, N'link1text', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (117, N'', 0, 1, N'link2text', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (118, N'', 0, 1, N'link3text', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (119, N'', 0, 1, N'link1url', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (120, N'', 0, 1, N'link2url', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (121, N'', 0, 1, N'link3url', 35)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (124, N'0', 0, 1, NULL, 36)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (125, N'0', 0, 1, NULL, 37)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (126, N'0', 0, 1, NULL, 38)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (127, N'0', 0, 1, NULL, 39)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (128, N'0', 0, 1, NULL, 40)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (129, N'0', 0, 1, NULL, 41)
GO
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (130, N'A4', 0, 1, NULL, 42)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (131, N'Microsoft Print to PDF', 0, 0, NULL, 43)
SET IDENTITY_INSERT [dbo].[setValues] OFF
GO
SET IDENTITY_INSERT [dbo].[systems] ON 

INSERT [dbo].[systems] ([systemId], [name], [notes], [isActive], [createDate], [updateDate], [createUserId], [updateUserId], [company_commission], [balance], [code]) VALUES (9, N'السوري', N'', 1, CAST(N'2023-10-01T18:11:23.8463362' AS DateTime2), CAST(N'2023-10-01T18:11:23.8463362' AS DateTime2), 2, 2, CAST(6.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), N'1')
INSERT [dbo].[systems] ([systemId], [name], [notes], [isActive], [createDate], [updateDate], [createUserId], [updateUserId], [company_commission], [balance], [code]) VALUES (10, N'اياتيكو', N'', 1, CAST(N'2023-10-01T18:11:35.5778414' AS DateTime2), CAST(N'2023-10-01T18:11:35.5778414' AS DateTime2), 2, 2, CAST(6.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), N'2')
SET IDENTITY_INSERT [dbo].[systems] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (1, N'administrator', N'administrator', N'Support', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2023-07-28T15:51:31.4860429' AS DateTime2), N'8ea60f80800198548a9a81aa2e4a6115', N'ad', N'57440ff6b80f068efd50410ea24fd593.png', N'', CAST(0.000 AS Decimal(20, 3)), 1, 2, 1, N'Us-Admin', 1, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (2, N'admin', N'admin', N'admin', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2023-10-17T22:36:38.2006888' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'ad', N'c37858823db0093766eee74d8ee1f1e5.png', N'', CAST(0.000 AS Decimal(20, 3)), 1, 2, 1, N'Us-adminuser', 1, NULL, NULL, NULL, 1, 9)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
ALTER TABLE [dbo].[countriesCodes] ADD  CONSTRAINT [DF_countriesCodes_isDefault]  DEFAULT ((0)) FOR [isDefault]
GO
ALTER TABLE [dbo].[customers] ADD  CONSTRAINT [DF_customers_balance]  DEFAULT ((0)) FOR [balance]
GO
ALTER TABLE [dbo].[customers] ADD  CONSTRAINT [DF_customers_balanceType]  DEFAULT ((0)) FOR [balanceType]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_users_balance]  DEFAULT ((0)) FOR [balance]
GO
ALTER TABLE [dbo].[cities]  WITH CHECK ADD  CONSTRAINT [FK_cities_countriesCodes] FOREIGN KEY([countryId])
REFERENCES [dbo].[countriesCodes] ([countryId])
GO
ALTER TABLE [dbo].[cities] CHECK CONSTRAINT [FK_cities_countriesCodes]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_countriesCodes] FOREIGN KEY([countryId])
REFERENCES [dbo].[countriesCodes] ([countryId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_countriesCodes]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_users]
GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_users1]
GO
ALTER TABLE [dbo].[error]  WITH CHECK ADD  CONSTRAINT [FK_error_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[error] CHECK CONSTRAINT [FK_error_users]
GO
ALTER TABLE [dbo].[flights]  WITH CHECK ADD  CONSTRAINT [FK_flights_airlines] FOREIGN KEY([airlineId])
REFERENCES [dbo].[airlines] ([airlineId])
GO
ALTER TABLE [dbo].[flights] CHECK CONSTRAINT [FK_flights_airlines]
GO
ALTER TABLE [dbo].[flights]  WITH CHECK ADD  CONSTRAINT [FK_flights_flightTable] FOREIGN KEY([flightTableId])
REFERENCES [dbo].[flightTable] ([flightTableId])
GO
ALTER TABLE [dbo].[flights] CHECK CONSTRAINT [FK_flights_flightTable]
GO
ALTER TABLE [dbo].[flights]  WITH CHECK ADD  CONSTRAINT [FK_flights_fromTable] FOREIGN KEY([fromTableId])
REFERENCES [dbo].[fromTable] ([fromTableId])
GO
ALTER TABLE [dbo].[flights] CHECK CONSTRAINT [FK_flights_fromTable]
GO
ALTER TABLE [dbo].[flights]  WITH CHECK ADD  CONSTRAINT [FK_flights_toTable] FOREIGN KEY([toTableId])
REFERENCES [dbo].[toTable] ([toTableId])
GO
ALTER TABLE [dbo].[flights] CHECK CONSTRAINT [FK_flights_toTable]
GO
ALTER TABLE [dbo].[office]  WITH CHECK ADD  CONSTRAINT [FK_office_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[office] CHECK CONSTRAINT [FK_office_users]
GO
ALTER TABLE [dbo].[office]  WITH CHECK ADD  CONSTRAINT [FK_office_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[office] CHECK CONSTRAINT [FK_office_users1]
GO
ALTER TABLE [dbo].[officeFiles]  WITH CHECK ADD  CONSTRAINT [FK_officeFiles_office] FOREIGN KEY([officeId])
REFERENCES [dbo].[office] ([officeId])
GO
ALTER TABLE [dbo].[officeFiles] CHECK CONSTRAINT [FK_officeFiles_office]
GO
ALTER TABLE [dbo].[officeSystem]  WITH CHECK ADD  CONSTRAINT [FK_officeSystem_office] FOREIGN KEY([officeId])
REFERENCES [dbo].[office] ([officeId])
GO
ALTER TABLE [dbo].[officeSystem] CHECK CONSTRAINT [FK_officeSystem_office]
GO
ALTER TABLE [dbo].[officeSystem]  WITH CHECK ADD  CONSTRAINT [FK_officeSystem_systems] FOREIGN KEY([systemId])
REFERENCES [dbo].[systems] ([systemId])
GO
ALTER TABLE [dbo].[officeSystem] CHECK CONSTRAINT [FK_officeSystem_systems]
GO
ALTER TABLE [dbo].[operations]  WITH CHECK ADD  CONSTRAINT [FK_operations_durationsTable] FOREIGN KEY([durationId])
REFERENCES [dbo].[durationsTable] ([durationId])
GO
ALTER TABLE [dbo].[operations] CHECK CONSTRAINT [FK_operations_durationsTable]
GO
ALTER TABLE [dbo].[operations]  WITH CHECK ADD  CONSTRAINT [FK_operations_statementsTable] FOREIGN KEY([opStatementId])
REFERENCES [dbo].[statementsTable] ([opStatementId])
GO
ALTER TABLE [dbo].[operations] CHECK CONSTRAINT [FK_operations_statementsTable]
GO
ALTER TABLE [dbo].[passengerFiles]  WITH CHECK ADD  CONSTRAINT [FK_passengerFiles_passengers] FOREIGN KEY([passengerId])
REFERENCES [dbo].[passengers] ([passengerId])
GO
ALTER TABLE [dbo].[passengerFiles] CHECK CONSTRAINT [FK_passengerFiles_passengers]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_exchange] FOREIGN KEY([exchangeId])
REFERENCES [dbo].[exchange] ([exchangeId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_exchange]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_flights] FOREIGN KEY([flightId])
REFERENCES [dbo].[flights] ([flightId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_flights]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_office] FOREIGN KEY([officeId])
REFERENCES [dbo].[office] ([officeId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_office]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_passengers] FOREIGN KEY([passengerId])
REFERENCES [dbo].[passengers] ([passengerId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_passengers]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_payOp] FOREIGN KEY([sourceId])
REFERENCES [dbo].[payOp] ([payOpId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_payOp]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_paySides] FOREIGN KEY([paysideId])
REFERENCES [dbo].[paySides] ([paysideId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_paySides]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_serviceData] FOREIGN KEY([serviceId])
REFERENCES [dbo].[serviceData] ([serviceId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_serviceData]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_systems] FOREIGN KEY([systemId])
REFERENCES [dbo].[systems] ([systemId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_systems]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_users]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_users1]
GO
ALTER TABLE [dbo].[payOp]  WITH CHECK ADD  CONSTRAINT [FK_payOp_users2] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[payOp] CHECK CONSTRAINT [FK_payOp_users2]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_exchange] FOREIGN KEY([exchangeId])
REFERENCES [dbo].[exchange] ([exchangeId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_exchange]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_flights] FOREIGN KEY([flightId])
REFERENCES [dbo].[flights] ([flightId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_flights]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_office] FOREIGN KEY([officeId])
REFERENCES [dbo].[office] ([officeId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_office]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_officeSystem] FOREIGN KEY([osId])
REFERENCES [dbo].[officeSystem] ([osId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_officeSystem]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_operations] FOREIGN KEY([operationId])
REFERENCES [dbo].[operations] ([operationId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_operations]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_passengers] FOREIGN KEY([passengerId])
REFERENCES [dbo].[passengers] ([passengerId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_passengers]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_systems] FOREIGN KEY([systemId])
REFERENCES [dbo].[systems] ([systemId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_systems]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_users]
GO
ALTER TABLE [dbo].[serviceData]  WITH CHECK ADD  CONSTRAINT [FK_serviceData_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[serviceData] CHECK CONSTRAINT [FK_serviceData_users1]
GO
ALTER TABLE [dbo].[serviceDataFiles]  WITH CHECK ADD  CONSTRAINT [FK_serviceDataFiles_serviceData] FOREIGN KEY([serviceId])
REFERENCES [dbo].[serviceData] ([serviceId])
GO
ALTER TABLE [dbo].[serviceDataFiles] CHECK CONSTRAINT [FK_serviceDataFiles_serviceData]
GO
ALTER TABLE [dbo].[setValues]  WITH CHECK ADD  CONSTRAINT [FK_setValues_setting] FOREIGN KEY([settingId])
REFERENCES [dbo].[setting] ([settingId])
GO
ALTER TABLE [dbo].[setValues] CHECK CONSTRAINT [FK_setValues_setting]
GO
ALTER TABLE [dbo].[systems]  WITH CHECK ADD  CONSTRAINT [FK_systems_users] FOREIGN KEY([createUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[systems] CHECK CONSTRAINT [FK_systems_users]
GO
ALTER TABLE [dbo].[systems]  WITH CHECK ADD  CONSTRAINT [FK_systems_users1] FOREIGN KEY([updateUserId])
REFERENCES [dbo].[users] ([userId])
GO
ALTER TABLE [dbo].[systems] CHECK CONSTRAINT [FK_systems_users1]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_countriesCodes] FOREIGN KEY([countryId])
REFERENCES [dbo].[countriesCodes] ([countryId])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_countriesCodes]
GO
ALTER TABLE [dbo].[userSetValues]  WITH CHECK ADD  CONSTRAINT [FK_userSetValues_setValues] FOREIGN KEY([valId])
REFERENCES [dbo].[setValues] ([valId])
GO
ALTER TABLE [dbo].[userSetValues] CHECK CONSTRAINT [FK_userSetValues_setValues]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'syr or soto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'serviceData'
GO

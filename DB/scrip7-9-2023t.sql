USE [bookdb]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'serviceData'
GO
ALTER TABLE [dbo].[userSetValues] DROP CONSTRAINT [FK_userSetValues_setValues]
GO
ALTER TABLE [dbo].[users] DROP CONSTRAINT [FK_users_countriesCodes]
GO
ALTER TABLE [dbo].[setValues] DROP CONSTRAINT [FK_setValues_setting]
GO
ALTER TABLE [dbo].[serviceDataFiles] DROP CONSTRAINT [FK_serviceDataFiles_serviceData]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_users1]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_users]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_passengers]
GO
ALTER TABLE [dbo].[serviceData] DROP CONSTRAINT [FK_serviceData_operations]
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
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_serviceData]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_paySides]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_passengers]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_office]
GO
ALTER TABLE [dbo].[payOp] DROP CONSTRAINT [FK_payOp_flights]
GO
ALTER TABLE [dbo].[passengerFiles] DROP CONSTRAINT [FK_passengerFiles_passengers]
GO
ALTER TABLE [dbo].[operations] DROP CONSTRAINT [FK_operations_statementsTable]
GO
ALTER TABLE [dbo].[operations] DROP CONSTRAINT [FK_operations_durationsTable]
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
/****** Object:  Table [dbo].[userSetValues]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[userSetValues]') AND type in (N'U'))
DROP TABLE [dbo].[userSetValues]
GO
/****** Object:  Table [dbo].[users]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
DROP TABLE [dbo].[users]
GO
/****** Object:  Table [dbo].[toTable]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[toTable]') AND type in (N'U'))
DROP TABLE [dbo].[toTable]
GO
/****** Object:  Table [dbo].[statementsTable]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[statementsTable]') AND type in (N'U'))
DROP TABLE [dbo].[statementsTable]
GO
/****** Object:  Table [dbo].[setValues]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setValues]') AND type in (N'U'))
DROP TABLE [dbo].[setValues]
GO
/****** Object:  Table [dbo].[setting]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setting]') AND type in (N'U'))
DROP TABLE [dbo].[setting]
GO
/****** Object:  Table [dbo].[serviceDataFiles]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceDataFiles]') AND type in (N'U'))
DROP TABLE [dbo].[serviceDataFiles]
GO
/****** Object:  Table [dbo].[serviceData]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceData]') AND type in (N'U'))
DROP TABLE [dbo].[serviceData]
GO
/****** Object:  Table [dbo].[paySides]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[paySides]') AND type in (N'U'))
DROP TABLE [dbo].[paySides]
GO
/****** Object:  Table [dbo].[payOp]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[payOp]') AND type in (N'U'))
DROP TABLE [dbo].[payOp]
GO
/****** Object:  Table [dbo].[passengers]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[passengers]') AND type in (N'U'))
DROP TABLE [dbo].[passengers]
GO
/****** Object:  Table [dbo].[passengerFiles]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[passengerFiles]') AND type in (N'U'))
DROP TABLE [dbo].[passengerFiles]
GO
/****** Object:  Table [dbo].[operations]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[operations]') AND type in (N'U'))
DROP TABLE [dbo].[operations]
GO
/****** Object:  Table [dbo].[officeFiles]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[officeFiles]') AND type in (N'U'))
DROP TABLE [dbo].[officeFiles]
GO
/****** Object:  Table [dbo].[office]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[office]') AND type in (N'U'))
DROP TABLE [dbo].[office]
GO
/****** Object:  Table [dbo].[fromTable]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fromTable]') AND type in (N'U'))
DROP TABLE [dbo].[fromTable]
GO
/****** Object:  Table [dbo].[flightTable]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[flightTable]') AND type in (N'U'))
DROP TABLE [dbo].[flightTable]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[flights]') AND type in (N'U'))
DROP TABLE [dbo].[flights]
GO
/****** Object:  Table [dbo].[exchange]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[exchange]') AND type in (N'U'))
DROP TABLE [dbo].[exchange]
GO
/****** Object:  Table [dbo].[error]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[error]') AND type in (N'U'))
DROP TABLE [dbo].[error]
GO
/****** Object:  Table [dbo].[durationsTable]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[durationsTable]') AND type in (N'U'))
DROP TABLE [dbo].[durationsTable]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[customers]') AND type in (N'U'))
DROP TABLE [dbo].[customers]
GO
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[countriesCodes]') AND type in (N'U'))
DROP TABLE [dbo].[countriesCodes]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 7/9/2023 11:23:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cities]') AND type in (N'U'))
DROP TABLE [dbo].[cities]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[customers]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[durationsTable]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[error]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[exchange]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[flights]    Script Date: 7/9/2023 11:23:31 AM ******/
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
 CONSTRAINT [PK_flights] PRIMARY KEY CLUSTERED 
(
	[flightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flightTable]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[fromTable]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[office]    Script Date: 7/9/2023 11:23:31 AM ******/
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
 CONSTRAINT [PK_office] PRIMARY KEY CLUSTERED 
(
	[officeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[officeFiles]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[operations]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[passengerFiles]    Script Date: 7/9/2023 11:23:31 AM ******/
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
/****** Object:  Table [dbo].[passengers]    Script Date: 7/9/2023 11:23:32 AM ******/
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
 CONSTRAINT [PK_passengers] PRIMARY KEY CLUSTERED 
(
	[passengerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payOp]    Script Date: 7/9/2023 11:23:32 AM ******/
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
 CONSTRAINT [PK_payOptb] PRIMARY KEY CLUSTERED 
(
	[payOpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paySides]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[serviceData]    Script Date: 7/9/2023 11:23:32 AM ******/
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
 CONSTRAINT [PK_serviceData] PRIMARY KEY CLUSTERED 
(
	[serviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[serviceDataFiles]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[setting]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[setValues]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[statementsTable]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[toTable]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[users]    Script Date: 7/9/2023 11:23:32 AM ******/
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
/****** Object:  Table [dbo].[userSetValues]    Script Date: 7/9/2023 11:23:32 AM ******/
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
SET IDENTITY_INSERT [dbo].[customers] ON 

INSERT [dbo].[customers] ([custId], [custname], [lastName], [company], [email], [phone], [mobile], [fax], [address], [custlevel], [createDate], [updateDate], [custCode], [image], [notes], [balance], [balanceType], [createUserId], [updateUserId], [isActive], [countryId]) VALUES (4, N'demo ', N'customer', N'Demo', N'', NULL, N'+965-959595959', NULL, N'', N'Normal', CAST(N'2023-05-15T17:16:32.9577927' AS DateTime2), CAST(N'2023-05-15T17:16:32.9577927' AS DateTime2), N'qjgtk', NULL, N'', CAST(0.000 AS Decimal(20, 3)), 0, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[customers] OFF
GO
SET IDENTITY_INSERT [dbo].[durationsTable] ON 

INSERT [dbo].[durationsTable] ([durationId], [name], [isActive], [notes]) VALUES (5, N'1', 1, N'')
INSERT [dbo].[durationsTable] ([durationId], [name], [isActive], [notes]) VALUES (6, N'2', 1, N'')
SET IDENTITY_INSERT [dbo].[durationsTable] OFF
GO
SET IDENTITY_INSERT [dbo].[error] ON 

INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2082, N'-2146233087', N'''Provide value on ''System.Windows.StaticResourceExtension'' threw an exception.'' Line number ''79'' and line position ''45''.', N'   at System.Windows.Markup.WpfXamlLoader.Load(XamlReader xamlReader, IXamlObjectWriterFactory writerFactory, Boolean skipJournaledProperties, Object rootObject, XamlObjectWriterSettings settings, Uri baseUri)
   at System.Windows.Markup.WpfXamlLoader.LoadBaml(XamlReader xamlReader, Boolean skipJournaledProperties, Object rootObject, XamlAccessLevel accessLevel, Uri baseUri)
   at System.Windows.Markup.XamlReader.LoadBaml(Stream stream, ParserContext parserContext, Object parent, Boolean closeStream)
   at System.Windows.Application.LoadComponent(Object component, Uri resourceLocator)
   at BookAccountApp.View.sectionData.win_lvc.InitializeComponent() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\win_lvc.xaml:line 1
   at BookAccountApp.View.sectionData.win_lvc..ctor(IEnumerable`1 _passengersQuery, Int32 _data, Boolean _isSameList) in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\win_lvc.xaml.cs:line 54', N'System.Object Load(System.Xaml.XamlReader, System.Xaml.IXamlObjectWriterFactory, Boolean, System.Object, System.Xaml.XamlObjectWriterSettings, System.Uri)', CAST(N'2023-08-28T03:14:11.5656433' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2083, N'-2147467261', N'Object reference not set to an instance of an object.', N'   at BookAccountApp.View.sectionData.win_lvc.fillSelectedChart() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\win_lvc.xaml.cs:line 730
   at BookAccountApp.View.sectionData.win_lvc.Window_Loaded(Object sender, RoutedEventArgs e) in D:\Github\book-account\BookAccountApp\BookAccountApp\View\sectionData\win_lvc.xaml.cs:line 141', N'Void fillSelectedChart()', CAST(N'2023-08-28T03:14:11.7780754' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2084, N'-2147024816', N'The file ''D:\1test\ff\bkg-blu.jpg'' already exists.', N'   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.File.InternalCopy(String sourceFileName, String destFileName, Boolean overwrite, Boolean checkHost)
   at System.IO.File.Copy(String sourceFileName, String destFileName)
   at BookAccountApp.View.windows.wd_files.Btn_export_Click(Object sender, RoutedEventArgs e) in D:\Github\book-account\BookAccountApp\BookAccountApp\View\windows\wd_files.xaml.cs:line 384', N'Void WinIOError(Int32, System.String)', CAST(N'2023-08-28T21:12:10.4576509' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (2085, N'-2147024893', N'Could not find a part of the path ''Thumb/passenger\4\bkg-blu.jpg''.', N'   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   at System.IO.File.InternalCopy(String sourceFileName, String destFileName, Boolean overwrite, Boolean checkHost)
   at System.IO.File.Copy(String sourceFileName, String destFileName)
   at BookAccountApp.View.windows.wd_files.Btn_export_Click(Object sender, RoutedEventArgs e) in D:\Github\book-account\BookAccountApp\BookAccountApp\View\windows\wd_files.xaml.cs:line 389', N'Void WinIOError(Int32, System.String)', CAST(N'2023-08-28T22:36:32.2163038' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (3082, N'-2146233086', N'Specified argument was out of the range of valid values.
Parameter name: index', N'   at System.Windows.Media.VisualCollection.get_Item(Int32 index)
   at System.Windows.Controls.UIElementCollection.get_Item(Int32 index)
   at System.Windows.Controls.Grid.GridChildrenCollectionEnumeratorSimple.MoveNext()
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at BookAccountApp.View.accounting.uc_payment.Clear() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\accounting\uc_payment.xaml.cs:line 440
   at BookAccountApp.View.accounting.uc_payment.<UserControl_Loaded>d__11.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\accounting\uc_payment.xaml.cs:line 97', N'System.Windows.Media.Visual get_Item(Int32)', CAST(N'2023-08-30T17:06:38.7303356' AS DateTime2), 2)
INSERT [dbo].[error] ([errorId], [num], [msg], [stackTrace], [targetSite], [createDate], [createUserId]) VALUES (3083, N'-2146233086', N'Specified argument was out of the range of valid values.
Parameter name: index', N'   at System.Windows.Media.VisualCollection.get_Item(Int32 index)
   at System.Windows.Controls.UIElementCollection.get_Item(Int32 index)
   at System.Windows.Controls.Grid.GridChildrenCollectionEnumeratorSimple.MoveNext()
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1._VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(FrameworkElement fe, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.VisitNode(DependencyObject d, Boolean visitedViaVisualTree)
   at System.Windows.DescendentsWalker`1.WalkLogicalChildren(FrameworkElement feParent, FrameworkContentElement fceParent, IEnumerator logicalChildren)
   at System.Windows.DescendentsWalker`1.WalkFrameworkElementLogicalThenVisualChildren(FrameworkElement feParent, Boolean hasLogicalChildren)
   at System.Windows.DescendentsWalker`1.IterateChildren(DependencyObject d)
   at System.Windows.DescendentsWalker`1.StartWalk(DependencyObject startNode, Boolean skipStartNode)
   at System.Windows.FrameworkElement.OnPropertyChanged(DependencyPropertyChangedEventArgs e)
   at System.Windows.DependencyObject.NotifyPropertyChange(DependencyPropertyChangedEventArgs args)
   at System.Windows.DependencyObject.UpdateEffectiveValue(EntryIndex entryIndex, DependencyProperty dp, PropertyMetadata metadata, EffectiveValueEntry oldEntry, EffectiveValueEntry& newEntry, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType)
   at System.Windows.DependencyObject.SetValueCommon(DependencyProperty dp, Object value, PropertyMetadata metadata, Boolean coerceWithDeferredReference, Boolean coerceWithCurrentValue, OperationType operationType, Boolean isInternal)
   at System.Windows.FrameworkElement.set_DataContext(Object value)
   at BookAccountApp.View.accounting.uc_payment.Clear() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\accounting\uc_payment.xaml.cs:line 469
   at BookAccountApp.View.accounting.uc_payment.<Btn_save_Click>d__13.MoveNext() in D:\Github\book-account\BookAccountApp\BookAccountApp\View\accounting\uc_payment.xaml.cs:line 242', N'System.Windows.Media.Visual get_Item(Int32)', CAST(N'2023-08-30T17:26:13.7725275' AS DateTime2), 2)
SET IDENTITY_INSERT [dbo].[error] OFF
GO
SET IDENTITY_INSERT [dbo].[flights] ON 

INSERT [dbo].[flights] ([flightId], [airline], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [flightTableId], [fromTableId], [toTableId], [isActive], [balance], [commission_ratio]) VALUES (12, N'السورية', N'', CAST(N'2023-08-27T18:03:03.2432736' AS DateTime2), CAST(N'2023-08-27T23:04:53.7173400' AS DateTime2), 2, 2, 11, 3, 5, 0, NULL, NULL)
INSERT [dbo].[flights] ([flightId], [airline], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [flightTableId], [fromTableId], [toTableId], [isActive], [balance], [commission_ratio]) VALUES (13, N'السورية', N'', CAST(N'2023-08-27T18:03:24.2955996' AS DateTime2), CAST(N'2023-08-27T23:04:46.7863161' AS DateTime2), 2, 2, 12, 4, 6, 1, NULL, NULL)
INSERT [dbo].[flights] ([flightId], [airline], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [flightTableId], [fromTableId], [toTableId], [isActive], [balance], [commission_ratio]) VALUES (15, N'السورية', N'', CAST(N'2023-08-27T22:19:59.5066526' AS DateTime2), CAST(N'2023-08-27T23:04:40.4832095' AS DateTime2), 2, 2, 13, 4, 6, 1, NULL, NULL)
INSERT [dbo].[flights] ([flightId], [airline], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [flightTableId], [fromTableId], [toTableId], [isActive], [balance], [commission_ratio]) VALUES (16, N'شركة1', N'', CAST(N'2023-08-28T01:40:51.6867706' AS DateTime2), CAST(N'2023-08-28T01:40:51.6867706' AS DateTime2), 2, 2, 11, 3, 5, 1, NULL, NULL)
INSERT [dbo].[flights] ([flightId], [airline], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [flightTableId], [fromTableId], [toTableId], [isActive], [balance], [commission_ratio]) VALUES (18, N'hh', N'', CAST(N'2023-08-28T02:08:23.2838269' AS DateTime2), CAST(N'2023-08-28T02:08:23.2838269' AS DateTime2), 2, 2, 11, 3, 5, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[flights] OFF
GO
SET IDENTITY_INSERT [dbo].[flightTable] ON 

INSERT [dbo].[flightTable] ([flightTableId], [name], [isActive], [notes]) VALUES (11, N'دمشق-دبي', 1, N'')
INSERT [dbo].[flightTable] ([flightTableId], [name], [isActive], [notes]) VALUES (12, N'حلب-دمشق', 1, N'')
INSERT [dbo].[flightTable] ([flightTableId], [name], [isActive], [notes]) VALUES (13, N'حلب-الشارقة', 1, N'')
SET IDENTITY_INSERT [dbo].[flightTable] OFF
GO
SET IDENTITY_INSERT [dbo].[fromTable] ON 

INSERT [dbo].[fromTable] ([fromTableId], [name], [isActive], [notes]) VALUES (3, N'دمشق', 1, N'')
INSERT [dbo].[fromTable] ([fromTableId], [name], [isActive], [notes]) VALUES (4, N'حلب', 1, N'')
SET IDENTITY_INSERT [dbo].[fromTable] OFF
GO
SET IDENTITY_INSERT [dbo].[office] ON 

INSERT [dbo].[office] ([officeId], [name], [agentName], [joinDate], [mobile], [address], [userName], [passwordSY], [PasswordSoto], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [commission_ratio]) VALUES (3, N'العيسى', NULL, CAST(N'2020-08-03T00:00:00.0000000' AS DateTime2), N'0959595959', NULL, N'محمد', N'123456', N'654321', N'', CAST(N'2023-08-26T14:14:20.2638351' AS DateTime2), CAST(N'2023-08-27T23:39:49.0389151' AS DateTime2), 2, 2, 0, NULL, NULL)
INSERT [dbo].[office] ([officeId], [name], [agentName], [joinDate], [mobile], [address], [userName], [passwordSY], [PasswordSoto], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [commission_ratio]) VALUES (4, N'السلام', NULL, CAST(N'2021-08-06T00:00:00.0000000' AS DateTime2), N'095959595', NULL, N'سمير', N'987654', N'456789', N'', CAST(N'2023-08-26T14:15:21.1111891' AS DateTime2), CAST(N'2023-08-26T14:15:21.1111891' AS DateTime2), 2, 2, 1, NULL, NULL)
INSERT [dbo].[office] ([officeId], [name], [agentName], [joinDate], [mobile], [address], [userName], [passwordSY], [PasswordSoto], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [commission_ratio]) VALUES (5, N'مكتب2', NULL, CAST(N'2020-08-03T00:00:00.0000000' AS DateTime2), N'51651616', NULL, N'user', N'123', N'321', N'', CAST(N'2023-08-27T23:40:54.3036245' AS DateTime2), CAST(N'2023-08-27T23:40:54.3036245' AS DateTime2), 2, 2, 1, NULL, NULL)
INSERT [dbo].[office] ([officeId], [name], [agentName], [joinDate], [mobile], [address], [userName], [passwordSY], [PasswordSoto], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [commission_ratio]) VALUES (8, N'مكتب13', NULL, CAST(N'2023-08-29T00:00:00.0000000' AS DateTime2), N'43534', NULL, N'user13', N'987', N'789', N'مم', CAST(N'2023-08-28T01:53:03.7639186' AS DateTime2), CAST(N'2023-08-28T01:53:03.7639186' AS DateTime2), 2, 2, 1, NULL, NULL)
INSERT [dbo].[office] ([officeId], [name], [agentName], [joinDate], [mobile], [address], [userName], [passwordSY], [PasswordSoto], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance], [commission_ratio]) VALUES (9, N'kpkp', NULL, CAST(N'2023-08-23T00:00:00.0000000' AS DateTime2), N'6666', NULL, N'777', N'888', N'999', N'pp', CAST(N'2023-08-28T02:09:01.8107890' AS DateTime2), CAST(N'2023-08-28T02:09:01.8107890' AS DateTime2), 2, 2, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[office] OFF
GO
SET IDENTITY_INSERT [dbo].[operations] ON 

INSERT [dbo].[operations] ([operationId], [operation], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [opStatementId], [durationId], [isActive]) VALUES (6, N'عملية 1', N'', CAST(N'2023-08-26T14:17:44.6942508' AS DateTime2), CAST(N'2023-08-28T01:15:23.2776572' AS DateTime2), 2, 2, 5, 5, 1)
INSERT [dbo].[operations] ([operationId], [operation], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [opStatementId], [durationId], [isActive]) VALUES (7, N'عملية2', N'', CAST(N'2023-08-26T14:17:55.2061735' AS DateTime2), CAST(N'2023-08-28T01:22:44.1635630' AS DateTime2), 2, 2, 6, 6, 0)
INSERT [dbo].[operations] ([operationId], [operation], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [opStatementId], [durationId], [isActive]) VALUES (8, N'عملية3', N'', CAST(N'2023-08-26T15:37:23.9814191' AS DateTime2), CAST(N'2023-08-26T15:37:23.9814191' AS DateTime2), 2, 2, 5, 6, 1)
INSERT [dbo].[operations] ([operationId], [operation], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [opStatementId], [durationId], [isActive]) VALUES (9, N'عملية4', N'', CAST(N'2023-08-28T00:52:02.3180061' AS DateTime2), CAST(N'2023-08-28T00:52:02.3180061' AS DateTime2), 2, 2, 5, 6, 1)
SET IDENTITY_INSERT [dbo].[operations] OFF
GO
SET IDENTITY_INSERT [dbo].[passengers] ON 

INSERT [dbo].[passengers] ([passengerId], [name], [lastName], [father], [mother], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance]) VALUES (4, N'احمد', N'محمد', N'محمود', N'هدى', N'', CAST(N'2023-08-26T14:13:14.5341059' AS DateTime2), CAST(N'2023-08-28T00:54:21.4429317' AS DateTime2), 2, 2, 1, NULL)
INSERT [dbo].[passengers] ([passengerId], [name], [lastName], [father], [mother], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance]) VALUES (5, N'سعيد', N'نجار', N'سالم', N'هديل', N'', CAST(N'2023-08-26T14:13:40.7064300' AS DateTime2), CAST(N'2023-08-26T14:13:40.7064300' AS DateTime2), 2, 2, 1, NULL)
INSERT [dbo].[passengers] ([passengerId], [name], [lastName], [father], [mother], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance]) VALUES (6, N'محمود', N'الجابر', N'', N'', N'', CAST(N'2023-08-26T15:35:39.4973402' AS DateTime2), CAST(N'2023-08-28T01:14:25.0144760' AS DateTime2), 2, 2, 0, NULL)
INSERT [dbo].[passengers] ([passengerId], [name], [lastName], [father], [mother], [notes], [createDate], [updateDate], [createUserId], [updateUserId], [isActive], [balance]) VALUES (10, N'مسافر1', NULL, NULL, NULL, N'', CAST(N'2023-08-28T01:40:21.5792901' AS DateTime2), CAST(N'2023-08-28T01:40:21.5792901' AS DateTime2), NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[passengers] OFF
GO
SET IDENTITY_INSERT [dbo].[paySides] ON 

INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (1, N'passenger', N'مسافر', N'd', 1, N'passenger', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (2, N'office', N'مكتب', N'p d', 1, N'office', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (3, N'soto', N'نظام السوتو', N'p', 1, N'soto', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (4, N'syr', N'نظام السوري', N'p', 1, N'syr', CAST(0.000 AS Decimal(20, 3)))
INSERT [dbo].[paySides] ([paysideId], [side], [sideAr], [notes], [isActive], [code], [balance]) VALUES (5, N'other', N'جهة أخرى', N'p d', 1, N'other', CAST(0.000 AS Decimal(20, 3)))
SET IDENTITY_INSERT [dbo].[paySides] OFF
GO
SET IDENTITY_INSERT [dbo].[serviceData] ON 

INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (29, N'SY-000001', NULL, NULL, N'6266', NULL, 3, CAST(N'2023-09-02T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, CAST(100.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-02T13:05:00.7197038' AS DateTime2), CAST(N'2023-09-02T13:05:00.7197038' AS DateTime2), 2, 2, 4, 12, NULL, N'syr', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(100.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(100.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL)
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (30, N'SY-000002', NULL, NULL, N'61651616', NULL, NULL, CAST(N'2023-09-09T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, CAST(200.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-02T13:05:31.6766381' AS DateTime2), CAST(N'2023-09-02T13:05:31.6766381' AS DateTime2), 2, 2, 6, 15, NULL, N'syr', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL)
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (31, N'SOTO-000001', NULL, NULL, N'66616', NULL, 4, NULL, NULL, NULL, NULL, CAST(200.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-02T13:05:51.7475112' AS DateTime2), CAST(N'2023-09-02T13:05:51.7475112' AS DateTime2), 2, 2, 5, 13, 6, N'soto', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (32, N'SOTO-000002', NULL, NULL, N'161616', NULL, NULL, NULL, NULL, NULL, NULL, CAST(300.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-02T13:06:09.3287534' AS DateTime2), CAST(N'2023-09-02T13:06:09.3287534' AS DateTime2), 2, 2, 4, 12, 6, N'soto', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(300.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (33, N'SY-000003', NULL, NULL, N'6561', NULL, 3, CAST(N'2023-09-09T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, CAST(200.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-02T13:06:44.8378566' AS DateTime2), CAST(N'2023-09-02T13:06:44.8378566' AS DateTime2), 2, 2, 5, 12, NULL, N'syr', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL)
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (1029, N'SY-000004', NULL, NULL, N'132132', NULL, 3, CAST(N'2023-09-05T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, CAST(200.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-05T16:11:24.4900000' AS DateTime2), CAST(N'2023-09-05T16:11:24.4900000' AS DateTime2), 2, 2, 4, 12, NULL, N'syr', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(200.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), NULL)
INSERT [dbo].[serviceData] ([serviceId], [serviceNum], [type], [passenger], [ticketNum], [airline], [officeId], [serviceDate], [pnr], [ticketvalueSP], [ticketvalueDollar], [total], [priceBeforTax], [commitionRatio], [commitionValue], [cost], [saleValue], [paid], [profit], [notes], [state], [createDate], [updateDate], [createUserId], [updateUserId], [passengerId], [flightId], [operationId], [systemType], [isActive], [system_commission_value], [system_commission_ratio], [office_commission_value], [office_commission_ratio], [company_commission_value], [company_commission_ratio], [totalnet], [passengerPaid], [passengerUnpaid], [officePaid], [officeUnpaid], [airlinePaid], [airlineUnpaid], [systemPaid], [systemUnpaid], [exchangeId]) VALUES (1030, N'SOTO-000003', NULL, NULL, N'62962', NULL, 4, NULL, NULL, NULL, NULL, CAST(300.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', NULL, CAST(N'2023-09-05T16:42:17.3515277' AS DateTime2), CAST(N'2023-09-05T16:42:17.3515277' AS DateTime2), 2, 2, 6, 13, 7, N'soto', 1, CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(0.000 AS Decimal(20, 3)), CAST(300.000 AS Decimal(20, 3)), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
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
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (58, N'SupClouds', 1, 1, NULL, 1)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (59, N'Kuwait', 1, 1, NULL, 2)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (60, N'admin@SupClouds.com', 1, 1, NULL, 3)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (61, N'+965-998484189', 1, 1, NULL, 4)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (62, N'+965--', 1, 1, NULL, 5)
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (63, N'+965--', 1, 1, NULL, 6)
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
INSERT [dbo].[setValues] ([valId], [value], [isDefault], [isSystem], [notes], [settingId]) VALUES (87, N'1', 1, 1, N'0', 29)
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
SET IDENTITY_INSERT [dbo].[setValues] OFF
GO
SET IDENTITY_INSERT [dbo].[statementsTable] ON 

INSERT [dbo].[statementsTable] ([opStatementId], [name], [isActive], [notes]) VALUES (5, N'بيان 1', 1, N'')
INSERT [dbo].[statementsTable] ([opStatementId], [name], [isActive], [notes]) VALUES (6, N'بيان2', 1, N'')
SET IDENTITY_INSERT [dbo].[statementsTable] OFF
GO
SET IDENTITY_INSERT [dbo].[toTable] ON 

INSERT [dbo].[toTable] ([toTableId], [name], [isActive], [notes]) VALUES (5, N'دبي', 1, N'')
INSERT [dbo].[toTable] ([toTableId], [name], [isActive], [notes]) VALUES (6, N'دمشق', 1, N'')
INSERT [dbo].[toTable] ([toTableId], [name], [isActive], [notes]) VALUES (7, N'الشارقة', 1, N'')
SET IDENTITY_INSERT [dbo].[toTable] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (1, N'administrator', N'administrator', N'Support', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2023-07-28T15:51:31.4860429' AS DateTime2), N'8ea60f80800198548a9a81aa2e4a6115', N'ad', N'57440ff6b80f068efd50410ea24fd593.png', N'', CAST(0.000 AS Decimal(20, 3)), 1, 2, 1, N'Us-Admin', NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (2, N'admin', N'admin', N'admin', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2023-09-05T17:08:43.5276781' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'ad', N'c37858823db0093766eee74d8ee1f1e5.png', N'', CAST(0.000 AS Decimal(20, 3)), 1, 2, 1, N'Us-adminuser', NULL, NULL, NULL, NULL, 1, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (3, N'sysagent', N'sysagent', N'sysagent', NULL, N'', NULL, N'+966-095550226', NULL, N'', NULL, NULL, CAST(N'2021-12-08T12:25:13.5504988' AS DateTime2), N'e4959b2ae3b5256076a7b5e99f88b8ba', N'ag', NULL, N'', CAST(0.000 AS Decimal(20, 3)), 1, 1, 1, N'aaa', NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (4, N'ahmad', N'ahmad', N'ahmad', NULL, N'', NULL, N'+963-944444444', NULL, N'', NULL, CAST(N'2023-07-10T14:01:50.3551343' AS DateTime2), CAST(N'2023-07-28T15:50:36.0939643' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'us', N'd2ec5f1ed83abfca0dfec76506b696b3.png', N'', CAST(0.000 AS Decimal(20, 3)), 2, 2, 1, N'gbm', NULL, NULL, NULL, NULL, NULL, 9)
INSERT [dbo].[users] ([userId], [name], [AccountName], [lastName], [company], [email], [phone], [mobile], [fax], [address], [agentLevel], [createDate], [updateDate], [password], [type], [image], [notes], [balance], [createUserId], [updateUserId], [isActive], [code], [isAdmin], [groupId], [balanceType], [job], [isOnline], [countryId]) VALUES (6, N'test', N'test', N'test', NULL, N'', NULL, N'+963-599994949', NULL, N'', NULL, CAST(N'2023-07-28T16:19:02.6861123' AS DateTime2), CAST(N'2023-07-28T16:19:29.0893130' AS DateTime2), N'9b43a5e653134fc8350ca9944eadaf2f', N'us', NULL, N'', CAST(0.000 AS Decimal(20, 3)), 2, 2, 1, N'jnm', NULL, NULL, NULL, NULL, 1, 9)
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

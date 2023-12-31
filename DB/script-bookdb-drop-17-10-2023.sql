USE [bookdb]
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
/****** Object:  Table [dbo].[userSetValues]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[userSetValues]') AND type in (N'U'))
DROP TABLE [dbo].[userSetValues]
GO
/****** Object:  Table [dbo].[users]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[users]') AND type in (N'U'))
DROP TABLE [dbo].[users]
GO
/****** Object:  Table [dbo].[toTable]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[toTable]') AND type in (N'U'))
DROP TABLE [dbo].[toTable]
GO
/****** Object:  Table [dbo].[systems]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[systems]') AND type in (N'U'))
DROP TABLE [dbo].[systems]
GO
/****** Object:  Table [dbo].[statementsTable]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[statementsTable]') AND type in (N'U'))
DROP TABLE [dbo].[statementsTable]
GO
/****** Object:  Table [dbo].[setValues]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setValues]') AND type in (N'U'))
DROP TABLE [dbo].[setValues]
GO
/****** Object:  Table [dbo].[setting]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setting]') AND type in (N'U'))
DROP TABLE [dbo].[setting]
GO
/****** Object:  Table [dbo].[serviceDataFiles]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceDataFiles]') AND type in (N'U'))
DROP TABLE [dbo].[serviceDataFiles]
GO
/****** Object:  Table [dbo].[serviceData]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[serviceData]') AND type in (N'U'))
DROP TABLE [dbo].[serviceData]
GO
/****** Object:  Table [dbo].[paySides]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[paySides]') AND type in (N'U'))
DROP TABLE [dbo].[paySides]
GO
/****** Object:  Table [dbo].[payOp]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[payOp]') AND type in (N'U'))
DROP TABLE [dbo].[payOp]
GO
/****** Object:  Table [dbo].[passengers]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[passengers]') AND type in (N'U'))
DROP TABLE [dbo].[passengers]
GO
/****** Object:  Table [dbo].[passengerFiles]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[passengerFiles]') AND type in (N'U'))
DROP TABLE [dbo].[passengerFiles]
GO
/****** Object:  Table [dbo].[operations]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[operations]') AND type in (N'U'))
DROP TABLE [dbo].[operations]
GO
/****** Object:  Table [dbo].[officeSystem]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[officeSystem]') AND type in (N'U'))
DROP TABLE [dbo].[officeSystem]
GO
/****** Object:  Table [dbo].[officeFiles]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[officeFiles]') AND type in (N'U'))
DROP TABLE [dbo].[officeFiles]
GO
/****** Object:  Table [dbo].[office]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[office]') AND type in (N'U'))
DROP TABLE [dbo].[office]
GO
/****** Object:  Table [dbo].[fromTable]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fromTable]') AND type in (N'U'))
DROP TABLE [dbo].[fromTable]
GO
/****** Object:  Table [dbo].[flightTable]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[flightTable]') AND type in (N'U'))
DROP TABLE [dbo].[flightTable]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[flights]') AND type in (N'U'))
DROP TABLE [dbo].[flights]
GO
/****** Object:  Table [dbo].[exchange]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[exchange]') AND type in (N'U'))
DROP TABLE [dbo].[exchange]
GO
/****** Object:  Table [dbo].[error]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[error]') AND type in (N'U'))
DROP TABLE [dbo].[error]
GO
/****** Object:  Table [dbo].[durationsTable]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[durationsTable]') AND type in (N'U'))
DROP TABLE [dbo].[durationsTable]
GO
/****** Object:  Table [dbo].[customers]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[customers]') AND type in (N'U'))
DROP TABLE [dbo].[customers]
GO
/****** Object:  Table [dbo].[countriesCodes]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[countriesCodes]') AND type in (N'U'))
DROP TABLE [dbo].[countriesCodes]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cities]') AND type in (N'U'))
DROP TABLE [dbo].[cities]
GO
/****** Object:  Table [dbo].[airlines]    Script Date: 17/10/2023 12:40:11 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[airlines]') AND type in (N'U'))
DROP TABLE [dbo].[airlines]
GO

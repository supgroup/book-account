USE [bookdb]
GO
DELETE FROM [dbo].[payOp]
     
GO
DELETE FROM [dbo].[error]
     
GO
UPDATE [dbo].[serviceData]
   SET  
       [state] = 'draft'
       
 
GO
UPDATE [dbo].[paySides]
   SET  
      [balance] = 0
 
GO
UPDATE [dbo].[office]
   SET 
      [balance] =0  
GO
DELETE FROM [dbo].[customers]
      
GO
DELETE FROM [dbo].[operations]
    
GO

DELETE FROM [dbo].[durationsTable]
     
GO
DELETE FROM [dbo].[serviceData]
      
GO

DELETE FROM [dbo].[exchange]
     
GO
DELETE FROM [dbo].[flights]
     
GO
DELETE FROM [dbo].[flightTable]
     
GO
DELETE FROM [dbo].[fromTable]
    
GO
DELETE FROM [dbo].[officeSystem]
      
GO

DELETE FROM [dbo].[office]
     
GO
DELETE FROM [dbo].[passengerFiles]
     
GO
DELETE FROM [dbo].[passengers]
     
GO

DELETE FROM [dbo].[serviceDataFiles]
    
GO

DELETE FROM [dbo].[statementsTable]
     
GO
DELETE FROM [dbo].[systems]
      
GO
DELETE FROM [dbo].[toTable]
      
GO
DELETE FROM [dbo].[users]
      WHERE AccountName<>'admin' and AccountName<>'administrator'
GO

DELETE FROM [dbo].[airlines]
     
GO







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



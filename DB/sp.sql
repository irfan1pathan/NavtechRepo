USE [navtech]
GO
/****** Object:  StoredProcedure [dbo].[AddOrder]    Script Date: 5/2/2020 7:08:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AddOrder]
@jsondata nvarchar(max)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			Declare  @prdid      nvarchar(max)
			        ,@userid     nvarchar(max)
					,@selectedqty nvarchar(max)
			
			set @userid       =JSON_VALUE(@jsondata,'$.UserId')
			
		Select 
		@userid as UserId
		,pid
		,qty
		into #tbl
		from openjson(@jsondata,'$.ProdDetails')
		WITH
		(
		 pid nvarchar(max) '$.pid'
		,qty nvarchar(max) '$.qty'
		)

		Delete A
		FROM #tbl A
		JOIN [Order] B on A.UserId=B.UserId and A.pid=B.PrdId and A.qty=B.SelectedQuantity

		if((Select count(*) from #tbl)>0)
		BEGIN
			Merge [Order] AS T
			Using (Select * from #tbl) as S
			on T.UserId=S.UserId AND T.SelectedQuantity=S.qty and T.PrdId=S.pid
			WHEN NOT MATCHED 
			THEN
			Insert(
				 PrdId
				,UserId
				,SelectedQuantity
				,OrderStatus
				) 
				values(
				  S.pid
				 ,S.UserId
				 ,S.qty
				,1
				);
		END	
			SELECT 1 AS DBResult
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		Select 0 as DBResult
	END CATCH
END

/*
declare @jsondata nvarchar(max)='{"UserId":1,"ProdDetails":[{"pid":"1","qty":"3"},{"pid":"2","qty":"3"}]}'
exec AddOrder @jsondata
*/


GO
/****** Object:  StoredProcedure [dbo].[sp_productDetails]    Script Date: 5/2/2020 7:08:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_productDetails]  
@jsondata nvarchar(max)  
AS  
BEGIN  
    declare @sql           nvarchar(max)  
        ,@UserName      int  
     ,@IsAdmin       nvarchar(max)  
     ,@mainqry       nvarchar(max)='{}'  
     ,@data          nvarchar(max)  
 set @UserName=Json_VALUE(@jsondata,'$.UserId');  
 set @IsAdmin=(Select Role from Users where UId=@UserName) 
  Select   
   A.OrderId  
  ,C.UId  
  ,C.UserName  
  ,C.Address  
  ,C.Country  
  ,C.State  
  ,C.City  
  ,C.Zip  
  ,B.ProductName  
  ,B.Productprice  
  ,A.SelectedQuantity  
  ,D.StatusName  
  into #maindata  
  from [Order] A  
  Left JOIN Products B on A.PrdId=B.PrdId  
  Left JOIN Users C on A.UserId=C.UId  
  Left JOIN OrderStatus D on A.OrderStatus=D.StatusId where A.IsDelete=0  
    
  drop table if exists #filtereddata   
  Select * into #filtereddata from #maindata where 1=2  
  
  --select Json_Query(@jsondata)  
  
  set @sql='  
  Insert into #filtereddata  
  select * from #maindata  
  '  
  if (@IsAdmin!='Admin')  
  BEGIN  
  set @sql=@sql+'where UId='+Cast(@UserName as nvarchar(max))+''  
  END  
  print @sql
  exec (@sql)  
  
  set @data=(select * from #filtereddata for json path)  
  
  if(@data=null)  
  BEGIN  
     Select '{tableData:[]}'  
  END  
  ELSE  
  BEGIN  
   Select Replace(Replace(JSON_Modify(@MainQry,'append $.tableData',Json_Query(@data)),'[[','['),']]',']') as result  
  END  
  
END

/*
declare @jsondata nvarchar(max)='{"UserId":2}'
exec sp_productDetails @jsondata
*/
GO

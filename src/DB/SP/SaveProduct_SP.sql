USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[SaveSubCategory_SP]    Script Date: 12/01/2015 16:29:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer A. Shinde
-- Create date: 01 Dec 2015
-- Description:	Save Product Details
-- =============================================
Create PROCEDURE [dbo].[SaveProduct_SP] ---0,4,'hghg',1,0
	@ProductId int,
	@CategoryID int,
	@SubCategoryID int,
	@ProductName nvarchar(max),
	@ImagePath nvarchar(max),
	@IsActive bit,
	@IsDeleted bit
AS
Declare @msg nvarchar(50)
BEGIN
	IF(@ProductId=0)
		BEGIN
			IF NOT EXISTS (SELECT ProductID FROM Product Where ProductName = @ProductName)--Check Whether given name Already exist or not
				BEGIN	
					IF NOT EXISTS (SELECT ProductName FROM Product Where ProductName = @ProductName)--Check Whether given name Already exist or not
				   BEGIN			
					INSERT INTO Product (ProductName,CategoryID,SubCategoryID,ImagePath, CreatedDate, IsActive, IsDeleted)
					VALUES (@ProductName,@CategoryID,@SubCategoryID,@ImagePath,GETDATE(),@IsActive, 0)
					Set @msg='Product Save Sucessfully!!!'
				   END
				   END
		      ELSE
				BEGIN
				 Set @msg='Product Name Already Exists'
				 
				END
		 END
		
		
	ELSE
		BEGIN
			UPDATE Product SET ProductName=@ProductName,CategoryID=@CategoryID,SubCategoryID=@SubCategoryID,ImagePath=@ImagePath, UpdatedDate = GETDATE(), 
			IsActive = @IsActive, IsDeleted = @IsDeleted
			WHERE ProductID = @ProductId
			Set @msg='Product Update Sucessfully!!!'
		END
		Select @msg
END

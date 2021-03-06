USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[SaveSubCategory_SP]    Script Date: 12/01/2015 12:57:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SaveSubCategory_SP] ---0,4,'hghg',1,0
	@SubCatID int,
	@CatID int,
	@SubCategoryName nvarchar(max),
	@IsActive bit,
	@IsDeleted bit
AS
Declare @msg nvarchar(50)
BEGIN
	IF(@SubCatID=0)
		BEGIN
			IF NOT EXISTS (SELECT SubCategoryID FROM SubCategory Where SubCategoryName = @SubCategoryName)--Check Whether given name Already exist or not
				BEGIN	
					IF NOT EXISTS (SELECT SubCategoryName FROM SubCategory Where SubCategoryName = @SubCategoryName)--Check Whether given name Already exist or not
				   BEGIN			
					INSERT INTO SubCategory (SubCategoryName,CategoryID, CreatedDate, IsActive, IsDeleted)
					VALUES (@SubCategoryName,@CatID,GETDATE(),@IsActive, 0)
					Set @msg='Sub Category Save Sucessfully!!!'
				   END
				  END
		      ELSE
				BEGIN
				 Set @msg='Sub Category Name Already Exists'
				END
		 END
		
		
	ELSE
		BEGIN
			UPDATE SubCategory SET SubCategoryName=@SubCategoryName,CategoryID=@CatID, UpdatedDate = GETDATE(), 
			IsActive = @IsActive, IsDeleted = @IsDeleted
			WHERE SubCategoryID = @SubCatID
			Set @msg='Sub Category Update Sucessfully!!!'
		END
		Select @msg
END

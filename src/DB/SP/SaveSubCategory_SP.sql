USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[saveCategory_SP]    Script Date: 11/30/2015 19:09:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[SaveSubCategory_SP]
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
					INSERT INTO SubCategory (SubCategoryName,CategoryID, CreatedDate, IsActive, IsDeleted)
					VALUES (@SubCategoryName,@CatID,GETDATE(),@IsActive, 0)
					Set @msg='Sub Category Save Sucessfully!!!'
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

USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[saveCategory_SP]    Script Date: 11/30/2015 16:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[saveCategory_SP]
	@CatID int,
	@CategoryName nvarchar(max),
	@IsActive bit,
	@IsDeleted bit
AS
Declare @msg nvarchar(50)
BEGIN
	IF(@CatID=0)
		BEGIN
			IF NOT EXISTS (SELECT CategoryID FROM Category Where CategoryName = @CategoryName)--Check Whether given name Already exist or not
				BEGIN					
					INSERT INTO Category (CategoryName, CreatedDate, IsActive, IsDeleted)
					VALUES (@CategoryName,GETDATE(),@IsActive, 0)
					Set @msg='Category Save Sucessfully!!!'
				END
		END
	ELSE
		BEGIN
			UPDATE Category SET CategoryName=@CategoryName, UpdatedDate = GETDATE(), 
			IsActive = @IsActive, IsDeleted = @IsDeleted
			WHERE CategoryID = @CatID
			Set @msg='Category Update Sucessfully!!!'
		END
		Select @msg
END

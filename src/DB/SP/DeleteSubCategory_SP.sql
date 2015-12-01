USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory_SP]    Script Date: 12/01/2015 12:38:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 1 Dec 2015
-- Description:	Delete Sub Category
-- =============================================
Create PROCEDURE [dbo].[DeleteSubCategory_SP] 
	(
	@SubCategoryID int,
	@UpdatedDate nvarchar(50) ) 
	
AS
DECLARE @msg NVARCHAR(50)
BEGIN
	UPDATE SubCategory SET IsActive=0  , IsDeleted=1 ,UpdatedDate=@UpdatedDate
	WHERE SubCategoryID= @SubCategoryID
	if (@@ROWCOUNT=1)
		BEGIN 
		       
		       SET @msg = 'Sub Category Deleted Sucessfully!!!'
		END
	else
		BEGIN
			SET @msg = 'Sub Category NOT Deleted'
		END
	SELECT @msg
END
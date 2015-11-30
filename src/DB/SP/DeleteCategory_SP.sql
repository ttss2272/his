
/****** Object:  StoredProcedure [dbo].[DeleteBranch_SP]    Script Date: 11/30/2015 17:15:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 30 Nov 2015
-- Description:	Delete Category
-- =============================================
Create PROCEDURE [dbo].[DeleteCategory_SP] 
	(
	@CategoryID int,
	@UpdatedDate nvarchar(50) ) 
	
AS
DECLARE @msg NVARCHAR(50)
BEGIN
	UPDATE Category SET IsActive=0  , IsDeleted=1 ,UpdatedDate=@UpdatedDate
	WHERE CategoryID= @CategoryID
	if (@@ROWCOUNT=1)
		BEGIN 
		       
		       SET @msg = 'Category Deleted Sucessfully!!!'
		END
	else
		BEGIN
			SET @msg = 'Category NOT Deleted'
		END
	SELECT @msg
END

/****** Object:  StoredProcedure [dbo].[BindInstituteName_SP]    Script Date: 11/30/2015 18:35:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 30 Nov 2015
-- Description:	Bind Category Name
-- =============================================
alter PROCEDURE [dbo].[BindCategoryName_SP] 
AS
BEGIN

	Select 0 AS CategoryID ,'Select' AS CategoryName
	Union
	SELECT CategoryID,CategoryName  from Category
	WHERE IsDeleted<>'True' AND IsActive=1
END

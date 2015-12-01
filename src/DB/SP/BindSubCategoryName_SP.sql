USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[BindCategoryName_SP]    Script Date: 12/01/2015 15:42:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 01 Dec 2015
-- Description:	Bind Sub Category Name
-- =============================================
Create PROCEDURE [dbo].[BindSubCategoryName_SP] 
@CategoryID int
AS
BEGIN

	Select -1 AS SubCategoryID ,'Select' AS SubCategoryName
	Union
	SELECT SubCategoryID,SubCategoryName  from SubCategory
	WHERE CategoryID=@CategoryID AND IsDeleted<>'True' AND IsActive=1
END

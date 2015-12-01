USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[BindCategory_SP]    Script Date: 12/01/2015 10:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 01-12-2015
-- Description:	Get Details To Grid view
-- =============================================
Alter PROCEDURE [dbo].[BindCategoryForSub_SP] --0,0,''
(
	@SubCategoryID int,
	@CategoryID int,
	@SubCategoryName nvarchar(50)
) 
	
AS
BEGIN
SELECT SubCategoryID,SubCat.CategoryID,cat.CategoryName,SubCategoryName  from SubCategory SubCat
   inner join Category cat 
   on cat.CategoryID=SubCat.CategoryID

	WHERE SubCategoryID LIKE CASE WHEN @SubCategoryID<>0 THEN @SubCategoryID ELSE CONVERT (NVARCHAR(50),SubCategoryID) END
	AND SubCat.CategoryID LIKE CASE WHEN @CategoryID<>0 THEN @CategoryID ELSE CONVERT (NVARCHAR(50),SubCat.CategoryID ) END
	AND SubCategoryName LIKE CASE WHEN @SubCategoryName<>'' THEN '%'+@SubCategoryName+'%' ELSE CONVERT (NVARCHAR(50),SubCategoryName)END
	AND SubCat.IsDeleted<>'True' 
	 
END
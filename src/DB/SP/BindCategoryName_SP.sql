USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[BindCategoryName_SP]    Script Date: 12/01/2015 11:50:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 30 Nov 2015
-- Description:	Bind Category Name
-- =============================================
ALTER PROCEDURE [dbo].[BindCategoryName_SP] 
AS
BEGIN

	Select -1 AS CategoryID ,'Select' AS CategoryName
	Union
	SELECT CategoryID,CategoryName  from Category
	WHERE IsDeleted<>'True' AND IsActive=1
END

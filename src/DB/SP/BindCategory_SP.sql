USE [WPFImage]
GO
/****** Object:  StoredProcedure [dbo].[BindCategory_SP]    Script Date: 12/01/2015 10:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sameer Shinde
-- Create date: 30-11-2015
-- Description:	Get Details To bind Drop Down
-- =============================================
ALTER PROCEDURE [dbo].[BindCategory_SP] --0,''
(
	@CategoryID int,
	@CategoryName nvarchar(50)
) 
	
AS
BEGIN
SELECT CategoryID,CategoryName  from Category

	WHERE CategoryID LIKE CASE WHEN @CategoryID<>0 THEN @CategoryID ELSE CONVERT (NVARCHAR(50),CategoryID) END
	AND CategoryName LIKE CASE WHEN @CategoryName<>'' THEN '%'+@CategoryName+'%' ELSE CONVERT (NVARCHAR(50),CategoryName)END
	AND IsDeleted<>'True' 
	 
END
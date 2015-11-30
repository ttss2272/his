/****** Object:  StoredProcedure [dbo].[BindBatch_SP]    Script Date: 11/30/2015 15:40:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Pravin
-- Create date: 05-11-2015
-- Description:	Get Details To bind Drop Down
-- =============================================
alter PROCEDURE [dbo].[BindCategory_SP] --0,''
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

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetBySearch
	@search nvarchar(max)=null
AS
BEGIN
	SELECT *from [dbo].[Tbl_Product] P
	left join [dbo].[Tbl_Category] C on p.CategoryId=c.CategoryId
	where
	P.ProductName LIKE CASE WHEN @search is not null then  '%'+@search+'%' else P.ProductName end
	OR
	C.CategoryName LIKE CASE WHEN @search is not null then  '%'+@search+'%' else C.CategoryName end
END
GO

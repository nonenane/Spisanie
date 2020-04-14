USE [dbase1]
GO
/****** Object:  StoredProcedure [Processing].[UpdateProgConfig]    Script Date: 14.04.2020 0:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<S G Y>
-- Create date: <14.04.2020>
-- Description:	<Получение цен товара с учётом наценки>
-- =============================================
CREATE PROCEDURE [spisanie].[getPriceTovarWithPrcn]
	@id_tovar int ,
	@date date
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @prnc numeric(16,2) = 1, @rcena numeric(16,2)
select @prnc = CAST(LEFT(value, CHARINDEX(',', value) - 1) + '.' + SUBSTRING(value,(CHARINDEX(',',value)+1),2) AS numeric(16,2)) 
from dbo.prog_config where id_prog = 106 and id_value='prnc'

select TOP(1) @rcena = rcena from dbo.s_rcena where id_tovar = @id_tovar and tdate_n<=@date order by tdate_n desc

select  @rcena * (100 - @prnc)/100 as minPrice, @rcena * (100 + @prnc)/100 as maxPrice, @prnc as prnc

END

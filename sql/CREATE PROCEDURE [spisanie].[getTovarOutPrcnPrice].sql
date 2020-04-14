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
-- Description:	<Получение товаров которые не укладываются в условия по проценту>
-- =============================================
CREATE PROCEDURE [spisanie].[getTovarOutPrcnPrice]	
	@date date
AS
BEGIN
	SET NOCOUNT ON;


DECLARE @prnc numeric(16,2) = 1, @rcena numeric(16,2)
select @prnc = CAST(LEFT(value, CHARINDEX(',', value) - 1) + '.' + SUBSTRING(value,(CHARINDEX(',',value)+1),2) AS numeric(16,2)) 
from dbo.prog_config where id_prog = 106 and id_value='prnc'

DECLARE @table table (dprihod date,ttn varchar(150),id_operand int,zcena numeric(16,4),realRcena  numeric(16,2),id_otdel int ,id_tovar int )

BEGIN --Приход
INSERT INTO @table
select a.dprihod,a.ttn,a.id_operand,a.zcena,a.realRcena,a.id_otdel,a.id_tovar
from(
select 
	 a.id
	,a.dprihod
	,a.ttn
	,a.id_operand
	,p.zcena
	,p.rcena
	,r.rcena as realRcena
	,r.rcena * (100 - @prnc)/100 as minPrice 
	,r.rcena * (100 + @prnc)/100 as maxPrice	
	,p.id_otdel
	,p.id_tovar
from 
	dbo.j_allprihod a
		inner join dbo.j_prihod p on p.id_allprihod = a.id
		left join dbo.s_rcena r on r.id_tovar = p.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = p.id_tovar and rr.tdate_n<=a.dprihod order by rr.tdate_n desc)
where 
	InventSpis = 1 and dprihod = @date) as a
where 
(a.minPrice>a.rcena OR a.rcena>a.maxPrice) or (a.minPrice>a.zcena OR a.zcena>a.maxPrice)
END

BEGIN --Отгрзка
INSERT INTO @table
select  a.dprihod,a.ttn,a.id_operand,a.zcena,a.realRcena,a.id_otdel,a.id_tovar 
from(
select 
	 a.id
	,a.dprihod
	,a.ttn
	,a.id_operand
	,p.zcena
	,p.rcena
	,r.rcena as realRcena
	,r.rcena * (100 - @prnc)/100 as minPrice 
	,r.rcena * (100 + @prnc)/100 as maxPrice	
	,p.id_otdel
	,p.id_tovar
from 
	dbo.j_allprihod a
		inner join dbo.j_otgruz p on p.id_allprihod = a.id
		left join dbo.s_rcena r on r.id_tovar = p.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = p.id_tovar and rr.tdate_n<=a.dprihod order by rr.tdate_n desc)

where 
	InventSpis = 1 and dprihod = @date ) as a
where 
--(a.minPrice<=a.rcena and a.rcena<=a.maxPrice) or (a.minPrice<=a.zcena and a.zcena<=a.maxPrice)
(a.minPrice>a.rcena OR a.rcena>a.maxPrice) or (a.minPrice>a.zcena OR a.zcena>a.maxPrice)
END

BEGIN --возврат касс
INSERT INTO @table
select a.dprihod,a.ttn,a.id_operand,a.rcena,a.realRcena,a.id_otdel,a.id_tovar
from(
select 
	 a.id
	,a.dprihod
	,a.ttn
	,a.id_operand	
	,p.rcena
	,r.rcena as realRcena
	,r.rcena * (100 - @prnc)/100 as minPrice 
	,r.rcena * (100 + @prnc)/100 as maxPrice	
	,p.id_otdel
	,p.id_tovar
from 
	dbo.j_allprihod a
		inner join dbo.j_vozvkass p on p.id_allprihod = a.id
		left join dbo.s_rcena r on r.id_tovar = p.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = p.id_tovar and rr.tdate_n<=a.dprihod order by rr.tdate_n desc)
where 
	InventSpis = 1 and dprihod = @date) as a
where 
	(a.minPrice>a.rcena OR a.rcena>a.maxPrice)
END

BEGIN --Списание
INSERT INTO @table
select a.dprihod,a.ttn,a.id_operand,a.zcena,a.realRcena,a.id_otdel,a.id_tovar
from(
select 
	 a.id
	,a.dprihod
	,a.ttn
	,a.id_operand
	,p.zcena
	,p.rcena
	,r.rcena as realRcena
	,r.rcena * (100 - @prnc)/100 as minPrice 
	,r.rcena * (100 + @prnc)/100 as maxPrice	
	,p.id_otdel
	,p.id_tovar
from 
	dbo.j_allprihod a
		inner join dbo.j_spis p on p.id_allprihod = a.id
		left join dbo.s_rcena r on r.id_tovar = p.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = p.id_tovar and rr.tdate_n<=a.dprihod order by rr.tdate_n desc)
where 
	InventSpis = 1 and dprihod = @date) as a
where 
	(a.minPrice>a.rcena OR a.rcena>a.maxPrice)
END

BEGIN --Переоценка
INSERT INTO @table
select a.dprihod,a.ttn,a.id_operand,a.zcena,a.realRcena,a.id_otdel,a.id_tovar
from(
select 
	 a.id
	,a.dprihod
	,a.ttn
	,a.id_operand
	,p.zcena
	,p.rcena
	,p.bcena
	,r.rcena as realRcena
	,r.rcena * (100 - @prnc)/100 as minPrice 
	,r.rcena * (100 + @prnc)/100 as maxPrice	
	,p.id_otdel
	,p.id_tovar
from 
	dbo.j_allprihod a
		inner join dbo.j_pereoc p on p.id_allprihod = a.id
		left join dbo.s_rcena r on r.id_tovar = p.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = p.id_tovar and rr.tdate_n<=a.dprihod order by rr.tdate_n desc)
where 
	InventSpis = 1 and dprihod = @date) as a
where 
(a.minPrice>a.rcena OR a.rcena>a.maxPrice) or (a.minPrice>a.bcena OR a.bcena>a.maxPrice)
END


select 
	ltrim(rtrim(d.name)) as nameDeps,	
	ltrim(rtrim(t.ean))  as ean,
	ltrim(rtrim(tt.cName+' '+n.cname)) as nameTovar,
	p.zcena,
	p.realRcena,
	p.ttn,
	o.name as nameOperand,
	@prnc as prnc
from 
	@table p
		left join dbo.departments d on d.id = p.id_otdel
		left join dbo.s_tovar t on t.id = p.id_tovar
		left join dbo.s_ntovar n on n.id_tovar = p.id_tovar and n.id = (select top(1) nn.id from dbo.s_ntovar nn where nn.id_tovar = p.id_tovar and nn.tdate_n<=p.dprihod order by nn.tdate_n desc)
		left join dbo.s_TypeTovar tt on tt.id = n.ntypetovar
		left join dbo.s_operand o on o.id = p.id_operand

END

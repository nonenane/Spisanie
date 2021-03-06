USE [dbase1]
GO
/****** Object:  StoredProcedure [spisanie].[GetAdvige]    Script Date: 14.04.2020 15:23:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Mito
-- Create date: 11.08.10 12:31
-- Description:	процедура получения содержимого 
-- накладных для программы spisanie
-- =============================================
ALTER PROCEDURE [spisanie].[GetAdvige] 
@id int,
@type int
with recompile
AS
BEGIN
SET NOCOUNT ON

DECLARE @dPrihod date 
	select @dPrihod = dprihod from dbo.j_allprihod where id = @id

DECLARE @prnc numeric(16,2) = 1
select @prnc = CAST(LEFT(value, CHARINDEX(',', value) - 1) + '.' + SUBSTRING(value,(CHARINDEX(',',value)+1),2) AS numeric(16,2)) 
from dbo.prog_config where id_prog = 106 and id_value='prnc'


if @type=1
begin
	select 
			a.id
			,a.id_tovar as kodt
			,b.ean
			,ltrim(rtrim(d.cName)+' ' + rtrim(b.cname)) as name
			,substring(a.npp,charindex('ПП',a.npp)+2,LEN(a.npp)-charindex('ПП',a.npp))as npp
			,a.zcena as cena
			,a.zcena as oldcena
			,convert(int,round(case a.zcena when 0 then -100 else a.rcena*100/a.zcena-100 end,0)) as nadb
			,a.rcena
			,a.rcena as oldrcena
			,a.netto
			,a.nds
			,a.nds as oldnds
			,a.zcena*a.netto as zsum
			,a.rcena*a.netto as rsum
			,r.rcena as realRcena
			,r.rcena * (100 - @prnc)/100 as minPrice 
			,r.rcena * (100 + @prnc)/100 as maxPrice
			,@prnc as prnc
	from
			dbo.j_prihod a	left join dbo.v_tovar b on a.id_tovar=b.id_tovar
							left join dbo.s_nds c on b.id_nds=c.id
							left join dbo.s_TypeTovar d on b.ntypetovar = d.id
							left join dbo.s_rcena r on r.id_tovar = a.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = a.id_tovar and rr.tdate_n<=@dPrihod order by rr.tdate_n desc)
							
	where
			a.id_allprihod=@id	
	order by		
			CONVERT(int,substring(a.npp,charindex('ПП',a.npp)+2,LEN(a.npp)-charindex('ПП',a.npp)))
end

if @type=2
begin
	select 
			a.id
			,a.id_tovar as kodt
			,b.ean
			,ltrim(rtrim(d.cName)+' ' + rtrim(b.cname)) as name
			,substring(npp,charindex('ОП',npp)+2,LEN(npp)-charindex('ОП',npp))as npp
			,zcena as cena
			,convert(int,round(case a.zcena when 0 then -100 else a.rcena*100/a.zcena-100 end,0)) as nadb
			,a.rcena
			,a.rcena as oldrcena
			,a.netto
			,a.nds
			,a.nds as oldnds
			,a.zcena*a.netto as zsum
			,a.rcena*a.netto as rsum
			,r.rcena as realRcena
			,r.rcena * (100 - @prnc)/100 as minPrice 
			,r.rcena * (100 + @prnc)/100 as maxPrice
			,@prnc as prnc
	from
			dbo.j_otgruz a	left join dbo.v_tovar b on a.id_tovar=b.id_tovar
							left join dbo.s_nds c on b.id_nds=c.id
							left join dbo.s_TypeTovar d on b.ntypetovar = d.id
							left join dbo.s_rcena r on r.id_tovar = a.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = a.id_tovar and rr.tdate_n<=@dPrihod order by rr.tdate_n desc)
	where
			a.id_allprihod=@id
	order by		
			CONVERT(int,substring(a.npp,charindex('ОП',a.npp)+2,LEN(a.npp)-charindex('ОП',a.npp)))			
end

if @type=3
begin
	select 
			a.id
			,a.id_tovar as kodt
			,b.ean
			,ltrim(rtrim(d.cName)+' ' + rtrim(b.cname)) as name
			,substring(npp,charindex('БР',npp)+2,LEN(npp)-charindex('БР',npp))as npp
			,a.zcena as cena
			,convert(int,round(case a.zcena when 0 then -100 else a.rcena*100/a.zcena-100 end,0)) as nadb
			,a.rcena
			,a.rcena as oldrcena
			,a.netto
			,c.id as nds
			,c.id as oldnds
			,a.netto*a.rcena as rsum
			,r.rcena as realRcena
			,r.rcena * (100 - @prnc)/100 as minPrice 
			,r.rcena * (100 + @prnc)/100 as maxPrice
			,@prnc as prnc
	from
			dbo.j_vozvkass a	left join dbo.v_tovar b on a.id_tovar=b.id_tovar
								left join dbo.s_nds c on b.id_nds=c.id
								left join dbo.s_TypeTovar d on b.ntypetovar = d.id
								left join dbo.s_rcena r on r.id_tovar = a.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = a.id_tovar and rr.tdate_n<=@dPrihod order by rr.tdate_n desc)
	where
			a.id_allprihod=@id
	order by		
			CONVERT(int,substring(a.npp,charindex('БР',a.npp)+2,LEN(a.npp)-charindex('БР',a.npp)))			
end

if @type=4
begin
	select 
			a.id
			,a.id_tovar as kodt
			,b.ean
			,ltrim(rtrim(d.cName)+' ' + rtrim(b.cname)) as name
			,substring(npp,charindex('АС',npp)+2,LEN(npp)-charindex('АС',npp))as npp
			,a.zcena as cena
			,convert(int,round(case a.zcena when 0 then -100 else a.rcena*100/a.zcena-100 end,0)) as nadb
			,a.rcena
			,a.rcena as oldrcena
			,a.netto
			,a.nds
			,a.nds as oldnds
			,a.netto*a.rcena as rsum
			,r.rcena as realRcena
			,r.rcena * (100 - @prnc)/100 as minPrice 
			,r.rcena * (100 + @prnc)/100 as maxPrice
			,@prnc as prnc
	from
			dbo.j_spis a	left join dbo.v_tovar b on a.id_tovar=b.id_tovar
							left join dbo.s_nds c on b.id_nds=c.id
							left join dbo.s_TypeTovar d on b.ntypetovar = d.id
							left join dbo.s_rcena r on r.id_tovar = a.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = a.id_tovar and rr.tdate_n<=@dPrihod order by rr.tdate_n desc)

	where
			a.id_allprihod=@id			
end

if @type=5
begin
	select 
			a.id
			,a.id_tovar as kodt
			,b.ean
			,ltrim(rtrim(d.cName)+' ' + rtrim(b.cname)) as name
			,substring(npp,charindex('ОЦ',npp)+2,LEN(npp)-charindex('ОЦ',npp))as npp
			,zcena as cena
			,convert(int,round(case a.zcena when 0 then -100 else a.rcena*100/a.zcena-100 end,0)) as nadb
			,a.rcena
			,a.rcena as oldrcena
			,a.netto
			,a.nds
			,a.nds as oldnds
			,bcena
			,bcena as oldbcena
			,a.netto*a.rcena - a.netto*a.bcena as bsum
			,r.rcena as realRcena
			,r.rcena * (100 - @prnc)/100 as minPrice 
			,r.rcena * (100 + @prnc)/100 as maxPrice
			,@prnc as prnc
	from
			dbo.j_pereoc a	left join dbo.v_tovar b on a.id_tovar=b.id_tovar
							left join dbo.s_nds c on b.id_nds=c.id
							left join dbo.s_TypeTovar d on b.ntypetovar = d.id
							left join dbo.s_rcena r on r.id_tovar = a.id_tovar and r.id = (select TOP(1) rr.id from dbo.s_rcena rr where rr.id_tovar = a.id_tovar and rr.tdate_n<=@dPrihod order by rr.tdate_n desc)
	where
			a.id_allprihod=@id	
	order by		
			CONVERT(int,substring(a.npp,charindex('ОЦ',a.npp)+2,LEN(a.npp)-charindex('ОЦ',a.npp)))		
end

end
USE [dbase1]
GO
/****** Object:  StoredProcedure [spisanie].[GetRegdok]    Script Date: 16.04.2020 15:00:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Mito
-- Create date: 13.08.10 11:52
-- Description:	процедура получения заголовков 
-- накладных для программы spisanie
--Update:
-- Author:		SPG
-- Create date: 27.12.2013
-- Description:	Добавдение поля isCredit 
-- для программы spisanie
-- =============================================
ALTER PROCEDURE [spisanie].[GetRegdok] 
@close_date datetime,
@id_dep int,
@type int,
@close_date2 datetime = null
with recompile
AS
BEGIN
SET NOCOUNT ON
declare @sum decimal(11,4), @osumt decimal(11,4)
if @type = 1
begin
	select  
			a.id
			,left(id_custom,len(id_custom)-2)as vnudok
			,a.ttn
			,round(SUM(b.netto*b.zcena),2) as sum
			,round(SUM(b.netto*b.rcena),2) as osumt
			,a.dprihod
			,MAX(b.id_otdel) as id_dep
			,MAX(rtrim(c.name)) as dep
			,MAX(rtrim(d.cname)) as post
			,MAX(a.id_post) as id_post
			,MAX(e.FIO) as oper1
			,max(a.dkor) as dkor
			,max(a.Invoice) as sf	
			,MAX(LTRIM(rtrim(f.cname)))	as UL,
			'credit' = case
					when a.Credit = 0 then 'под реализацию'	
					when a.Credit = 1 then 'под реализацию'	
					when a.Credit = 2 then 'с отсрочкой'	
					when a.Credit = 3 then ' в кредит'	
					when a.Credit = 4 then 'вторая кредитная линия'	
					   end
			,a.id_operand
			,o.name as nameOperand
	from 
			(select 
					al.*
					,(select id_Post from dbo.tfGetListMainOrgs(al.ntypeorg,al.dprihod)) as ul_id
				from 
					dbo.j_allprihod al
			where
					al.InventSpis = 1
					and((@close_date2 is null and datediff(day,al.dprihod,@close_date)=0)
					or (@close_date2 is not null and datediff(day,al.dprihod,@close_date)<=0
						and datediff(day,al.dprihod,@close_date2)>=0))
					and al.id_operand = 1
			) a	left join dbo.j_prihod b on a.id=b.id_allprihod
				left join dbo.departments c on b.id_otdel=c.id
				left join dbo.s_post d on d.id=a.id_post
				left join dbo.ListUsers e on a.id_oper1=e.id_Access
				left join dbo.s_post f on a.ul_id = f.id
				left join dbo.s_operand o on o.id = a.id_operand
								
	where			
			@id_dep = 0 or (@id_dep>0 and b.id_otdel=@id_dep)
	group by
			a.id
			,a.id_custom
			,ttn
			,a.dprihod
			,a.Credit
			,o.name
			,a.id_operand
	order by
			ttn
end
		
if @type = 2
begin
	select  distinct
			a.id
			,left(id_custom,len(id_custom)-2)as vnudok
			,a.ttn
			,round(SUM(b.netto*b.zcena),2) as sum
			,round(SUM(b.netto*b.rcena),2) as osumt
			,a.dprihod
			,MAX(b.id_otdel) as id_dep
			,MAX(rtrim(c.name)) as dep
			,MAX(rtrim(d.cname)) as post
			,MAX(a.id_post) as id_post
			,MAX(e.FIO) as oper1
			,max(a.dkor) as dkor	
			,MAX(LTRIM(rtrim(f.cname)))	as UL		
			,'credit' = case
					when a.Credit = 0 then 'под реализацию'	
					when a.Credit = 1 then 'под реализацию'	
					when a.Credit = 2 then 'с отсрочкой'	
					when a.Credit = 3 then ' в кредит'	
					when a.Credit = 4 then 'вторая кредитная линия'	
					   end
			,a.id_operand
			,o.name as nameOperand
	from 
			(select 
					al.*
					,(select id_Post from dbo.tfGetListMainOrgs(al.ntypeorg,al.dprihod)) as ul_id
				from 
					dbo.j_allprihod al
			where
					al.InventSpis = 1
					and((@close_date2 is null and datediff(day,al.dprihod,@close_date)=0)
					or (@close_date2 is not null and datediff(day,al.dprihod,@close_date)<=0
						and datediff(day,al.dprihod,@close_date2)>=0))
					and al.id_operand = 8
			) a	left join dbo.j_otgruz b on a.id=b.id_allprihod
				left join dbo.departments c on b.id_otdel=c.id
				left join dbo.s_post d on d.id=a.id_post
				left join dbo.ListUsers e on a.id_oper1=e.id_Access
				left join dbo.s_post f on a.ul_id = f.id
				left join dbo.s_operand o on o.id = a.id_operand
	where
			@id_dep = 0 or (@id_dep>0 and b.id_otdel=@id_dep)
	group by
			a.id
			,a.id_custom
			,ttn
			,a.dprihod
			,a.Credit
			,o.name
			,a.id_operand
	order by 
			ttn
end

if @type = 3
begin
	select 
			distinct
			a.id
			,left(id_custom,len(id_custom)-2)as vnudok
			,ttn
			,round(SUM(b.netto*b.zcena),2) as sum
			,round(SUM(b.netto*b.rcena),2) as osumt
			,a.dprihod
			,MAX(b.id_otdel) as id_dep
			,MAX(rtrim(c.name)) as dep
			,MAX(rtrim(d.cname)) as post
			,MAX(a.id_post) as id_post
			,MAX(e.FIO) as oper1
			,max(a.dkor) as dkor
			,MAX(LTRIM(rtrim(f.cname)))	as UL	
			,'credit' = case
					when a.Credit = 0 then 'под реализацию'	
					when a.Credit = 1 then 'под реализацию'	
					when a.Credit = 2 then 'с отсрочкой'	
					when a.Credit = 3 then ' в кредит'	
					when a.Credit = 4 then 'вторая кредитная линия'	
					   end	
			,a.id_operand
			,o.name as nameOperand
	from 
			(select 
					al.*
					,(select id_Post from dbo.tfGetListMainOrgs(al.ntypeorg,al.dprihod)) as ul_id
				from 
					dbo.j_allprihod al
			where
					al.InventSpis = 1
					and((@close_date2 is null and datediff(day,al.dprihod,@close_date)=0)
					or (@close_date2 is not null and datediff(day,al.dprihod,@close_date)<=0
						and datediff(day,al.dprihod,@close_date2)>=0))
					and al.id_operand = 7
			) a	left join dbo.j_vozvkass b on a.id=b.id_allprihod
				left join dbo.departments c on b.id_otdel=c.id
				left join dbo.s_post d on d.id=a.id_post
				left join dbo.ListUsers e on a.id_oper1=e.id_Access
				left join dbo.s_post f on a.ul_id = f.id
				left join dbo.s_operand o on o.id = a.id_operand
	where
			@id_dep = 0 or (@id_dep>0 and b.id_otdel=@id_dep)
	group by
			a.id
			,a.id_custom
			,ttn
			,a.dprihod
			,a.Credit
			,o.name
			,a.id_operand
	order by 
			ttn
end

if @type = 4
begin
	select 
			distinct
			a.id
			,left(id_custom,len(id_custom)-2)as vnudok
			,a.ttn
			,round(sum(b.netto*b.rcena),2) as osumt
			,a.dprihod
			,MAX(b.id_otdel) as id_dep
			,MAX(rtrim(c.name)) as dep
			,MAX(rtrim(d.cname)) as post
			,MAX(a.id_post) as id_post
			,MAX(e.FIO) as oper1
			,max(a.dkor) as dkor
			,sum(b.netto*b.zcena) as sum
			,MAX(LTRIM(rtrim(f.cname)))	as UL	
			,'credit' = case
					when a.Credit = 0 then 'под реализацию'	
					when a.Credit = 1 then 'под реализацию'	
					when a.Credit = 2 then 'с отсрочкой'	
					when a.Credit = 3 then ' в кредит'	
					when a.Credit = 4 then 'вторая кредитная линия'	
					   end
			,a.id_operand
			,o.name as nameOperand
	from 
			(select 
					al.*
					,(select id_Post from dbo.tfGetListMainOrgs(al.ntypeorg,al.dprihod)) as ul_id
				from 
					dbo.j_allprihod al
			where
					al.InventSpis = 1
					and((@close_date2 is null and datediff(day,al.dprihod,@close_date)=0)
					or (@close_date2 is not null and datediff(day,al.dprihod,@close_date)<=0
						and datediff(day,al.dprihod,@close_date2)>=0))
					and al.id_operand = 5
			) a	left join dbo.j_spis b on a.id=b.id_allprihod
				left join dbo.departments c on b.id_otdel=c.id
				left join dbo.s_post d on d.id=a.id_post
				left join dbo.ListUsers e on a.id_oper1=e.id_Access
				left join dbo.s_post f on a.ul_id = f.id
				left join dbo.s_operand o on o.id = a.id_operand
	where
			@id_dep = 0 or (@id_dep>0 and b.id_otdel=@id_dep)
	group by
			a.id
			,a.id_custom
			,ttn
			,a.dprihod
			,a.Credit
			,o.name
			,a.id_operand
	order by 
			ttn
end

if @type = 5
begin
	declare @ttn varchar(10)
	set @ttn = null
	select 
			@ttn=ttn 
	from 
			dbo.j_allprihod a left join dbo.j_otgruz b on a.id=b.id_allprihod
	where
			a.dprihod=@close_date
			and b.id_otdel=@id_dep
			and InventSpis = 1
			and id_operand = 8
	select 
			distinct
			a.id
			,left(id_custom,len(id_custom)-2)as vnudok
			,rtrim(ttn)+ISNULL('.'+@ttn,'') as ttn
			,ttn as ttn2
			,round(SUM((netto*rcena)-(netto*bcena)),2) as osumt
			,a.dprihod
			,MAX(b.id_otdel) as id_dep
			,MAX(rtrim(c.name)) as dep
			,MAX(rtrim(d.cname)) as post
			,MAX(a.id_post) as id_post
			,MAX(e.FIO) as oper1
			,max(a.dkor) as dkor
			,MAX(LTRIM(rtrim(f.cname)))	as UL
			,'credit' = case
					when a.Credit = 0 then 'под реализацию'	
					when a.Credit = 1 then 'под реализацию'	
					when a.Credit = 2 then 'с отсрочкой'	
					when a.Credit = 3 then ' в кредит'	
					when a.Credit = 4 then 'вторая кредитная линия'	
					   end
			,a.id_operand
			,o.name as nameOperand
	from 
			(select 
					al.*
					,(select id_Post from dbo.tfGetListMainOrgs(al.ntypeorg,al.dprihod)) as ul_id
				from 
					dbo.j_allprihod al
			where
					al.InventSpis = 1
					and((@close_date2 is null and datediff(day,al.dprihod,@close_date)=0)
					or (@close_date2 is not null and datediff(day,al.dprihod,@close_date)<=0
						and datediff(day,al.dprihod,@close_date2)>=0))
					and al.id_operand = 4
			) a	left join dbo.j_pereoc b on a.id=b.id_allprihod
				left join dbo.departments c on b.id_otdel=c.id
				left join dbo.s_post d on d.id=a.id_post
				left join dbo.ListUsers e on a.id_oper1=e.id_Access
				left join dbo.s_post f on a.ul_id = f.id
				left join dbo.s_operand o on o.id = a.id_operand
	where
			@id_dep = 0 or (@id_dep>0 and b.id_otdel=@id_dep)
	group by
			a.id
			,a.id_custom
			,ttn
			,a.dprihod
			,a.Credit
			,o.name
			,a.id_operand
	order by 
			ttn
end	
		
end	
	

USE [dbase1]
GO
/****** Object:  StoredProcedure [spisanie].[Add_Allprihod]    Script Date: 17.04.2020 13:36:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Mito
-- Create date: 01.10.12 16:28
-- Description:	процедура создания шпрок накладных 
-- для программы spisanie
--Update:
-- Author:		SPG
-- Create date: 27.12.2013
-- Description:	Добавдение поля isCredit 
-- для программы spisanie
-- =============================================
ALTER PROCEDURE [spisanie].[Add_Allprihod] 
@id_user int,
@close_date datetime,
@type int,
@id_prog int,
@ntypeorg int,
@isCredit int
AS
BEGIN
SET NOCOUNT ON

declare @vnudok int, @id_post int

declare @id_invoices int

declare @typename varchar(2)

declare @id_sub int 

select top 1
		@id_invoices = CONVERT(int,value)
from
		dbo.prog_config
where
		id_prog = @id_prog
		and id_value = 'invs'
						
select 
		@vnudok=MAX(id)+1
from
		dbo.j_allprihod
		
declare @newid int
set @newid = -1

if @type = 1
begin
	set @typename = 'ПП'
	select 
			@id_post = CONVERT(int,value)
	from
			dbo.prog_config
	where
			id_prog = @id_prog
			and id_value = 'post'
			
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 1
			
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,id_Creator
			   ,Invoice
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'ПП'
			   ,@close_date
			   ,CONVERT(varchar(9),@vnudok)+'//'
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,1
			   ,@id_user
			   ,CONVERT(varchar(9),@vnudok)+'//'
			   ,@id_sub
			   ,@isCredit)
end

if @type = 2
begin
	set @typename = 'ОЦ'
	-- id поставщика переоценки из ПО "Ведение накладных"
	select TOP(1) @id_post = convert(int,value) from dbo.prog_config where id_prog = @id_invoices and id_value = 'dpos'
		
	declare @id_ot int
	select 
			@id_ot = MAX(id)
	from 
			dbo.j_allprihod a
	where
			id_operand = 8
			and InventSpis = 1
			and DATEDIFF(day,dprihod,@close_date)=0
			
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 4
				
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,id_Creator
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'ОЦ'
			   ,@close_date
			   ,CONVERT(varchar(9),@vnudok)+'.'+RIGHT(convert(varchar(9),@id_ot),10-LEN(CONVERT(varchar(9),@vnudok)))
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,4
			   ,@id_user
			   ,@id_sub
			   ,@isCredit)
			   
			set @newid = SCOPE_IDENTITY()
			
			declare @id_pereoc int
			select 
					@id_pereoc = MAX(id)
			from 
					dbo.j_allprihod a
			where
					id_operand = 4
					and InventSpis = 1
					and DATEDIFF(day,dprihod,@close_date)=0   
					
			insert into invoices.base_vs_second (id_base, id_second) values (@id_ot, @id_pereoc)
         
end

if @type = 6
begin
	set @typename = 'ОЦ'
	-- id поставщика переоценки из ПО "Ведение накладных"
	select TOP(1) @id_post = convert(int,value) from dbo.prog_config where id_prog = @id_invoices and id_value = 'dpos'
	
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 4
			
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,id_Creator
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'ОЦ'
			   ,@close_date
			   ,CONVERT(varchar(9),@vnudok)
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,4
			   ,@id_user
			   ,@id_sub
			   ,@isCredit)
end

if @type = 7
begin
	set @typename = 'ОЦ'
	-- id поставщика переоценки из ПО "Ведение накладных"
	select TOP(1) @id_post = convert(int,value) from dbo.prog_config where id_prog = @id_invoices and id_value = 'dpos'
		
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 4
			
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,Comment
			   ,id_Creator
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'ОЦ'
			   ,@close_date
			   ,CONVERT(varchar(9),@vnudok)
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,4
			   ,'.'
			    ,@id_user
			    ,@id_sub
			    ,@isCredit)
end
			   
if @type = 3
begin
	set @typename = 'ОП'
	select 
			@id_post = CONVERT(int,value)
	from
			dbo.prog_config
	where
			id_prog = @id_prog
			and id_value = 'buy'
	
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 8
			
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,id_Creator
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'ОП'
			   ,@close_date
			   ,'i'+CONVERT(varchar(9),@vnudok)+'//'
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,8
			    ,@id_user
			    ,@id_sub
			    ,@isCredit)
end

if @type = 4
begin
	set @typename = 'БР'
	-- id поставщика возрат от покупателя из ПО "Ведение накладных"
	select TOP(1) @id_post = convert(int,value) from dbo.prog_config where id_prog = @id_invoices and id_value = 'ppos'
			
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 7
			
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,id_Creator
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'БР'
			   ,@close_date
			   ,CONVERT(varchar(9),@vnudok)+'//'
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,7
			   ,@id_user
			   ,@id_sub
			   ,@isCredit)
end

if @type = 5
begin
	set @typename = 'АС'
	-- id поставщика списания из ПО "Ведение накладных"
	select TOP(1) @id_post = convert(int,value) from dbo.prog_config where id_prog = @id_invoices and id_value = 'spos'
	
	select top 1
			@id_sub = id 
	from 
			dbo.s_Operation 
	where 
			id_SubType = 7 
			and id_Operand = 5
			
	INSERT INTO dbo.j_allprihod
			   ([id_custom]
			   ,[dprihod]
			   ,[ttn]
			   ,[id_post]
			   ,[id_priem]
			   ,[dkor]
			   ,[id_oper1]
			   ,[InventSpis]
			   ,ntypeorg
			   ,DateCreate
			   ,id_operand
			   ,id_Creator
			   ,SubTypeOperand
			   ,Credit)
		 VALUES
			   (CONVERT(varchar(9),@vnudok)+'АС'
			   ,@close_date
			   ,CONVERT(varchar(9),@vnudok)+'//'
			   ,@id_post
			   ,@id_user
			   ,GETDATE()
			   ,@id_user
			   ,1
			   ,@ntypeorg
			   ,GETDATE()
			   ,5
			   ,@id_user
			   ,@id_sub
			   ,@isCredit)
end

if @newid = -1
begin
	set @newid = SCOPE_IDENTITY()
end

update dbo.j_allprihod
set [id_custom] =  CONVERT(varchar(9),@newid)+@typename
where id = @newid

/*	
select 
		@newid as id, 
		CONVERT(varchar(9),@newid) as vnudok
*/

select 
	id
	,CONVERT(varchar(9),@newid) as vnudok
	,ttn
	,a.id_custom
	,ntypeorg
	,id_post
from dbo.j_allprihod a where id = @newid

end 



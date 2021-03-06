USE [dbase1]
GO
/****** Object:  StoredProcedure [Processing].[UpdateProgConfig]    Script Date: 14.04.2020 0:20:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Букинга Кирилл>
-- Create date: <25.03.2009>
-- Description:	<Удаление акта переоценки>
-- добавлено <Балкаева Наташа> <11.09.2009>:
-- @idvalue,
-- (id_value is null or (id_value is not null and id_value=@idvalue))
-- =============================================
ALTER PROCEDURE [Processing].[UpdateProgConfig]

@Idprogramm smallint   = null,
@idvalue char(4) = null,
@value	varChar(64) = null with execute as self


AS
BEGIN
	SET NOCOUNT ON;

	if @idvalue is not null and not exists (select id from dbo.prog_config where id_prog = @Idprogramm and id_value = @idvalue)
		BEGIN
			INSERT INTO dbo.prog_config (id_prog,id_value,type_value,value,value_name,comment)
				values (@Idprogramm,@idvalue,'N',@value,'','')

			return;
		END
	ELSE
		BEGIN
			UPDATE prog_config
			 SET value = @value
			 WHERE id_prog=@Idprogramm and 
				   (id_value is null or (id_value is not null and id_value=@idvalue))
		END
END

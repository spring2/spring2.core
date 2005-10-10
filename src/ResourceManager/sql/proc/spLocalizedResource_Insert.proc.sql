if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLocalizedResource_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLocalizedResource_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spLocalizedResource_Insert
	@ResourceId	Int = null,
	@Locale	Int = null,
	@Language	Int = null,
	@Content	VarChar(2000) = null

AS


INSERT INTO LocalizedResource
(	ResourceId,
	Locale,
	Language,
	Content)
VALUES (
	@ResourceId,
	@Locale,
	@Language,
	@Content)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spLocalizedResource_Insert: Unable to insert new row into LocalizedResource table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


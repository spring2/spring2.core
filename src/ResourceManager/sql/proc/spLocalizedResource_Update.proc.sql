if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spLocalizedResource_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spLocalizedResource_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spLocalizedResource_Update

	@LocalizedResourceId	Int = null,
	@ResourceId	Int = null,
	@Locale	VarChar(100) = null,
	@Language	VarChar(100) = null,
	@Content	VarChar(4000) = null

AS


UPDATE
	LocalizedResource
SET
	ResourceId = @ResourceId,
	Locale = @Locale,
	Language = @Language,
	Content = @Content
WHERE
LocalizedResourceId = @LocalizedResourceId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spLocalizedResource_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO


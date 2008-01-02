if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLink_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLink_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMenuLink_Update

	@MenuLinkId	Int = null,
	@Name	VarChar(75) = null,
	@Target	VarChar(150) = null,
	@Active	Bit = null,
	@MenuLinkGroupId	Int = null,
	@ParentMenuLinkId	Int = null,
	@EffectiveDate	DateTime = null,
	@ExpirationDate	DateTime = null

AS


UPDATE
	MenuLink
SET
	Name = @Name,
	Target = @Target,
	Active = @Active,
	MenuLinkGroupId = @MenuLinkGroupId,
	ParentMenuLinkId = @ParentMenuLinkId,
	EffectiveDate = @EffectiveDate,
	ExpirationDate = @ExpirationDate
WHERE
MenuLinkId = @MenuLinkId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spMenuLink_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO


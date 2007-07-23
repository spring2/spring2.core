if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLink_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLink_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMenuLink_Insert
	@Name	VarChar(75) = null,
	@Target	VarChar(150) = null,
	@Active	Bit = null,
	@MenuLinkGroupId	Int = null,
	@ParentMenuLinkId	Int = null,
	@EffectiveDate	DateTime = null,
	@ExpirationDate	DateTime = null

AS


INSERT INTO MenuLink
(	Name,
	Target,
	Active,
	MenuLinkGroupId,
	ParentMenuLinkId,
	EffectiveDate,
	ExpirationDate)
VALUES (
	@Name,
	@Target,
	@Active,
	@MenuLinkGroupId,
	@ParentMenuLinkId,
	@EffectiveDate,
	@ExpirationDate)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spMenuLink_Insert: Unable to insert new row into MenuLink table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


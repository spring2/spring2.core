if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLinkKey_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLinkKey_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMenuLinkKey_Insert
	@MenuLinkId	Int = null,
	@Key	VarChar(100) = null

AS


INSERT INTO MenuLinkKey
(	MenuLinkId,
	[Key])
VALUES (
	@MenuLinkId,
	@Key)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR ('spMenuLinkKey_Insert: Unable to insert new row into MenuLinkKey table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


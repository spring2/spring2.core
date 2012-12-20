if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLinkGroup_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLinkGroup_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMenuLinkGroup_Insert
	@Name	VarChar(80) = null

AS


INSERT INTO MenuLinkGroup
(	Name)
VALUES (
	@Name)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR ('spMenuLinkGroup_Insert: Unable to insert new row into MenuLinkGroup table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


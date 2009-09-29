if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLinkGroup_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLinkGroup_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMenuLinkGroup_Update

	@MenuLinkGroupId	Int = null,
	@Name	VarChar(80) = null

AS


UPDATE
	MenuLinkGroup
SET
	Name = @Name
WHERE
MenuLinkGroupId = @MenuLinkGroupId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spMenuLinkGroup_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO


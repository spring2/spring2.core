if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLinkGroup_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLinkGroup_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMenuLinkGroup_Delete

@MenuLinkGroupId	Int

AS

if not exists(SELECT * FROM MenuLinkGroup WHERE (	MenuLinkGroupId = @MenuLinkGroupId
))
    BEGIN
        RAISERROR  ('spMenuLinkGroup_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	MenuLinkGroup
WHERE 
	MenuLinkGroupId = @MenuLinkGroupId
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLink_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLink_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spMenuLink_Delete

@MenuLinkId	Int

AS

if not exists(SELECT * FROM MenuLink WHERE (	MenuLinkId = @MenuLinkId
))
    BEGIN
        RAISERROR  ('spMenuLink_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	MenuLink
WHERE 
	MenuLinkId = @MenuLinkId
GO


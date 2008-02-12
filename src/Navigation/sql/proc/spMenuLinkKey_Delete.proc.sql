if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spMenuLinkKey_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spMenuLinkKey_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spMenuLinkKey_Delete

@MenuLinkId	Int,
@Key	VarChar(100)

AS

if not exists(SELECT * FROM MenuLinkKey WHERE (	MenuLinkId = @MenuLinkId
 and
	[Key] = @Key
))
    BEGIN
        RAISERROR  ('spMenuLinkKey_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	MenuLinkKey
WHERE 
	MenuLinkId = @MenuLinkId
 and
	[Key] = @Key
GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spResource_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spResource_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spResource_Insert
	@Context	VarChar(100) = null,
	@Field	VarChar(100) = null,
	@ContextIdentity	Int = null

AS


INSERT INTO Resource
(	Context,
	Field,
	ContextIdentity)
VALUES (
	@Context,
	@Field,
	@ContextIdentity)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spResource_Insert: Unable to insert new row into Resource table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


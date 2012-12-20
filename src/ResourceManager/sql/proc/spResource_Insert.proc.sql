if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spResource_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spResource_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spResource_Insert
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
        RAISERROR ('spResource_Insert: Unable to insert new row into Resource table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


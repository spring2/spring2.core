if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spResource_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spResource_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spResource_Update

	@ResourceId	Int = null,
	@Context	VarChar(100) = null,
	@Field	VarChar(100) = null,
	@ContextIdentity	Int = null

AS


UPDATE
	Resource
SET
	Context = @Context,
	Field = @Field,
	ContextIdentity = @ContextIdentity
WHERE
ResourceId = @ResourceId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spResource_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO


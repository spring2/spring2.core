if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spConfigurationSetting_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spConfigurationSetting_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spConfigurationSetting_Insert
	@Key	VarChar(100) = null,
	@Value	VarChar(1024) = null,
	@LastModifiedDate	DateTime = null,
	@LastModifiedUserId	Int = null

AS


INSERT INTO ConfigurationSetting
(	[Key],
	Value,
	LastModifiedDate,
	LastModifiedUserId)
VALUES (
	@Key,
	@Value,
	@LastModifiedDate,
	@LastModifiedUserId)

if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spConfigurationSetting_Insert: Unable to insert new row into ConfigurationSetting table '
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


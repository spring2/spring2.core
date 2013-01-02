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
	@LastModifiedUserId	Int = null,
	@EffectiveDate DateTime = null

AS


INSERT INTO ConfigurationSetting
(	[Key],
	Value,
	LastModifiedDate,
	LastModifiedUserId,
	EffectiveDate
	)
VALUES (
	@Key,
	@Value,
	@LastModifiedDate,
	@LastModifiedUserId,
	@EffectiveDate)

if @@rowcount <> 1 or @@error!=0
    BEGIN
		RAISERROR ('spConfigurationSetting_Insert: Unable to insert new row into ConfigurationSetting table ', 16, 1, @@ROWCOUNT)
        RETURN(-1)
    END

return SCOPE_IDENTITY()
GO


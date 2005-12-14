if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spConfigurationSetting_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spConfigurationSetting_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.spConfigurationSetting_Delete

@ConfigurationSettingId	Int

AS

if not exists(SELECT * FROM ConfigurationSetting WHERE (	ConfigurationSettingId = @ConfigurationSettingId
))
    BEGIN
        RAISERROR  ('spConfigurationSetting_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	ConfigurationSetting
WHERE 
	ConfigurationSettingId = @ConfigurationSettingId
GO


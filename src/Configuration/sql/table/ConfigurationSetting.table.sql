SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn%' and xtype='P')
drop procedure #spAlterColumn
GO

CREATE PROCEDURE #spAlterColumn
    @table varchar(100),
    @column varchar(100),
    @type varchar(50),
    @required bit
AS
if exists (select * from syscolumns where name=@column and id=object_id(@table))
begin
	declare @nullstring varchar(8)
	set @nullstring = case when @required=0 then 'null' else 'not null' end
	exec('alter table [' + @table + '] alter column [' + @column + '] ' + @type + ' ' + @nullstring)
end
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[ConfigurationSetting]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.ConfigurationSetting (
	ConfigurationSettingId Int IDENTITY(1,1) NOT NULL,
	[Key] VarChar(100) NOT NULL,
	Value VarChar(1024) NOT NULL,
	LastModifiedDate DateTime NOT NULL,
	LastModifiedUserId Int NOT NULL,
	EffectiveDate DateTime NOT NULL CONSTRAINT [DF_ConfigurationSetting_EffectiveDate] DEFAULT (getdate())
)
GO

if not exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'ConfigurationSettingId')
  BEGIN
	ALTER TABLE ConfigurationSetting ADD
	    ConfigurationSettingId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'ConfigurationSettingId')
  BEGIN
	exec #spAlterColumn 'ConfigurationSetting', 'ConfigurationSettingId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'Key')
  BEGIN
	ALTER TABLE ConfigurationSetting ADD
	    [Key] VarChar(100) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'Key')
  BEGIN
	exec #spAlterColumn 'ConfigurationSetting', 'Key', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'Value')
  BEGIN
	ALTER TABLE ConfigurationSetting ADD
	    Value VarChar(1024) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'Value')
  BEGIN
	exec #spAlterColumn 'ConfigurationSetting', 'Value', 'VarChar(1024)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'LastModifiedDate')
  BEGIN
	ALTER TABLE ConfigurationSetting ADD
	    LastModifiedDate DateTime NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'LastModifiedDate')
  BEGIN
	exec #spAlterColumn 'ConfigurationSetting', 'LastModifiedDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'LastModifiedUserId')
  BEGIN
	ALTER TABLE ConfigurationSetting ADD
	    LastModifiedUserId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'LastModifiedUserId')
  BEGIN
	exec #spAlterColumn 'ConfigurationSetting', 'LastModifiedUserId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('ConfigurationSetting') and name = 'EffectiveDate')
  BEGIN
	ALTER TABLE ConfigurationSetting ADD
	    EffectiveDate DateTime NOT NULL
	CONSTRAINT
	    [DF_ConfigurationSetting_EffectiveDate] DEFAULT getdate() WITH VALUES
  END
GO

if exists(select * from syscolumns where id=object_id('Category') and name = 'EffectiveDate')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('ConfigurationSetting') and name = 'EffectiveDate'

	if @cdefault is not null
		exec('alter table ConfigurationSetting DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_ConfigurationSetting_EffectiveDate' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_ConfigurationSetting_EffectiveDate' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_ConfigurationSetting_EffectiveDate]')
          end
	
	update ConfigurationSetting set EffectiveDate = getdate() where EffectiveDate IS NULL
	exec #spAlterColumn 'ConfigurationSetting', 'EffectiveDate', 'DateTime', 1
	if not exists(select * from sysobjects where name = 'DF_ConfigurationSetting_EffectiveDate' and xtype='D')
		alter table ConfigurationSetting
			ADD CONSTRAINT [DF_ConfigurationSetting_EffectiveDate] DEFAULT getdate() FOR EffectiveDate WITH VALUES
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_ConfigurationSetting') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE ConfigurationSetting WITH NOCHECK ADD
	CONSTRAINT PK_ConfigrationSetting PRIMARY KEY NONCLUSTERED
	(
		ConfigurationSettingId
	)
GO

if exists (select * from dbo.sysobjects where id = object_id(N'UN_ConfigurationSetting') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE ConfigurationSetting DROP CONSTRAINT UN_ConfigurationSetting
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_ConfigurationSettingEffectiveDate') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE ConfigurationSetting ADD
	CONSTRAINT UN_ConfigurationSettingEffectiveDate UNIQUE
	(
		[Key],
		EffectiveDate
	)
GO


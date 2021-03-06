SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_PublicationType%' and xtype='P')
drop procedure #spAlterColumn_PublicationType
GO

CREATE PROCEDURE #spAlterColumn_PublicationType
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[PublicationType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.PublicationType (
	PublicationTypeId Int IDENTITY(1,1) NOT NULL,
	Name VarChar(50) NOT NULL,
	EmailSubject VarChar(500) NULL,
	EmailBody Text NULL,
	EmailBodyType VarChar(100) NULL,
	Description VarChar(max) NULL,
	MailMessageType VarChar(100) NULL,
	LastSentDate DateTime NOT NULL CONSTRAINT [DF_PublicationType_LastSentDate] DEFAULT (getdate()),
	FrequencyInMinutes Int NOT NULL,
	ProviderName VarChar(250) NOT NULL,
	AllowSubscription Bit NOT NULL,
	AutoSubscribe Bit NOT NULL,
	EffectiveDate DateTime NOT NULL CONSTRAINT [DF_PublicationType_EffectiveDate] DEFAULT (getdate()),
	ExpirationDate DateTime NULL,
	CreateDate DateTime NOT NULL,
	CreateUserId Int NOT NULL,
	LastModifiedDate DateTime NOT NULL,
	LastModifiedUserId Int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'PublicationTypeId')
  BEGIN
	ALTER TABLE PublicationType ADD
	    PublicationTypeId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='PublicationTypeId') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='PublicationTypeId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'PublicationTypeId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'Name')
  BEGIN
	ALTER TABLE PublicationType ADD
	    Name VarChar(50) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='Name') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='Name' and character_maximum_length=50 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'Name', 'VarChar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'EmailSubject')
  BEGIN
	ALTER TABLE PublicationType ADD
	    EmailSubject VarChar(500) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='EmailSubject') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='EmailSubject' and character_maximum_length=500 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'EmailSubject', 'VarChar(500)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'EmailBody')
  BEGIN
	ALTER TABLE PublicationType ADD
	    EmailBody Text NULL
  END
GO
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'EmailBodyType')
  BEGIN
	ALTER TABLE PublicationType ADD
	    EmailBodyType VarChar(100) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='EmailBodyType') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='EmailBodyType' and character_maximum_length=100 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'EmailBodyType', 'VarChar(100)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'Description')
  BEGIN
	ALTER TABLE PublicationType ADD
	    Description VarChar(max) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='Description') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='Description' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'Description', 'VarChar(max)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'MailMessageType')
  BEGIN
	ALTER TABLE PublicationType ADD
	    MailMessageType VarChar(100) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='MailMessageType') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='MailMessageType' and character_maximum_length=100 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'MailMessageType', 'VarChar(100)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'LastSentDate')
  BEGIN
	ALTER TABLE PublicationType ADD
	    LastSentDate DateTime NOT NULL
	CONSTRAINT
	    [DF_PublicationType_LastSentDate] DEFAULT getdate() WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='LastSentDate') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='LastSentDate' and is_nullable='NO')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('PublicationType') and name = 'LastSentDate'

	if @cdefault is not null
		exec('alter table PublicationType DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_PublicationType_LastSentDate' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_PublicationType_LastSentDate' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_PublicationType_LastSentDate]')
          end
	
	update PublicationType set LastSentDate = getdate() where LastSentDate IS NULL
	exec #spAlterColumn_PublicationType 'PublicationType', 'LastSentDate', 'DateTime', 1
	if not exists(select * from sysobjects where name = 'DF_PublicationType_LastSentDate' and xtype='D')
		alter table PublicationType
			ADD CONSTRAINT [DF_PublicationType_LastSentDate] DEFAULT getdate() FOR LastSentDate WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'FrequencyInMinutes')
  BEGIN
	ALTER TABLE PublicationType ADD
	    FrequencyInMinutes Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='FrequencyInMinutes') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='FrequencyInMinutes' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'FrequencyInMinutes', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'ProviderName')
  BEGIN
	ALTER TABLE PublicationType ADD
	    ProviderName VarChar(250) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='ProviderName') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='ProviderName' and character_maximum_length=250 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'ProviderName', 'VarChar(250)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'AllowSubscription')
  BEGIN
	ALTER TABLE PublicationType ADD
	    AllowSubscription Bit NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='AllowSubscription') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='AllowSubscription' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'AllowSubscription', 'Bit', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'AutoSubscribe')
  BEGIN
	ALTER TABLE PublicationType ADD
	    AutoSubscribe Bit NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='AutoSubscribe') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='AutoSubscribe' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'AutoSubscribe', 'Bit', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'EffectiveDate')
  BEGIN
	ALTER TABLE PublicationType ADD
	    EffectiveDate DateTime NOT NULL
	CONSTRAINT
	    [DF_PublicationType_EffectiveDate] DEFAULT getdate() WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='EffectiveDate') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='EffectiveDate' and is_nullable='NO')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('PublicationType') and name = 'EffectiveDate'

	if @cdefault is not null
		exec('alter table PublicationType DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_PublicationType_EffectiveDate' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_PublicationType_EffectiveDate' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_PublicationType_EffectiveDate]')
          end
	
	update PublicationType set EffectiveDate = getdate() where EffectiveDate IS NULL
	exec #spAlterColumn_PublicationType 'PublicationType', 'EffectiveDate', 'DateTime', 1
	if not exists(select * from sysobjects where name = 'DF_PublicationType_EffectiveDate' and xtype='D')
		alter table PublicationType
			ADD CONSTRAINT [DF_PublicationType_EffectiveDate] DEFAULT getdate() FOR EffectiveDate WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'ExpirationDate')
  BEGIN
	ALTER TABLE PublicationType ADD
	    ExpirationDate DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='ExpirationDate') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='ExpirationDate' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'ExpirationDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'CreateDate')
  BEGIN
	ALTER TABLE PublicationType ADD
	    CreateDate DateTime NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='CreateDate') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='CreateDate' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'CreateDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'CreateUserId')
  BEGIN
	ALTER TABLE PublicationType ADD
	    CreateUserId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='CreateUserId') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='CreateUserId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'CreateUserId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'LastModifiedDate')
  BEGIN
	ALTER TABLE PublicationType ADD
	    LastModifiedDate DateTime NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='LastModifiedDate') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='LastModifiedDate' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'LastModifiedDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationType') and name = 'LastModifiedUserId')
  BEGIN
	ALTER TABLE PublicationType ADD
	    LastModifiedUserId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationType' and column_name='LastModifiedUserId') and not exists(select * from information_schema.columns where table_name='PublicationType' and column_name='LastModifiedUserId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationType 'PublicationType', 'LastModifiedUserId', 'Int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PublicationType') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PublicationType WITH NOCHECK ADD
	CONSTRAINT PK_PublicationType PRIMARY KEY CLUSTERED
	(
		PublicationTypeId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_PublicationType_Name') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE PublicationType ADD
	CONSTRAINT UN_PublicationType_Name UNIQUE
	(
		Name
	)
GO


drop procedure #spAlterColumn_PublicationType
GO

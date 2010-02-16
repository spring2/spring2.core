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

if not exists (select * from dbo.sysobjects where id = object_id(N'[CommunicationSubscriptionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.CommunicationSubscriptionType (
	CommunicationSubscriptionTypeId Int IDENTITY(1,1) NOT NULL,
	Name VarChar(50) NOT NULL,
	EmailSubject VarChar(100) NOT NULL,
	EmailBody Text NULL,
	EmailBodyType VarChar(100) NULL,
	MailMessageType VarChar(100) NULL,
	LastSentDate DateTime NOT NULL CONSTRAINT [DF_CommunicationSubscriptionType_LastSentDate] DEFAULT (getdate()),
	FrequencyInMinutes Int NOT NULL,
	ProviderName VarChar(100) NOT NULL,
	AllowSubscription Bit NOT NULL,
	AutoSubscribe Bit NOT NULL,
	EffectiveDate DateTime NOT NULL CONSTRAINT [DF_CommunicationSubscriptionType_EffectiveDate] DEFAULT (getdate()),
	ExpirationDate DateTime NULL,
	CreateDate DateTime NOT NULL,
	CreateUserId Int NOT NULL,
	LastModifiedDate DateTime NOT NULL,
	LastModifiedUserId Int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'CommunicationSubscriptionTypeId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    CommunicationSubscriptionTypeId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'CommunicationSubscriptionTypeId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'CommunicationSubscriptionTypeId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'Name')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    Name VarChar(50) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'Name')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'Name', 'VarChar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EmailSubject')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    EmailSubject VarChar(100) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EmailSubject')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'EmailSubject', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EmailBody')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    EmailBody Text NULL
  END
GO
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EmailBodyType')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    EmailBodyType VarChar(100) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EmailBodyType')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'EmailBodyType', 'VarChar(100)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'MailMessageType')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    MailMessageType VarChar(100) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'MailMessageType')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'MailMessageType', 'VarChar(100)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastSentDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    LastSentDate DateTime NOT NULL
	CONSTRAINT
	    [DF_CommunicationSubscriptionType_LastSentDate] DEFAULT getdate() WITH VALUES
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastSentDate')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastSentDate'

	if @cdefault is not null
		exec('alter table CommunicationSubscriptionType DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_CommunicationSubscriptionType_LastSentDate' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_CommunicationSubscriptionType_LastSentDate' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_CommunicationSubscriptionType_LastSentDate]')
          end
	
	update CommunicationSubscriptionType set LastSentDate = getdate() where LastSentDate IS NULL
	exec #spAlterColumn 'CommunicationSubscriptionType', 'LastSentDate', 'DateTime', 1
	if not exists(select * from sysobjects where name = 'DF_CommunicationSubscriptionType_LastSentDate' and xtype='D')
		alter table CommunicationSubscriptionType
			ADD CONSTRAINT [DF_CommunicationSubscriptionType_LastSentDate] DEFAULT getdate() FOR LastSentDate WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'FrequencyInMinutes')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    FrequencyInMinutes Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'FrequencyInMinutes')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'FrequencyInMinutes', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'ProviderName')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    ProviderName VarChar(100) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'ProviderName')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'ProviderName', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'AllowSubscription')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    AllowSubscription Bit NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'AllowSubscription')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'AllowSubscription', 'Bit', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'AutoSubscribe')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    AutoSubscribe Bit NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'AutoSubscribe')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'AutoSubscribe', 'Bit', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EffectiveDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    EffectiveDate DateTime NOT NULL
	CONSTRAINT
	    [DF_CommunicationSubscriptionType_EffectiveDate] DEFAULT getdate() WITH VALUES
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EffectiveDate')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'EffectiveDate'

	if @cdefault is not null
		exec('alter table CommunicationSubscriptionType DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_CommunicationSubscriptionType_EffectiveDate' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_CommunicationSubscriptionType_EffectiveDate' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_CommunicationSubscriptionType_EffectiveDate]')
          end
	
	update CommunicationSubscriptionType set EffectiveDate = getdate() where EffectiveDate IS NULL
	exec #spAlterColumn 'CommunicationSubscriptionType', 'EffectiveDate', 'DateTime', 1
	if not exists(select * from sysobjects where name = 'DF_CommunicationSubscriptionType_EffectiveDate' and xtype='D')
		alter table CommunicationSubscriptionType
			ADD CONSTRAINT [DF_CommunicationSubscriptionType_EffectiveDate] DEFAULT getdate() FOR EffectiveDate WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'ExpirationDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    ExpirationDate DateTime NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'ExpirationDate')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'ExpirationDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'CreateDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    CreateDate DateTime NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'CreateDate')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'CreateDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'CreateUserId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    CreateUserId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'CreateUserId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'CreateUserId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastModifiedDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    LastModifiedDate DateTime NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastModifiedDate')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'LastModifiedDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastModifiedUserId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionType ADD
	    LastModifiedUserId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionType') and name = 'LastModifiedUserId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionType', 'LastModifiedUserId', 'Int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_CommunicationSubscriptionType') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE CommunicationSubscriptionType WITH NOCHECK ADD
	CONSTRAINT PK_CommunicationSubscriptionType PRIMARY KEY CLUSTERED
	(
		CommunicationSubscriptionTypeId
	)
GO


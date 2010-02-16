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

if not exists (select * from dbo.sysobjects where id = object_id(N'[CommunicationSubscriptionTracking]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.CommunicationSubscriptionTracking (
	CommunicationPrimaryKeyId Int IDENTITY(1,1) NOT NULL,
	CommunicationSubscriptionTypeId Int NOT NULL,
	CreateDate DateTime NOT NULL,
	CreateUserId Int NOT NULL,
	LastModifiedDate DateTime NOT NULL,
	LastModifiedUserId Int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CommunicationPrimaryKeyId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionTracking ADD
	    CommunicationPrimaryKeyId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CommunicationPrimaryKeyId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionTracking', 'CommunicationPrimaryKeyId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CommunicationSubscriptionTypeId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionTracking ADD
	    CommunicationSubscriptionTypeId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CommunicationSubscriptionTypeId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionTracking', 'CommunicationSubscriptionTypeId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CreateDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionTracking ADD
	    CreateDate DateTime NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CreateDate')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionTracking', 'CreateDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CreateUserId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionTracking ADD
	    CreateUserId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'CreateUserId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionTracking', 'CreateUserId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'LastModifiedDate')
  BEGIN
	ALTER TABLE CommunicationSubscriptionTracking ADD
	    LastModifiedDate DateTime NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'LastModifiedDate')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionTracking', 'LastModifiedDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'LastModifiedUserId')
  BEGIN
	ALTER TABLE CommunicationSubscriptionTracking ADD
	    LastModifiedUserId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('CommunicationSubscriptionTracking') and name = 'LastModifiedUserId')
  BEGIN
	exec #spAlterColumn 'CommunicationSubscriptionTracking', 'LastModifiedUserId', 'Int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_CommunicationSubscriptionTracking') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE CommunicationSubscriptionTracking WITH NOCHECK ADD
	CONSTRAINT PK_CommunicationSubscriptionTracking PRIMARY KEY CLUSTERED
	(
		CommunicationPrimaryKeyId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_CommunicationSubscriptionTracking_Type') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE CommunicationSubscriptionTracking ADD
	CONSTRAINT FK_CommunicationSubscriptionTracking_Type FOREIGN KEY
	(
		CommunicationSubscriptionTypeId
	)
	REFERENCES CommunicationSubscriptionType
	(
		CommunicationSubscriptionTypeId
	)
GO


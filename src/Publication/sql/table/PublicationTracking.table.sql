SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_PublicationTracking%' and xtype='P')
drop procedure #spAlterColumn_PublicationTracking
GO

CREATE PROCEDURE #spAlterColumn_PublicationTracking
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[PublicationTracking]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.PublicationTracking (
	PublicationTrackingId Int IDENTITY(1,1) NOT NULL,
	PublicationPrimaryKeyId Int NOT NULL,
	PublicationTypeId Int NOT NULL,
	CreateDate DateTime NOT NULL,
	CreateUserId Int NOT NULL,
	LastModifiedDate DateTime NOT NULL,
	LastModifiedUserId Int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'PublicationTrackingId')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    PublicationTrackingId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='PublicationTrackingId') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='PublicationTrackingId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'PublicationTrackingId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'PublicationPrimaryKeyId')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    PublicationPrimaryKeyId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='PublicationPrimaryKeyId') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='PublicationPrimaryKeyId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'PublicationPrimaryKeyId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'PublicationTypeId')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    PublicationTypeId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='PublicationTypeId') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='PublicationTypeId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'PublicationTypeId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'CreateDate')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    CreateDate DateTime NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='CreateDate') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='CreateDate' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'CreateDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'CreateUserId')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    CreateUserId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='CreateUserId') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='CreateUserId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'CreateUserId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'LastModifiedDate')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    LastModifiedDate DateTime NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='LastModifiedDate') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='LastModifiedDate' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'LastModifiedDate', 'DateTime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PublicationTracking') and name = 'LastModifiedUserId')
  BEGIN
	ALTER TABLE PublicationTracking ADD
	    LastModifiedUserId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='LastModifiedUserId') and not exists(select * from information_schema.columns where table_name='PublicationTracking' and column_name='LastModifiedUserId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_PublicationTracking 'PublicationTracking', 'LastModifiedUserId', 'Int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PublicationTracking') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PublicationTracking WITH NOCHECK ADD
	CONSTRAINT PK_PublicationTracking PRIMARY KEY CLUSTERED
	(
		PublicationTrackingId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_PublicationPrimaryKey_Type') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE PublicationTracking ADD
	CONSTRAINT UN_PublicationPrimaryKey_Type UNIQUE
	(
		PublicationPrimaryKeyId
,
		PublicationTypeId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_PublicationTracking_Type') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE PublicationTracking ADD
	CONSTRAINT FK_PublicationTracking_Type FOREIGN KEY
	(
		PublicationTypeId
	)
	REFERENCES PublicationType
	(
		PublicationTypeId
	)
GO


drop procedure #spAlterColumn_PublicationTracking
GO

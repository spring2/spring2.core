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

if not exists (select * from dbo.sysobjects where id = object_id(N'[LocalizedResource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE LocalizedResource (
	LocalizedResourceId Int IDENTITY(1,1) NOT NULL,
	ResourceId Int NOT NULL,
	Locale Int NOT NULL,
	Language Int NOT NULL,
	Content VarChar(2000) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'LocalizedResourceId')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    LocalizedResourceId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'LocalizedResourceId')
  BEGIN
	exec #spAlterColumn 'LocalizedResource', 'LocalizedResourceId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'ResourceId')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    ResourceId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'ResourceId')
  BEGIN
	exec #spAlterColumn 'LocalizedResource', 'ResourceId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Locale')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    Locale Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Locale')
  BEGIN
	exec #spAlterColumn 'LocalizedResource', 'Locale', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Language')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    Language Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Language')
  BEGIN
	exec #spAlterColumn 'LocalizedResource', 'Language', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Content')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    Content VarChar(2000) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Content')
  BEGIN
	exec #spAlterColumn 'LocalizedResource', 'Content', 'VarChar(2000)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_LocalizedResource') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE LocalizedResource WITH NOCHECK ADD
	CONSTRAINT PK_LocalizedResource PRIMARY KEY NONCLUSTERED
	(
		LocalizedResourceId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_LocalizedResource_Resource') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE LocalizedResource ADD
	CONSTRAINT FK_LocalizedResource_Resource FOREIGN KEY
	(
		ResourceId
	)
	REFERENCES Resource
	(
		ResourceId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_LocalizedResourceLocalLanguage') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE LocalizedResource ADD
	CONSTRAINT UN_LocalizedResourceLocalLanguage UNIQUE
	(
		ResourceId
,
		Locale
,
		Language
	)
GO


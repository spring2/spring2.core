SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_LocalizedResource%' and xtype='P')
drop procedure #spAlterColumn_LocalizedResource
GO

CREATE PROCEDURE #spAlterColumn_LocalizedResource
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
CREATE TABLE dbo.LocalizedResource (
	LocalizedResourceId Int IDENTITY(1,1) NOT NULL,
	ResourceId Int NOT NULL,
	Locale VarChar(100) NOT NULL,
	Language VarChar(100) NOT NULL,
	Content VarChar(4000) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'LocalizedResourceId')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    LocalizedResourceId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='LocalizedResourceId') and not exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='LocalizedResourceId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_LocalizedResource 'LocalizedResource', 'LocalizedResourceId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'ResourceId')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    ResourceId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='ResourceId') and not exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='ResourceId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_LocalizedResource 'LocalizedResource', 'ResourceId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Locale')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    Locale VarChar(100) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='Locale') and not exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='Locale' and character_maximum_length=100 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_LocalizedResource 'LocalizedResource', 'Locale', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Language')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    Language VarChar(100) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='Language') and not exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='Language' and character_maximum_length=100 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_LocalizedResource 'LocalizedResource', 'Language', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('LocalizedResource') and name = 'Content')
  BEGIN
	ALTER TABLE LocalizedResource ADD
	    Content VarChar(4000) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='Content') and not exists(select * from information_schema.columns where table_name='LocalizedResource' and column_name='Content' and character_maximum_length=4000 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_LocalizedResource 'LocalizedResource', 'Content', 'VarChar(4000)', 1
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


drop procedure #spAlterColumn_LocalizedResource
GO

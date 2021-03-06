SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_Resource%' and xtype='P')
drop procedure #spAlterColumn_Resource
GO

CREATE PROCEDURE #spAlterColumn_Resource
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Resource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.Resource (
	ResourceId Int IDENTITY(1,1) NOT NULL,
	Context VarChar(100) NOT NULL,
	Field VarChar(100) NOT NULL,
	ContextIdentity Int NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'ResourceId')
  BEGIN
	ALTER TABLE Resource ADD
	    ResourceId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='Resource' and column_name='ResourceId') and not exists(select * from information_schema.columns where table_name='Resource' and column_name='ResourceId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_Resource 'Resource', 'ResourceId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'Context')
  BEGIN
	ALTER TABLE Resource ADD
	    Context VarChar(100) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='Resource' and column_name='Context') and not exists(select * from information_schema.columns where table_name='Resource' and column_name='Context' and character_maximum_length=100 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_Resource 'Resource', 'Context', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'Field')
  BEGIN
	ALTER TABLE Resource ADD
	    Field VarChar(100) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='Resource' and column_name='Field') and not exists(select * from information_schema.columns where table_name='Resource' and column_name='Field' and character_maximum_length=100 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_Resource 'Resource', 'Field', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'ContextIdentity')
  BEGIN
	ALTER TABLE Resource ADD
	    ContextIdentity Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='Resource' and column_name='ContextIdentity') and not exists(select * from information_schema.columns where table_name='Resource' and column_name='ContextIdentity' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_Resource 'Resource', 'ContextIdentity', 'Int', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Resource') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Resource WITH NOCHECK ADD
	CONSTRAINT PK_Resource PRIMARY KEY NONCLUSTERED
	(
		ResourceId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_ResourceEntityPropertyIdentity') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE Resource ADD
	CONSTRAINT UN_ResourceEntityPropertyIdentity UNIQUE
	(
		Context
,
		Field
,
		ContextIdentity
	)
GO


drop procedure #spAlterColumn_Resource
GO

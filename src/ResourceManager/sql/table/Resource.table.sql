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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Resource]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Resource (
	ResourceId Int IDENTITY(1,1) NOT NULL,
	Context VarChar(100) NOT NULL,
	Field VarChar(100) NOT NULL,
	[Identity] Int NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'ResourceId')
  BEGIN
	ALTER TABLE Resource ADD
	    ResourceId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Resource') and name = 'ResourceId')
  BEGIN
	exec #spAlterColumn 'Resource', 'ResourceId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'Context')
  BEGIN
	ALTER TABLE Resource ADD
	    Context VarChar(100) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Resource') and name = 'Context')
  BEGIN
	exec #spAlterColumn 'Resource', 'Context', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'Field')
  BEGIN
	ALTER TABLE Resource ADD
	    Field VarChar(100) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Resource') and name = 'Field')
  BEGIN
	exec #spAlterColumn 'Resource', 'Field', 'VarChar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Resource') and name = 'Identity')
  BEGIN
	ALTER TABLE Resource ADD
	    [Identity] Int NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Resource') and name = 'Identity')
  BEGIN
	exec #spAlterColumn 'Resource', 'Identity', 'Int', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_User') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Resource WITH NOCHECK ADD
	CONSTRAINT PK_User PRIMARY KEY NONCLUSTERED
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
		[Identity]
	)
GO


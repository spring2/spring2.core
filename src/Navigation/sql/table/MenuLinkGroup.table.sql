SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_MenuLinkGroup%' and xtype='P')
drop procedure #spAlterColumn_MenuLinkGroup
GO

CREATE PROCEDURE #spAlterColumn_MenuLinkGroup
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MenuLinkGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.MenuLinkGroup (
	MenuLinkGroupId Int IDENTITY(1,1) NOT NULL,
	Name VarChar(80) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('MenuLinkGroup') and name = 'MenuLinkGroupId')
  BEGIN
	ALTER TABLE MenuLinkGroup ADD
	    MenuLinkGroupId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLinkGroup' and column_name='MenuLinkGroupId') and not exists(select * from information_schema.columns where table_name='MenuLinkGroup' and column_name='MenuLinkGroupId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLinkGroup 'MenuLinkGroup', 'MenuLinkGroupId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLinkGroup') and name = 'Name')
  BEGIN
	ALTER TABLE MenuLinkGroup ADD
	    Name VarChar(80) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLinkGroup' and column_name='Name') and not exists(select * from information_schema.columns where table_name='MenuLinkGroup' and column_name='Name' and character_maximum_length=80 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MenuLinkGroup 'MenuLinkGroup', 'Name', 'VarChar(80)', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_MenuLinkGroup') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE MenuLinkGroup WITH NOCHECK ADD
	CONSTRAINT PK_MenuLinkGroup PRIMARY KEY NONCLUSTERED
	(
		MenuLinkGroupId
	)
GO

if not exists (select * from sys.indexes where name='IxMenuLinkGroup_Name' and object_id=object_id(N'[MenuLinkGroup]'))
	CREATE INDEX IxMenuLinkGroup_Name ON MenuLinkGroup
	(
        	Name
	)
GO

drop procedure #spAlterColumn_MenuLinkGroup
GO

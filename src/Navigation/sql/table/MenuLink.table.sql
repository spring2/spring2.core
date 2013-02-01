SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_MenuLink%' and xtype='P')
drop procedure #spAlterColumn_MenuLink
GO

CREATE PROCEDURE #spAlterColumn_MenuLink
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MenuLink]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.MenuLink (
	MenuLinkId Int IDENTITY(1,1) NOT NULL,
	Name VarChar(75) NOT NULL,
	Target VarChar(150) NOT NULL,
	Active Bit NOT NULL,
	MenuLinkGroupId Int NULL,
	ParentMenuLinkId Int NULL,
	EffectiveDate DateTime NULL,
	ExpirationDate DateTime NULL,
	Sequence Int NULL,
	TargetWindow VarChar(50) NOT NULL CONSTRAINT [DF_MenuLink_TargetWindow] DEFAULT ('_self')
)
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'MenuLinkId')
  BEGIN
	ALTER TABLE MenuLink ADD
	    MenuLinkId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='MenuLinkId') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='MenuLinkId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'MenuLinkId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Name')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Name VarChar(75) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Name') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Name' and character_maximum_length=75 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'Name', 'VarChar(75)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Target')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Target VarChar(150) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Target') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Target' and character_maximum_length=150 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'Target', 'VarChar(150)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Active')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Active Bit NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Active') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Active' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'Active', 'Bit', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'MenuLinkGroupId')
  BEGIN
	ALTER TABLE MenuLink ADD
	    MenuLinkGroupId Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='MenuLinkGroupId') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='MenuLinkGroupId' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'MenuLinkGroupId', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'ParentMenuLinkId')
  BEGIN
	ALTER TABLE MenuLink ADD
	    ParentMenuLinkId Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='ParentMenuLinkId') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='ParentMenuLinkId' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'ParentMenuLinkId', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'EffectiveDate')
  BEGIN
	ALTER TABLE MenuLink ADD
	    EffectiveDate DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='EffectiveDate') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='EffectiveDate' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'EffectiveDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'ExpirationDate')
  BEGIN
	ALTER TABLE MenuLink ADD
	    ExpirationDate DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='ExpirationDate') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='ExpirationDate' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'ExpirationDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Sequence')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Sequence Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Sequence') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='Sequence' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MenuLink 'MenuLink', 'Sequence', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'TargetWindow')
  BEGIN
	ALTER TABLE MenuLink ADD
	    TargetWindow VarChar(50) NOT NULL
	CONSTRAINT
	    [DF_MenuLink_TargetWindow] DEFAULT '_self' WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLink' and column_name='TargetWindow') and not exists(select * from information_schema.columns where table_name='MenuLink' and column_name='TargetWindow' and character_maximum_length=50 and is_nullable='YES')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('MenuLink') and name = 'TargetWindow'

	if @cdefault is not null
		exec('alter table MenuLink DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_MenuLink_TargetWindow' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_MenuLink_TargetWindow' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_MenuLink_TargetWindow]')
          end
	
	exec #spAlterColumn_MenuLink 'MenuLink', 'TargetWindow', 'VarChar(50)', 0
	if not exists(select * from sysobjects where name = 'DF_MenuLink_TargetWindow' and xtype='D')
		alter table MenuLink
			ADD CONSTRAINT [DF_MenuLink_TargetWindow] DEFAULT '_self' FOR TargetWindow WITH VALUES
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_MenuLink') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE MenuLink WITH NOCHECK ADD
	CONSTRAINT PK_MenuLink PRIMARY KEY NONCLUSTERED
	(
		MenuLinkId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_MenuLink_MenuLinkGroup') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE MenuLink ADD
	CONSTRAINT FK_MenuLink_MenuLinkGroup FOREIGN KEY
	(
		MenuLinkGroupId
	)
	REFERENCES MenuLinkGroup
	(
		MenuLinkGroupId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_MenuLink_ParentMenuLink') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE MenuLink ADD
	CONSTRAINT FK_MenuLink_ParentMenuLink FOREIGN KEY
	(
		ParentMenuLinkId
	)
	REFERENCES MenuLink
	(
		MenuLinkId
	)
GO


drop procedure #spAlterColumn_MenuLink
GO

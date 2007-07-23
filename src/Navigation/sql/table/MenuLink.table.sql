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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MenuLink]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE MenuLink (
	MenuLinkId Int IDENTITY(1,1) NOT NULL,
	Name VarChar(75) NOT NULL,
	Target VarChar(150) NOT NULL,
	Active Bit NOT NULL,
	MenuLinkGroupId Int NULL,
	ParentMenuLinkId Int NULL,
	EffectiveDate DateTime NULL,
	ExpirationDate DateTime NULL
)
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'MenuLinkId')
  BEGIN
	ALTER TABLE MenuLink ADD
	    MenuLinkId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'MenuLinkId')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'MenuLinkId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Name')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Name VarChar(75) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Name')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'Name', 'VarChar(75)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Target')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Target VarChar(150) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Target')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'Target', 'VarChar(150)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Active')
  BEGIN
	ALTER TABLE MenuLink ADD
	    Active Bit NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'Active')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'Active', 'Bit', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'MenuLinkGroupId')
  BEGIN
	ALTER TABLE MenuLink ADD
	    MenuLinkGroupId Int NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'MenuLinkGroupId')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'MenuLinkGroupId', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'ParentMenuLinkId')
  BEGIN
	ALTER TABLE MenuLink ADD
	    ParentMenuLinkId Int NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'ParentMenuLinkId')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'ParentMenuLinkId', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'EffectiveDate')
  BEGIN
	ALTER TABLE MenuLink ADD
	    EffectiveDate DateTime NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'EffectiveDate')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'EffectiveDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLink') and name = 'ExpirationDate')
  BEGIN
	ALTER TABLE MenuLink ADD
	    ExpirationDate DateTime NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLink') and name = 'ExpirationDate')
  BEGIN
	exec #spAlterColumn 'MenuLink', 'ExpirationDate', 'DateTime', 0
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


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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MenuLinkKey]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE MenuLinkKey (
	MenuLinkId Int NOT NULL,
	[Key] VarChar(100) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('MenuLinkKey') and name = 'MenuLinkId')
  BEGIN
	ALTER TABLE MenuLinkKey ADD
	    MenuLinkId Int NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLinkKey') and name = 'MenuLinkId')
  BEGIN
	exec #spAlterColumn 'MenuLinkKey', 'MenuLinkId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLinkKey') and name = 'Key')
  BEGIN
	ALTER TABLE MenuLinkKey ADD
	    [Key] VarChar(100) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MenuLinkKey') and name = 'Key')
  BEGIN
	exec #spAlterColumn 'MenuLinkKey', 'Key', 'VarChar(100)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_MenuLinkKey') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE MenuLinkKey WITH NOCHECK ADD
	CONSTRAINT PK_MenuLinkKey PRIMARY KEY NONCLUSTERED
	(
		MenuLinkId
,
		[Key]
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_MenuLinkKey_MenuLink') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE MenuLinkKey ADD
	CONSTRAINT FK_MenuLinkKey_MenuLink FOREIGN KEY
	(
		MenuLinkId
	)
	REFERENCES MenuLink
	(
		MenuLinkId
	)
GO


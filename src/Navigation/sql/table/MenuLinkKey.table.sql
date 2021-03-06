SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_MenuLinkKey%' and xtype='P')
drop procedure #spAlterColumn_MenuLinkKey
GO

CREATE PROCEDURE #spAlterColumn_MenuLinkKey
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
CREATE TABLE dbo.MenuLinkKey (
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


if exists(select * from information_schema.columns where table_name='MenuLinkKey' and column_name='MenuLinkId') and not exists(select * from information_schema.columns where table_name='MenuLinkKey' and column_name='MenuLinkId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLinkKey 'MenuLinkKey', 'MenuLinkId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MenuLinkKey') and name = 'Key')
  BEGIN
	ALTER TABLE MenuLinkKey ADD
	    [Key] VarChar(100) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MenuLinkKey' and column_name='Key') and not exists(select * from information_schema.columns where table_name='MenuLinkKey' and column_name='Key' and character_maximum_length=100 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MenuLinkKey 'MenuLinkKey', 'Key', 'VarChar(100)', 1
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


drop procedure #spAlterColumn_MenuLinkKey
GO

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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MailMessageRoute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE MailMessageRoute (
	MailMessageRouteId Int IDENTITY(1,1) NOT NULL,
	MailMessage VarChar(50) NOT NULL,
	RoutingType VarChar(10) NOT NULL,
	Status VarChar(1) NOT NULL CONSTRAINT [DF_MailMessageRoute_Status] DEFAULT ('Y'),
	EmailAddress VarChar(200) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'MailMessageRouteId')
  BEGIN
	ALTER TABLE MailMessageRoute ADD
	    MailMessageRouteId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'MailMessageRouteId')
  BEGIN
	exec #spAlterColumn 'MailMessageRoute', 'MailMessageRouteId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'MailMessage')
  BEGIN
	ALTER TABLE MailMessageRoute ADD
	    MailMessage VarChar(50) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'MailMessage')
  BEGIN
	exec #spAlterColumn 'MailMessageRoute', 'MailMessage', 'VarChar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'RoutingType')
  BEGIN
	ALTER TABLE MailMessageRoute ADD
	    RoutingType VarChar(10) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'RoutingType')
  BEGIN
	exec #spAlterColumn 'MailMessageRoute', 'RoutingType', 'VarChar(10)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'Status')
  BEGIN
	ALTER TABLE MailMessageRoute ADD
	    Status VarChar(1) NOT NULL
	CONSTRAINT
	    [DF_MailMessageRoute_Status] DEFAULT 'Y' WITH VALUES
  END
GO

if exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'Status')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('MailMessageRoute') and name = 'Status'

	if @cdefault is not null
		exec('alter table MailMessageRoute DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_MailMessageRoute_Status' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_MailMessageRoute_Status' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_MailMessageRoute_Status]')
          end
	
	update MailMessageRoute set Status = 'Y' where Status IS NULL
	exec #spAlterColumn 'MailMessageRoute', 'Status', 'VarChar(1)', 1
	if not exists(select * from sysobjects where name = 'DF_MailMessageRoute_Status' and xtype='D')
		alter table MailMessageRoute
			ADD CONSTRAINT [DF_MailMessageRoute_Status] DEFAULT 'Y' FOR Status WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'EmailAddress')
  BEGIN
	ALTER TABLE MailMessageRoute ADD
	    EmailAddress VarChar(200) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('MailMessageRoute') and name = 'EmailAddress')
  BEGIN
	exec #spAlterColumn 'MailMessageRoute', 'EmailAddress', 'VarChar(200)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_MailMessageRoute') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE MailMessageRoute WITH NOCHECK ADD
	CONSTRAINT PK_MailMessageRoute PRIMARY KEY NONCLUSTERED
	(
		MailMessageRouteId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_MailMessageRoute') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE MailMessageRoute ADD
	CONSTRAINT UN_MailMessageRoute UNIQUE
	(
		MailMessage
,
		RoutingType
,
		EmailAddress
	)
GO


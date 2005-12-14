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

if not exists (select * from dbo.sysobjects where id = object_id(N'[AddressCache]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.AddressCache (
	AddressId Int IDENTITY(1,1) NOT NULL,
	Address1 VarChar(80) NULL,
	City VarChar(40) NULL,
	Region Char(2) NULL,
	PostalCode VarChar(10) NULL,
	Latitude Decimal(18, 8) NULL,
	Longitude Decimal(18, 8) NULL,
	Result VarChar(1000) NULL,
	Status VarChar(20) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'AddressId')
  BEGIN
	ALTER TABLE AddressCache ADD
	    AddressId Int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'AddressId')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'AddressId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Address1')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Address1 VarChar(80) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Address1')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Address1', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'City')
  BEGIN
	ALTER TABLE AddressCache ADD
	    City VarChar(40) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'City')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'City', 'VarChar(40)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Region')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Region Char(2) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Region')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Region', 'Char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'PostalCode')
  BEGIN
	ALTER TABLE AddressCache ADD
	    PostalCode VarChar(10) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'PostalCode')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'PostalCode', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Latitude')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Latitude Decimal(18, 8) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Latitude')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Latitude', 'Decimal(18, 8)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Longitude')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Longitude Decimal(18, 8) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Longitude')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Longitude', 'Decimal(18, 8)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Result')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Result VarChar(1000) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Result')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Result', 'VarChar(1000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Status')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Status VarChar(20) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Status')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Status', 'VarChar(20)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Address') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE AddressCache WITH NOCHECK ADD
	CONSTRAINT PK_Address PRIMARY KEY CLUSTERED
	(
		AddressId
	)
GO


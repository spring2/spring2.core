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
	City VarChar(50) NULL,
	Region VarChar(50) NULL,
	PostalCode VarChar(10) NULL,
	Latitude Decimal(18, 8) NULL,
	Longitude Decimal(18, 8) NULL,
	Result VarChar(8000) NULL,
	Status VarChar(20) NULL,
	StdAddress1 VarChar(80) NULL,
	StdCity VarChar(50) NULL,
	StdRegion VarChar(50) NULL,
	StdPostalCode VarChar(10) NULL,
	StdPlus4 VarChar(4) NULL,
	MatAddress1 VarChar(80) NULL,
	MatCity VarChar(50) NULL,
	MatRegion VarChar(50) NULL,
	MatPostalCode VarChar(10) NULL,
	MatchType Int NULL
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
	    City VarChar(50) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'City')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'City', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Region')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Region VarChar(50) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Region')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Region', 'VarChar(50)', 0
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
	    Result VarChar(8000) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Result')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Result', 'VarChar(8000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Status')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Status VarChar(20) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Status')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'Status', 'VarChar(20)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdAddress1')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdAddress1 VarChar(80) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdAddress1')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'StdAddress1', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdCity')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdCity VarChar(50) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdCity')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'StdCity', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdRegion')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdRegion VarChar(50) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdRegion')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'StdRegion', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdPostalCode')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdPostalCode VarChar(10) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdPostalCode')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'StdPostalCode', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdPlus4')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdPlus4 VarChar(4) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdPlus4')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'StdPlus4', 'VarChar(4)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatAddress1')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatAddress1 VarChar(80) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatAddress1')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'MatAddress1', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatCity')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatCity VarChar(50) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatCity')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'MatCity', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatRegion')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatRegion VarChar(50) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatRegion')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'MatRegion', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatPostalCode')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatPostalCode VarChar(10) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatPostalCode')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'MatPostalCode', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatchType')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatchType Int NULL
  END
GO

if exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatchType')
  BEGIN
	exec #spAlterColumn 'AddressCache', 'MatchType', 'Int', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Address') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE AddressCache WITH NOCHECK ADD
	CONSTRAINT PK_Address PRIMARY KEY CLUSTERED
	(
		AddressId
	)
GO


SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_AddressCache%' and xtype='P')
drop procedure #spAlterColumn_AddressCache
GO

CREATE PROCEDURE #spAlterColumn_AddressCache
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
	Result VarChar(8000) NULL,
	Status VarChar(20) NULL,
	StdAddress1 VarChar(80) NULL,
	StdCity VarChar(40) NULL,
	StdRegion Char(2) NULL,
	StdPostalCode VarChar(10) NULL,
	StdPlus4 Char(4) NULL,
	MatAddress1 VarChar(80) NULL,
	MatCity VarChar(40) NULL,
	MatRegion Char(2) NULL,
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


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='AddressId') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='AddressId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'AddressId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Address1')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Address1 VarChar(80) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Address1') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Address1' and character_maximum_length=80 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'Address1', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'City')
  BEGIN
	ALTER TABLE AddressCache ADD
	    City VarChar(40) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='City') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='City' and character_maximum_length=40 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'City', 'VarChar(40)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Region')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Region Char(2) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Region') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Region' and character_maximum_length=2 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'Region', 'Char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'PostalCode')
  BEGIN
	ALTER TABLE AddressCache ADD
	    PostalCode VarChar(10) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='PostalCode') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='PostalCode' and character_maximum_length=10 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'PostalCode', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Latitude')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Latitude Decimal(18, 8) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Latitude') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Latitude' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'Latitude', 'Decimal(18, 8)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Longitude')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Longitude Decimal(18, 8) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Longitude') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Longitude' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'Longitude', 'Decimal(18, 8)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Result')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Result VarChar(8000) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Result') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Result' and character_maximum_length=8000 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'Result', 'VarChar(8000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'Status')
  BEGIN
	ALTER TABLE AddressCache ADD
	    Status VarChar(20) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Status') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='Status' and character_maximum_length=20 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'Status', 'VarChar(20)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdAddress1')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdAddress1 VarChar(80) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdAddress1') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdAddress1' and character_maximum_length=80 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'StdAddress1', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdCity')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdCity VarChar(40) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdCity') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdCity' and character_maximum_length=40 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'StdCity', 'VarChar(40)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdRegion')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdRegion Char(2) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdRegion') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdRegion' and character_maximum_length=2 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'StdRegion', 'Char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdPostalCode')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdPostalCode VarChar(10) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdPostalCode') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdPostalCode' and character_maximum_length=10 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'StdPostalCode', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'StdPlus4')
  BEGIN
	ALTER TABLE AddressCache ADD
	    StdPlus4 Char(4) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdPlus4') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='StdPlus4' and character_maximum_length=4 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'StdPlus4', 'Char(4)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatAddress1')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatAddress1 VarChar(80) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatAddress1') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatAddress1' and character_maximum_length=80 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'MatAddress1', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatCity')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatCity VarChar(40) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatCity') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatCity' and character_maximum_length=40 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'MatCity', 'VarChar(40)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatRegion')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatRegion Char(2) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatRegion') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatRegion' and character_maximum_length=2 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'MatRegion', 'Char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatPostalCode')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatPostalCode VarChar(10) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatPostalCode') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatPostalCode' and character_maximum_length=10 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'MatPostalCode', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('AddressCache') and name = 'MatchType')
  BEGIN
	ALTER TABLE AddressCache ADD
	    MatchType Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatchType') and not exists(select * from information_schema.columns where table_name='AddressCache' and column_name='MatchType' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_AddressCache 'AddressCache', 'MatchType', 'Int', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Address') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE AddressCache WITH NOCHECK ADD
	CONSTRAINT PK_Address PRIMARY KEY CLUSTERED
	(
		AddressId
	)
GO


drop procedure #spAlterColumn_AddressCache
GO

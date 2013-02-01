SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_CurrencyExchange%' and xtype='P')
drop procedure #spAlterColumn_CurrencyExchange
GO

CREATE PROCEDURE #spAlterColumn_CurrencyExchange
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[CurrencyExchange]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.CurrencyExchange (
	CurrencyExchangeId Int IDENTITY(1,1) NOT NULL,
	CurrencyCode VarChar(3) NOT NULL,
	EffectiveDate DateTime NOT NULL CONSTRAINT [DF_CurrencyExchange_EffectiveDate] DEFAULT (getdate()),
	Rate Decimal(9, 5) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('CurrencyExchange') and name = 'CurrencyExchangeId')
  BEGIN
	ALTER TABLE CurrencyExchange ADD
	    CurrencyExchangeId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='CurrencyExchangeId') and not exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='CurrencyExchangeId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_CurrencyExchange 'CurrencyExchange', 'CurrencyExchangeId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CurrencyExchange') and name = 'CurrencyCode')
  BEGIN
	ALTER TABLE CurrencyExchange ADD
	    CurrencyCode VarChar(3) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='CurrencyCode') and not exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='CurrencyCode' and character_maximum_length=3 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_CurrencyExchange 'CurrencyExchange', 'CurrencyCode', 'VarChar(3)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('CurrencyExchange') and name = 'EffectiveDate')
  BEGIN
	ALTER TABLE CurrencyExchange ADD
	    EffectiveDate DateTime NOT NULL
	CONSTRAINT
	    [DF_CurrencyExchange_EffectiveDate] DEFAULT getdate() WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='EffectiveDate') and not exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='EffectiveDate' and character_maximum_length=40 and is_nullable='NO')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('CurrencyExchange') and name = 'EffectiveDate'

	if @cdefault is not null
		exec('alter table CurrencyExchange DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_CurrencyExchange_EffectiveDate' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_CurrencyExchange_EffectiveDate' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_CurrencyExchange_EffectiveDate]')
          end
	
	update CurrencyExchange set EffectiveDate = getdate() where EffectiveDate IS NULL
	exec #spAlterColumn_CurrencyExchange 'CurrencyExchange', 'EffectiveDate', 'DateTime', 1
	if not exists(select * from sysobjects where name = 'DF_CurrencyExchange_EffectiveDate' and xtype='D')
		alter table CurrencyExchange
			ADD CONSTRAINT [DF_CurrencyExchange_EffectiveDate] DEFAULT getdate() FOR EffectiveDate WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('CurrencyExchange') and name = 'Rate')
  BEGIN
	ALTER TABLE CurrencyExchange ADD
	    Rate Decimal(9, 5) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='Rate') and not exists(select * from information_schema.columns where table_name='CurrencyExchange' and column_name='Rate' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_CurrencyExchange 'CurrencyExchange', 'Rate', 'Decimal(9, 5)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Currency') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE CurrencyExchange WITH NOCHECK ADD
	CONSTRAINT PK_Currency PRIMARY KEY CLUSTERED
	(
		CurrencyExchangeId
	)
GO


drop procedure #spAlterColumn_CurrencyExchange
GO

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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Log (
	LogId int IDENTITY(1,1) NOT NULL,
	Date datetime NOT NULL,
	Thread varchar(255) NULL,
	Level varchar(50) NOT NULL,
	Logger varchar(255) NOT NULL,
	Message varchar(4000) NOT NULL,
	Exception varchar(2000) NULL,
	Machine varchar(25) NULL,
	UserName varchar(100) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'LogId')
  BEGIN
	ALTER TABLE Log ADD
	    LogId int IDENTITY(1,1) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'LogId')
  BEGIN
	exec #spAlterColumn 'Log', 'LogId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Date')
  BEGIN
	ALTER TABLE Log ADD
	    Date datetime NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Date')
  BEGIN
	exec #spAlterColumn 'Log', 'Date', 'datetime', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Thread')
  BEGIN
	ALTER TABLE Log ADD
	    Thread varchar(255) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Thread')
  BEGIN
	exec #spAlterColumn 'Log', 'Thread', 'varchar(255)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Level')
  BEGIN
	ALTER TABLE Log ADD
	    Level varchar(50) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Level')
  BEGIN
	exec #spAlterColumn 'Log', 'Level', 'varchar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Logger')
  BEGIN
	ALTER TABLE Log ADD
	    Logger varchar(255) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Logger')
  BEGIN
	exec #spAlterColumn 'Log', 'Logger', 'varchar(255)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Message')
  BEGIN
	ALTER TABLE Log ADD
	    Message varchar(4000) NOT NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Message')
  BEGIN
	exec #spAlterColumn 'Log', 'Message', 'varchar(4000)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Exception')
  BEGIN
	ALTER TABLE Log ADD
	    Exception varchar(2000) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Exception')
  BEGIN
	exec #spAlterColumn 'Log', 'Exception', 'varchar(2000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'Machine')
  BEGIN
	ALTER TABLE Log ADD
	    Machine varchar(25) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'UserName')
  BEGIN
	exec #spAlterColumn 'Log', 'UserName', 'varchar(100)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Log') and name = 'UserName')
  BEGIN
	ALTER TABLE Log ADD
	    UserName varchar(100) NULL
  END
GO

if exists(select * from syscolumns where id=object_id('Log') and name = 'Machine')
  BEGIN
	exec #spAlterColumn 'Log', 'Machine', 'varchar(25)', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Log') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Log WITH NOCHECK ADD
	CONSTRAINT PK_Log PRIMARY KEY NONCLUSTERED
	(
		LogId
	)
GO

if not exists (select * from sysindexes where name='IxLog_Date' and id=object_id(N'[Log]'))
	CREATE INDEX IxLog_Date ON Log
	(
        	Date
	)
GO
if not exists (select * from sysindexes where name='ixLog_DateLogId' and id=object_id(N'[Log]'))
	CREATE INDEX ixLog_DateLogId ON Log
	(
        	Date,        	LogId
	)
GO

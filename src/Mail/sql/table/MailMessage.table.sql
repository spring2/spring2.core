SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_MailMessage%' and xtype='P')
drop procedure #spAlterColumn_MailMessage
GO

CREATE PROCEDURE #spAlterColumn_MailMessage
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MailMessage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.MailMessage (
	MailMessageId Int IDENTITY(1,1) NOT NULL,
	ScheduleTime DateTime NULL,
	ProcessedTime DateTime NULL,
	Priority VarChar(10) NULL,
	[From] VarChar(250) NULL,
	[To] VarChar(6000) NULL,
	Cc VarChar(250) NULL,
	Bcc VarChar(250) NULL,
	Subject VarChar(80) NULL,
	BodyFormat VarChar(10) NULL,
	Body Text NULL,
	MailMessageStatus VarChar(30) NOT NULL,
	ReleasedByUserId Int NULL,
	MailMessageType VarChar(80) NOT NULL CONSTRAINT [DF_MailMessage_MailMessageType] DEFAULT (''),
	NumberOfAttempts Int NULL,
	MessageQueueDate DateTime NULL,
	ReferenceKey VarChar(50) NULL,
	UniqueKey VarChar(50) NULL,
	Checksum VarChar(50) NULL,
	OpenCount Int NOT NULL CONSTRAINT [DF_MailMessage_OpenCount] DEFAULT (0),
	Bounces Int NOT NULL CONSTRAINT [DF_MailMessage_Bounces] DEFAULT (0),
	LastOpenDate DateTime NULL,
	SmtpServer VarChar(50) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'MailMessageId')
  BEGIN
	ALTER TABLE MailMessage ADD
	    MailMessageId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MailMessageId') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MailMessageId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'MailMessageId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'ScheduleTime')
  BEGIN
	ALTER TABLE MailMessage ADD
	    ScheduleTime DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ScheduleTime') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ScheduleTime' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'ScheduleTime', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'ProcessedTime')
  BEGIN
	ALTER TABLE MailMessage ADD
	    ProcessedTime DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ProcessedTime') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ProcessedTime' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'ProcessedTime', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Priority')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Priority VarChar(10) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Priority') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Priority' and character_maximum_length=10 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'Priority', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'From')
  BEGIN
	ALTER TABLE MailMessage ADD
	    [From] VarChar(250) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='From') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='From' and character_maximum_length=250 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'From', 'VarChar(250)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'To')
  BEGIN
	ALTER TABLE MailMessage ADD
	    [To] VarChar(6000) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='To') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='To' and character_maximum_length=6000 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'To', 'VarChar(6000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Cc')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Cc VarChar(250) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Cc') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Cc' and character_maximum_length=250 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'Cc', 'VarChar(250)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Bcc')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Bcc VarChar(250) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Bcc') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Bcc' and character_maximum_length=250 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'Bcc', 'VarChar(250)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Subject')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Subject VarChar(80) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Subject') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Subject' and character_maximum_length=80 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'Subject', 'VarChar(80)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'BodyFormat')
  BEGIN
	ALTER TABLE MailMessage ADD
	    BodyFormat VarChar(10) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='BodyFormat') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='BodyFormat' and character_maximum_length=10 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'BodyFormat', 'VarChar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Body')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Body Text NULL
  END
GO
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'MailMessageStatus')
  BEGIN
	ALTER TABLE MailMessage ADD
	    MailMessageStatus VarChar(30) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MailMessageStatus') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MailMessageStatus' and character_maximum_length=30 and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'MailMessageStatus', 'VarChar(30)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'ReleasedByUserId')
  BEGIN
	ALTER TABLE MailMessage ADD
	    ReleasedByUserId Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ReleasedByUserId') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ReleasedByUserId' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'ReleasedByUserId', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'MailMessageType')
  BEGIN
	ALTER TABLE MailMessage ADD
	    MailMessageType VarChar(80) NOT NULL
	CONSTRAINT
	    [DF_MailMessage_MailMessageType] DEFAULT '' WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MailMessageType') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MailMessageType' and character_maximum_length=80 and is_nullable='NO')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('MailMessage') and name = 'MailMessageType'

	if @cdefault is not null
		exec('alter table MailMessage DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_MailMessage_MailMessageType' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_MailMessage_MailMessageType' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_MailMessage_MailMessageType]')
          end
	
	update MailMessage set MailMessageType = '' where MailMessageType IS NULL
	exec #spAlterColumn_MailMessage 'MailMessage', 'MailMessageType', 'VarChar(80)', 1
	if not exists(select * from sysobjects where name = 'DF_MailMessage_MailMessageType' and xtype='D')
		alter table MailMessage
			ADD CONSTRAINT [DF_MailMessage_MailMessageType] DEFAULT '' FOR MailMessageType WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'NumberOfAttempts')
  BEGIN
	ALTER TABLE MailMessage ADD
	    NumberOfAttempts Int NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='NumberOfAttempts') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='NumberOfAttempts' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'NumberOfAttempts', 'Int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'MessageQueueDate')
  BEGIN
	ALTER TABLE MailMessage ADD
	    MessageQueueDate DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MessageQueueDate') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='MessageQueueDate' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'MessageQueueDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'ReferenceKey')
  BEGIN
	ALTER TABLE MailMessage ADD
	    ReferenceKey VarChar(50) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ReferenceKey') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='ReferenceKey' and character_maximum_length=50 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'ReferenceKey', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'UniqueKey')
  BEGIN
	ALTER TABLE MailMessage ADD
	    UniqueKey VarChar(50) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='UniqueKey') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='UniqueKey' and character_maximum_length=50 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'UniqueKey', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Checksum')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Checksum VarChar(50) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Checksum') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Checksum' and character_maximum_length=50 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'Checksum', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'OpenCount')
  BEGIN
	ALTER TABLE MailMessage ADD
	    OpenCount Int NOT NULL
	CONSTRAINT
	    [DF_MailMessage_OpenCount] DEFAULT 0 WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='OpenCount') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='OpenCount' and is_nullable='YES')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('MailMessage') and name = 'OpenCount'

	if @cdefault is not null
		exec('alter table MailMessage DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_MailMessage_OpenCount' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_MailMessage_OpenCount' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_MailMessage_OpenCount]')
          end
	
	exec #spAlterColumn_MailMessage 'MailMessage', 'OpenCount', 'Int', 0
	if not exists(select * from sysobjects where name = 'DF_MailMessage_OpenCount' and xtype='D')
		alter table MailMessage
			ADD CONSTRAINT [DF_MailMessage_OpenCount] DEFAULT 0 FOR OpenCount WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'Bounces')
  BEGIN
	ALTER TABLE MailMessage ADD
	    Bounces Int NOT NULL
	CONSTRAINT
	    [DF_MailMessage_Bounces] DEFAULT 0 WITH VALUES
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Bounces') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='Bounces' and is_nullable='YES')
  BEGIN
	declare @cdefault varchar(1000)
	select @cdefault = '[' + object_name(cdefault) + ']' from syscolumns where id=object_id('MailMessage') and name = 'Bounces'

	if @cdefault is not null
		exec('alter table MailMessage DROP CONSTRAINT ' + @cdefault)
		
	if exists(select * from sysobjects where name = 'DF_MailMessage_Bounces' and xtype='D')
          begin
            declare @table sysname
            select @table=object_name(parent_obj) from  sysobjects where (name = 'DF_MailMessage_Bounces' and xtype='D')
            exec('alter table ' + @table + ' DROP CONSTRAINT [DF_MailMessage_Bounces]')
          end
	
	exec #spAlterColumn_MailMessage 'MailMessage', 'Bounces', 'Int', 0
	if not exists(select * from sysobjects where name = 'DF_MailMessage_Bounces' and xtype='D')
		alter table MailMessage
			ADD CONSTRAINT [DF_MailMessage_Bounces] DEFAULT 0 FOR Bounces WITH VALUES
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'LastOpenDate')
  BEGIN
	ALTER TABLE MailMessage ADD
	    LastOpenDate DateTime NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='LastOpenDate') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='LastOpenDate' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'LastOpenDate', 'DateTime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailMessage') and name = 'SmtpServer')
  BEGIN
	ALTER TABLE MailMessage ADD
	    SmtpServer VarChar(50) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailMessage' and column_name='SmtpServer') and not exists(select * from information_schema.columns where table_name='MailMessage' and column_name='SmtpServer' and character_maximum_length=50 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailMessage 'MailMessage', 'SmtpServer', 'VarChar(50)', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_MailMessage') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE MailMessage WITH NOCHECK ADD
	CONSTRAINT PK_MailMessage PRIMARY KEY CLUSTERED
	(
		MailMessageId
	)
GO

if not exists (select * from sys.indexes where name='IxMailMessage_MailMessageStatus' and object_id=object_id(N'[MailMessage]'))
	CREATE INDEX IxMailMessage_MailMessageStatus ON MailMessage
	(
        	MailMessageStatus
	)
GO
if not exists (select * from sys.indexes where name='IxMailMessage_ScheduleTime' and object_id=object_id(N'[MailMessage]'))
	CREATE INDEX IxMailMessage_ScheduleTime ON MailMessage
	(
        	ScheduleTime
	)
GO
if not exists (select * from sys.indexes where name='IxMailMessage_ProcessedTime' and object_id=object_id(N'[MailMessage]'))
	CREATE INDEX IxMailMessage_ProcessedTime ON MailMessage
	(
        	ProcessedTime
	)
GO
if not exists (select * from sys.indexes where name='IxMailMessage_Subject' and object_id=object_id(N'[MailMessage]'))
	CREATE INDEX IxMailMessage_Subject ON MailMessage
	(
        	Subject
	)
GO
if not exists (select * from sys.indexes where name='IxMailMessage_UniqueKey' and object_id=object_id(N'[MailMessage]'))
	CREATE INDEX IxMailMessage_UniqueKey ON MailMessage
	(
        	UniqueKey
	)
GO

drop procedure #spAlterColumn_MailMessage
GO

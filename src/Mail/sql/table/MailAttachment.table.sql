SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn_MailAttachment%' and xtype='P')
drop procedure #spAlterColumn_MailAttachment
GO

CREATE PROCEDURE #spAlterColumn_MailAttachment
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

if not exists (select * from dbo.sysobjects where id = object_id(N'[MailAttachment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE dbo.MailAttachment (
	MailAttachmentId Int IDENTITY(1,1) NOT NULL,
	MailMessageId Int NOT NULL,
	Filename VarChar(50) NULL,
	Text Image NULL
)
GO

if not exists(select * from syscolumns where id=object_id('MailAttachment') and name = 'MailAttachmentId')
  BEGIN
	ALTER TABLE MailAttachment ADD
	    MailAttachmentId Int IDENTITY(1,1) NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='MailAttachmentId') and not exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='MailAttachmentId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MailAttachment 'MailAttachment', 'MailAttachmentId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailAttachment') and name = 'MailMessageId')
  BEGIN
	ALTER TABLE MailAttachment ADD
	    MailMessageId Int NOT NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='MailMessageId') and not exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='MailMessageId' and is_nullable='NO')
  BEGIN
	exec #spAlterColumn_MailAttachment 'MailAttachment', 'MailMessageId', 'Int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('MailAttachment') and name = 'Filename')
  BEGIN
	ALTER TABLE MailAttachment ADD
	    Filename VarChar(50) NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='Filename') and not exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='Filename' and character_maximum_length=50 and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailAttachment 'MailAttachment', 'Filename', 'VarChar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('MailAttachment') and name = 'Text')
  BEGIN
	ALTER TABLE MailAttachment ADD
	    Text Image NULL
  END
GO


if exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='Text') and not exists(select * from information_schema.columns where table_name='MailAttachment' and column_name='Text' and is_nullable='YES')
  BEGIN
	exec #spAlterColumn_MailAttachment 'MailAttachment', 'Text', 'Image', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_MailAttachment') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE MailAttachment WITH NOCHECK ADD
	CONSTRAINT PK_MailAttachment PRIMARY KEY NONCLUSTERED
	(
		MailAttachmentId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_MailAttachment_MailMessage') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE MailAttachment ADD
	CONSTRAINT FK_MailAttachment_MailMessage FOREIGN KEY
	(
		MailMessageId
	)
	REFERENCES MailMessage
	(
		MailMessageId
	)
GO


drop procedure #spAlterColumn_MailAttachment
GO

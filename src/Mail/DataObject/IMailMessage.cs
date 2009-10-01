using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;
using Spring2.Core.Types;


using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Mail.DataObject {

    public class MailMessageFields {
	private MailMessageFields() {}
	public static readonly String ENTITY_NAME = "MailMessage";
	
	public static readonly ColumnMetaData MAILMESSAGEID = new ColumnMetaData("MailMessageId", "MailMessageId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData SCHEDULETIME = new ColumnMetaData("ScheduleTime", "ScheduleTime", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData PROCESSEDTIME = new ColumnMetaData("ProcessedTime", "ProcessedTime", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData PRIORITY = new ColumnMetaData("Priority", "Priority", DbType.AnsiString, SqlDbType.VarChar, 10, 0, 0);
	public static readonly ColumnMetaData FROM = new ColumnMetaData("From", "From", DbType.AnsiString, SqlDbType.VarChar, 250, 0, 0);
	public static readonly ColumnMetaData TO = new ColumnMetaData("To", "To", DbType.AnsiString, SqlDbType.VarChar, 6000, 0, 0);
	public static readonly ColumnMetaData CC = new ColumnMetaData("Cc", "Cc", DbType.AnsiString, SqlDbType.VarChar, 250, 0, 0);
	public static readonly ColumnMetaData BCC = new ColumnMetaData("Bcc", "Bcc", DbType.AnsiString, SqlDbType.VarChar, 250, 0, 0);
	public static readonly ColumnMetaData SUBJECT = new ColumnMetaData("Subject", "Subject", DbType.AnsiString, SqlDbType.VarChar, 80, 0, 0);
	public static readonly ColumnMetaData BODYFORMAT = new ColumnMetaData("BodyFormat", "BodyFormat", DbType.AnsiString, SqlDbType.VarChar, 10, 0, 0);
	public static readonly ColumnMetaData BODY = new ColumnMetaData("Body", "Body", DbType.AnsiString, SqlDbType.Text, 0, 0, 0);
	public static readonly ColumnMetaData MAILMESSAGESTATUS = new ColumnMetaData("MailMessageStatus", "MailMessageStatus", DbType.AnsiString, SqlDbType.VarChar, 30, 0, 0);
	public static readonly ColumnMetaData RELEASEDBYUSERID = new ColumnMetaData("ReleasedByUserId", "ReleasedByUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData ATTACHMENTS = new ColumnMetaData("Attachments", "", DbType.Int32, SqlDbType.Int, 0, 0, 0);
	public static readonly ColumnMetaData MAILMESSAGETYPE = new ColumnMetaData("MailMessageType", "MailMessageType", DbType.AnsiString, SqlDbType.VarChar, 80, 0, 0);
	public static readonly ColumnMetaData NUMBEROFATTEMPTS = new ColumnMetaData("NumberOfAttempts", "NumberOfAttempts", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData MESSAGEQUEUEDATE = new ColumnMetaData("MessageQueueDate", "MessageQueueDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData REFERENCEKEY = new ColumnMetaData("ReferenceKey", "ReferenceKey", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
	public static readonly ColumnMetaData UNIQUEKEY = new ColumnMetaData("UniqueKey", "UniqueKey", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
	public static readonly ColumnMetaData CHECKSUM = new ColumnMetaData("Checksum", "Checksum", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
	public static readonly ColumnMetaData OPENCOUNT = new ColumnMetaData("OpenCount", "OpenCount", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData BOUNCES = new ColumnMetaData("Bounces", "Bounces", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTOPENDATE = new ColumnMetaData("LastOpenDate", "LastOpenDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData SMTPSERVER = new ColumnMetaData("SmtpServer", "SmtpServer", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
    }

    public interface IMailMessage : IBusinessEntity {
	IdType MailMessageId {
	    get;
	}
	DateTimeType ScheduleTime {
	    get;
	}
	DateTimeType ProcessedTime {
	    get;
	}
	MailPriorityEnum Priority {
	    get;
	}
	StringType From {
	    get;
	}
	StringType To {
	    get;
	}
	StringType Cc {
	    get;
	}
	StringType Bcc {
	    get;
	}
	StringType Subject {
	    get;
	}
	MailBodyFormatEnum BodyFormat {
	    get;
	}
	StringType Body {
	    get;
	}
	MailMessageStatusEnum MailMessageStatus {
	    get;
	}
	IdType ReleasedByUserId {
	    get;
	}
	MailAttachmentList Attachments {
	    get;
	}
	StringType MailMessageType {
	    get;
	}
	IntegerType NumberOfAttempts {
	    get;
	}
	DateTimeType MessageQueueDate {
	    get;
	}
	StringType ReferenceKey {
	    get;
	}
	StringType UniqueKey {
	    get;
	}
	StringType Checksum {
	    get;
	}
	IntegerType OpenCount {
	    get;
	}
	IntegerType Bounces {
	    get;
	}
	DateTimeType LastOpenDate {
	    get;
	}
	StringType SmtpServer {
	    get;
	}
    }
}

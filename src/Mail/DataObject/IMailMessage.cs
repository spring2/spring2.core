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
	public static readonly String MAILMESSAGEID = "MailMessageId";
	public static readonly String SCHEDULETIME = "ScheduleTime";
	public static readonly String PROCESSEDTIME = "ProcessedTime";
	public static readonly String PRIORITY = "Priority";
	public static readonly String FROM = "From";
	public static readonly String TO = "To";
	public static readonly String CC = "Cc";
	public static readonly String BCC = "Bcc";
	public static readonly String SUBJECT = "Subject";
	public static readonly String BODYFORMAT = "BodyFormat";
	public static readonly String BODY = "Body";
	public static readonly String MAILMESSAGESTATUS = "MailMessageStatus";
	public static readonly String RELEASEDBYUSERID = "ReleasedByUserId";
	public static readonly String ATTACHMENTS = "Attachments";
	public static readonly String MAILMESSAGETYPE = "MailMessageType";
	public static readonly String NUMBEROFATTEMPTS = "NumberOfAttempts";
	public static readonly String MESSAGEQUEUEDATE = "MessageQueueDate";
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
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;



namespace Spring2.Core.Mail.DataObject {

    public class MailMessageData : Spring2.Core.DataObject.DataObject {

	public static readonly MailMessageData DEFAULT = new MailMessageData();

	private IdType mailMessageId = IdType.DEFAULT;
	private DateTimeType scheduleTime = DateTimeType.DEFAULT;
	private DateTimeType processedTime = DateTimeType.DEFAULT;
	private MailPriorityEnum priority = MailPriorityEnum.DEFAULT;
	private StringType from = StringType.DEFAULT;
	private StringType to = StringType.DEFAULT;
	private StringType cc = StringType.DEFAULT;
	private StringType bcc = StringType.DEFAULT;
	private StringType subject = StringType.DEFAULT;
	private MailBodyFormatEnum bodyFormat = MailBodyFormatEnum.DEFAULT;
	private StringType body = StringType.DEFAULT;
	private MailMessageStatusEnum mailMessageStatus = MailMessageStatusEnum.DEFAULT;
	private IdType releasedByUserId = IdType.DEFAULT;
	private MailAttachmentList attachments = MailAttachmentList.DEFAULT;
	private StringType mailMessageType = StringType.DEFAULT;
	private IntegerType numberOfAttempts = IntegerType.DEFAULT;
	private DateTimeType messageQueueDate = DateTimeType.DEFAULT;

	public IdType MailMessageId {
	    get { return this.mailMessageId; }
	    set { this.mailMessageId = value; }
	}

	public DateTimeType ScheduleTime {
	    get { return this.scheduleTime; }
	    set { this.scheduleTime = value; }
	}

	public DateTimeType ProcessedTime {
	    get { return this.processedTime; }
	    set { this.processedTime = value; }
	}

	public MailPriorityEnum Priority {
	    get { return this.priority; }
	    set { this.priority = value; }
	}

	public StringType From {
	    get { return this.from; }
	    set { this.from = value; }
	}

	public StringType To {
	    get { return this.to; }
	    set { this.to = value; }
	}

	public StringType Cc {
	    get { return this.cc; }
	    set { this.cc = value; }
	}

	public StringType Bcc {
	    get { return this.bcc; }
	    set { this.bcc = value; }
	}

	public StringType Subject {
	    get { return this.subject; }
	    set { this.subject = value; }
	}

	public MailBodyFormatEnum BodyFormat {
	    get { return this.bodyFormat; }
	    set { this.bodyFormat = value; }
	}

	public StringType Body {
	    get { return this.body; }
	    set { this.body = value; }
	}

	public MailMessageStatusEnum MailMessageStatus {
	    get { return this.mailMessageStatus; }
	    set { this.mailMessageStatus = value; }
	}

	public IdType ReleasedByUserId {
	    get { return this.releasedByUserId; }
	    set { this.releasedByUserId = value; }
	}

	public MailAttachmentList Attachments {
	    get { return this.attachments; }
	    set { this.attachments = value; }
	}

	public StringType MailMessageType {
	    get { return this.mailMessageType; }
	    set { this.mailMessageType = value; }
	}

	public IntegerType NumberOfAttempts {
	    get { return this.numberOfAttempts; }
	    set { this.numberOfAttempts = value; }
	}

	public DateTimeType MessageQueueDate {
	    get { return this.messageQueueDate; }
	    set { this.messageQueueDate = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}

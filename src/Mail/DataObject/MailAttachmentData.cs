using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Types;


using Spring2.Core.Mail.Types;


namespace Spring2.Core.Mail.DataObject {

    public class MailAttachmentData : Spring2.Core.DataObject.DataObject {

	public static readonly MailAttachmentData DEFAULT = new MailAttachmentData();

	private IdType mailAttachmentId = IdType.DEFAULT;
	private IdType mailMessageId = IdType.DEFAULT;
	private StringType filename = StringType.DEFAULT;
	private StringType text = StringType.DEFAULT;

	public IdType MailAttachmentId {
	    get { return this.mailAttachmentId; }
	    set { this.mailAttachmentId = value; }
	}

	public IdType MailMessageId {
	    get { return this.mailMessageId; }
	    set { this.mailMessageId = value; }
	}

	public StringType Filename {
	    get { return this.filename; }
	    set { this.filename = value; }
	}

	public StringType Text {
	    get { return this.text; }
	    set { this.text = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}

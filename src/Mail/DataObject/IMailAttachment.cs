using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Types;


using Spring2.Core.Mail.Types;

using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Mail.DataObject {

    public class MailAttachmentFields {
	private MailAttachmentFields() {}
	public static readonly String MAILATTACHMENT = "MailAttachment"; //Entity Name
	
	public static readonly String MAILATTACHMENTID = "MailAttachmentId";
	public static readonly String MAILMESSAGEID = "MailMessageId";
	public static readonly String FILENAME = "Filename";
	public static readonly String BUFFER = "Buffer";
    }

    public interface IMailAttachment : IBusinessEntity {
	IdType MailAttachmentId {
	    get;
	}
	IdType MailMessageId {
	    get;
	}
	StringType Filename {
	    get;
	}
	Byte[] Buffer {
	    get;
	}

	#region
	void WriteAttachment(String path);
	#endregion
    }
}

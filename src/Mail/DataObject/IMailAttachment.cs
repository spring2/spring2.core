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
	public static readonly String ENTITY_NAME = "MailAttachment";
	
	public static readonly ColumnMetaData MAILATTACHMENTID = new ColumnMetaData("MailAttachmentId", "MailAttachmentId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData MAILMESSAGEID = new ColumnMetaData("MailMessageId", "MailMessageId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData FILENAME = new ColumnMetaData("Filename", "Filename", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
	public static readonly ColumnMetaData BUFFER = new ColumnMetaData("Buffer", "Text", DbType.Binary, SqlDbType.Binary, 0, 0, 0);
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

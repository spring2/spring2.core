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

    public class MailMessageRouteFields {
	private MailMessageRouteFields() {}
	public static readonly String ENTITY_NAME = "MailMessageRoute";
	
	public static readonly ColumnMetaData MAILMESSAGEROUTEID = new ColumnMetaData("MailMessageRouteId", "MailMessageRouteId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData MAILMESSAGE = new ColumnMetaData("MailMessage", "MailMessage", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
	public static readonly ColumnMetaData ROUTINGTYPE = new ColumnMetaData("RoutingType", "RoutingType", DbType.AnsiString, SqlDbType.VarChar, 10, 0, 0);
	public static readonly ColumnMetaData STATUS = new ColumnMetaData("Status", "Status", DbType.AnsiString, SqlDbType.VarChar, 1, 0, 0);
	public static readonly ColumnMetaData EMAILADDRESS = new ColumnMetaData("EmailAddress", "EmailAddress", DbType.AnsiString, SqlDbType.VarChar, 200, 0, 0);
    }

    public interface IMailMessageRoute : IBusinessEntity {
	IdType MailMessageRouteId {
	    get;
	}
	StringType MailMessage {
	    get;
	}
	RoutingTypeEnum RoutingType {
	    get;
	}
	ActiveStatusEnum Status {
	    get;
	}
	StringType EmailAddress {
	    get;
	}
    }
}

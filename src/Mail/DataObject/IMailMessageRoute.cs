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
	public static readonly String MAILMESSAGEROUTEID = "MailMessageRouteId";
	public static readonly String MAILMESSAGE = "MailMessage";
	public static readonly String ROUTINGTYPE = "RoutingType";
	public static readonly String STATUS = "Status";
	public static readonly String EMAILADDRESS = "EmailAddress";
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

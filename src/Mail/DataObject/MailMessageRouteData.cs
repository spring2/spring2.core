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

    public class MailMessageRouteData : Spring2.Core.DataObject.DataObject {

	public static readonly MailMessageRouteData DEFAULT = new MailMessageRouteData();

	private IdType mailMessageRouteId = IdType.DEFAULT;
	private StringType mailMessage = StringType.DEFAULT;
	private RoutingTypeEnum routingType = RoutingTypeEnum.DEFAULT;
	private ActiveStatusEnum status = ActiveStatusEnum.DEFAULT;
	private StringType emailAddress = StringType.DEFAULT;

	public IdType MailMessageRouteId {
	    get { return this.mailMessageRouteId; }
	    set { this.mailMessageRouteId = value; }
	}

	public StringType MailMessage {
	    get { return this.mailMessage; }
	    set { this.mailMessage = value; }
	}

	public RoutingTypeEnum RoutingType {
	    get { return this.routingType; }
	    set { this.routingType = value; }
	}

	public ActiveStatusEnum Status {
	    get { return this.status; }
	    set { this.status = value; }
	}

	public StringType EmailAddress {
	    get { return this.emailAddress; }
	    set { this.emailAddress = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}

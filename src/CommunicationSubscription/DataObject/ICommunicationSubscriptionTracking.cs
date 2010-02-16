using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.CommunicationSubscription.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;



using Spring2.Core.BusinessEntity;

namespace Spring2.Core.CommunicationSubscription.DataObject {

    public class CommunicationSubscriptionTrackingFields {
	private CommunicationSubscriptionTrackingFields() {}
	public static readonly String ENTITY_NAME = "CommunicationSubscriptionTracking";
	
	public static readonly String COMMUNICATIONPRIMARYKEYID = "CommunicationPrimaryKeyId";
	public static readonly String COMMUNICATIONSUBSCRIPTIONTYPEID = "CommunicationSubscriptionTypeId";
	public static readonly String CREATEDATE = "CreateDate";
	public static readonly String CREATEUSERID = "CreateUserId";
	public static readonly String LASTMODIFIEDUSERID = "LastModifiedUserId";
	public static readonly String LASTMODIFIEDDATE = "LastModifiedDate";
    }

    public interface ICommunicationSubscriptionTracking : IBusinessEntity {
	IdType CommunicationPrimaryKeyId {
	    get;
	}
	IdType CommunicationSubscriptionTypeId {
	    get;
	}
	DateTimeType CreateDate {
	    get;
	}
	IdType CreateUserId {
	    get;
	}
	IdType LastModifiedUserId {
	    get;
	}
	DateTimeType LastModifiedDate {
	    get;
	}
    }
}

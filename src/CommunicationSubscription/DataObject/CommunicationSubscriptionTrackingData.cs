using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.CommunicationSubscription.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;



namespace Spring2.Core.CommunicationSubscription.DataObject {

    public class CommunicationSubscriptionTrackingData : Spring2.Core.DataObject.DataObject {

	public static readonly CommunicationSubscriptionTrackingData DEFAULT = new CommunicationSubscriptionTrackingData();

	private IdType communicationSubscriptionTypeId = IdType.DEFAULT;
	private DateTimeType createDate = DateTimeType.DEFAULT;
	private IdType createUserId = IdType.DEFAULT;

	public IdType CommunicationSubscriptionTypeId {
	    get { return this.communicationSubscriptionTypeId; }
	    set { this.communicationSubscriptionTypeId = value; }
	}

	public DateTimeType CreateDate {
	    get { return this.createDate; }
	    set { this.createDate = value; }
	}

	public IdType CreateUserId {
	    get { return this.createUserId; }
	    set { this.createUserId = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}

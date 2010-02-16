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
	
	public static readonly ColumnMetaData COMMUNICATIONPRIMARYKEYID = new ColumnMetaData("CommunicationPrimaryKeyId", "CommunicationPrimaryKeyId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData COMMUNICATIONSUBSCRIPTIONTYPEID = new ColumnMetaData("CommunicationSubscriptionTypeId", "CommunicationSubscriptionTypeId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData CREATEDATE = new ColumnMetaData("CreateDate", "CreateDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData CREATEUSERID = new ColumnMetaData("CreateUserId", "CreateUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTMODIFIEDUSERID = new ColumnMetaData("LastModifiedUserId", "LastModifiedUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTMODIFIEDDATE = new ColumnMetaData("LastModifiedDate", "LastModifiedDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
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

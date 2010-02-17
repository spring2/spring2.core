using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.Publication.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;




using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Publication.DataObject {

    public class PublicationTrackingFields {
	private PublicationTrackingFields() {}
	public static readonly String ENTITY_NAME = "PublicationTracking";
	
	public static readonly ColumnMetaData PUBLICATIONPRIMARYKEYID = new ColumnMetaData("PublicationPrimaryKeyId", "PublicationPrimaryKeyId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData PUBLICATIONTYPEID = new ColumnMetaData("PublicationTypeId", "PublicationTypeId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData CREATEDATE = new ColumnMetaData("CreateDate", "CreateDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData CREATEUSERID = new ColumnMetaData("CreateUserId", "CreateUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTMODIFIEDUSERID = new ColumnMetaData("LastModifiedUserId", "LastModifiedUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTMODIFIEDDATE = new ColumnMetaData("LastModifiedDate", "LastModifiedDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
    }

    public interface IPublicationTracking : IBusinessEntity {
	IdType PublicationPrimaryKeyId {
	    get;
	}
	IdType PublicationTypeId {
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

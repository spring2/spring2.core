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

    public class PublicationTypeFields {
	private PublicationTypeFields() {}
	public static readonly String ENTITY_NAME = "PublicationType";
	
	public static readonly ColumnMetaData PUBLICATIONTYPEID = new ColumnMetaData("PublicationTypeId", "PublicationTypeId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData NAME = new ColumnMetaData("Name", "Name", DbType.AnsiString, SqlDbType.VarChar, 50, 0, 0);
	public static readonly ColumnMetaData EMAILSUBJECT = new ColumnMetaData("EmailSubject", "EmailSubject", DbType.AnsiString, SqlDbType.VarChar, 100, 0, 0);
	public static readonly ColumnMetaData EMAILBODY = new ColumnMetaData("EmailBody", "EmailBody", DbType.AnsiString, SqlDbType.Text, 0, 0, 0);
	public static readonly ColumnMetaData EMAILBODYTYPE = new ColumnMetaData("EmailBodyType", "EmailBodyType", DbType.AnsiString, SqlDbType.VarChar, 100, 0, 0);
	public static readonly ColumnMetaData MAILMESSAGETYPE = new ColumnMetaData("MailMessageType", "MailMessageType", DbType.AnsiString, SqlDbType.VarChar, 100, 0, 0);
	public static readonly ColumnMetaData LASTSENTDATE = new ColumnMetaData("LastSentDate", "LastSentDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData FREQUENCYINMINUTES = new ColumnMetaData("FrequencyInMinutes", "FrequencyInMinutes", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData PROVIDERNAME = new ColumnMetaData("ProviderName", "ProviderName", DbType.AnsiString, SqlDbType.VarChar, 100, 0, 0);
	public static readonly ColumnMetaData ALLOWSUBSCRIPTION = new ColumnMetaData("AllowSubscription", "AllowSubscription", DbType.Boolean, SqlDbType.Bit, 0, 0, 0);
	public static readonly ColumnMetaData AUTOSUBSCRIBE = new ColumnMetaData("AutoSubscribe", "AutoSubscribe", DbType.Boolean, SqlDbType.Bit, 0, 0, 0);
	public static readonly ColumnMetaData EFFECTIVEDATE = new ColumnMetaData("EffectiveDate", "EffectiveDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData EXPIRATIONDATE = new ColumnMetaData("ExpirationDate", "ExpirationDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData CREATEDATE = new ColumnMetaData("CreateDate", "CreateDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
	public static readonly ColumnMetaData CREATEUSERID = new ColumnMetaData("CreateUserId", "CreateUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTMODIFIEDUSERID = new ColumnMetaData("LastModifiedUserId", "LastModifiedUserId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData LASTMODIFIEDDATE = new ColumnMetaData("LastModifiedDate", "LastModifiedDate", DbType.DateTime, SqlDbType.DateTime, 0, 0, 0);
    }

    public interface IPublicationType : IBusinessEntity {
	IdType PublicationTypeId {
	    get;
	}
	StringType Name {
	    get;
	}
	StringType EmailSubject {
	    get;
	}
	StringType EmailBody {
	    get;
	}
	StringType EmailBodyType {
	    get;
	}
	StringType MailMessageType {
	    get;
	}
	DateTimeType LastSentDate {
	    get;
	}
	IntegerType FrequencyInMinutes {
	    get;
	}
	StringType ProviderName {
	    get;
	}
	BooleanType AllowSubscription {
	    get;
	}
	BooleanType AutoSubscribe {
	    get;
	}
	DateTimeType EffectiveDate {
	    get;
	}
	DateTimeType ExpirationDate {
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

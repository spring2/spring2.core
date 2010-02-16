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

    public class CommunicationSubscriptionTypeFields {
	private CommunicationSubscriptionTypeFields() {}
	public static readonly String ENTITY_NAME = "CommunicationSubscriptionType";
	
	public static readonly String COMMUNICATIONSUBSCRIPTIONTYPEID = "CommunicationSubscriptionTypeId";
	public static readonly String NAME = "Name";
	public static readonly String EMAILSUBJECT = "EmailSubject";
	public static readonly String EMAILBODY = "EmailBody";
	public static readonly String EMAILBODYTYPE = "EmailBodyType";
	public static readonly String MAILMESSAGETYPE = "MailMessageType";
	public static readonly String LASTSENTDATE = "LastSentDate";
	public static readonly String FREQUENCYINMINUTES = "FrequencyInMinutes";
	public static readonly String PROVIDERNAME = "ProviderName";
	public static readonly String ALLOWSUBSCRIPTION = "AllowSubscription";
	public static readonly String AUTOSUBSCRIBE = "AutoSubscribe";
	public static readonly String EFFECTIVEDATE = "EffectiveDate";
	public static readonly String EXPIRATIONDATE = "ExpirationDate";
	public static readonly String CREATEDATE = "CreateDate";
	public static readonly String CREATEUSERID = "CreateUserId";
	public static readonly String LASTMODIFIEDUSERID = "LastModifiedUserId";
	public static readonly String LASTMODIFIEDDATE = "LastModifiedDate";
    }

    public interface ICommunicationSubscriptionType : IBusinessEntity {
	IdType CommunicationSubscriptionTypeId {
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

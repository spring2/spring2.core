using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.CommunicationSubscription.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;





namespace Spring2.Core.CommunicationSubscription.DataObject {

    public class CommunicationSubscriptionTypeData : Spring2.Core.DataObject.DataObject {

	public static readonly CommunicationSubscriptionTypeData DEFAULT = new CommunicationSubscriptionTypeData();

	private StringType name = StringType.DEFAULT;
	private StringType emailSubject = StringType.DEFAULT;
	private StringType emailBody = StringType.DEFAULT;
	private StringType emailBodyType = StringType.DEFAULT;
	private StringType mailMessageType = StringType.DEFAULT;
	private DateTimeType lastSentDate = DateTimeType.DEFAULT;
	private IntegerType frequencyInMinutes = IntegerType.DEFAULT;
	private StringType providerName = StringType.DEFAULT;
	private BooleanType allowSubscription = BooleanType.DEFAULT;
	private BooleanType autoSubscribe = BooleanType.DEFAULT;
	private DateTimeType effectiveDate = DateTimeType.DEFAULT;
	private DateTimeType expirationDate = DateTimeType.DEFAULT;

	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public StringType EmailSubject {
	    get { return this.emailSubject; }
	    set { this.emailSubject = value; }
	}

	public StringType EmailBody {
	    get { return this.emailBody; }
	    set { this.emailBody = value; }
	}

	public StringType EmailBodyType {
	    get { return this.emailBodyType; }
	    set { this.emailBodyType = value; }
	}

	public StringType MailMessageType {
	    get { return this.mailMessageType; }
	    set { this.mailMessageType = value; }
	}

	public DateTimeType LastSentDate {
	    get { return this.lastSentDate; }
	    set { this.lastSentDate = value; }
	}

	public IntegerType FrequencyInMinutes {
	    get { return this.frequencyInMinutes; }
	    set { this.frequencyInMinutes = value; }
	}

	public StringType ProviderName {
	    get { return this.providerName; }
	    set { this.providerName = value; }
	}

	public BooleanType AllowSubscription {
	    get { return this.allowSubscription; }
	    set { this.allowSubscription = value; }
	}

	public BooleanType AutoSubscribe {
	    get { return this.autoSubscribe; }
	    set { this.autoSubscribe = value; }
	}

	public DateTimeType EffectiveDate {
	    get { return this.effectiveDate; }
	    set { this.effectiveDate = value; }
	}

	public DateTimeType ExpirationDate {
	    get { return this.expirationDate; }
	    set { this.expirationDate = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}

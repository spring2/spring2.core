using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Types;



namespace Spring2.Core.Publication.DataObject {

    public class PublicationTypeData : Spring2.Core.DataObject.DataObject {

	public static readonly PublicationTypeData DEFAULT = new PublicationTypeData();

	private StringType name = StringType.DEFAULT;
	private StringType description = StringType.DEFAULT;
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

	public StringType Description {
	    get { return this.description; }
	    set { this.description = value; }
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

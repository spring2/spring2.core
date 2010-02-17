using System;

using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Security;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Publication.Dao;
using Spring2.Core.Publication.DataObject;



namespace Spring2.Core.Publication.BusinessLogic {

    /// <summary>
    /// Publication Type
    /// </summary>
    public class PublicationType : Spring2.Core.BusinessEntity.BusinessEntity, IPublicationType {

	[Generate()]
	private IdType publicationTypeId = IdType.DEFAULT;
	[Generate()]
	private StringType name = StringType.DEFAULT;
	[Generate()]
	private StringType emailSubject = StringType.DEFAULT;
	[Generate()]
	private StringType emailBody = StringType.DEFAULT;
	[Generate()]
	private StringType emailBodyType = StringType.DEFAULT;
	[Generate()]
	private StringType mailMessageType = StringType.DEFAULT;
	[Generate()]
	private DateTimeType lastSentDate = DateTimeType.DEFAULT;
	[Generate()]
	private IntegerType frequencyInMinutes = IntegerType.DEFAULT;
	[Generate()]
	private StringType providerName = StringType.DEFAULT;
	[Generate()]
	private BooleanType allowSubscription = BooleanType.DEFAULT;
	[Generate()]
	private BooleanType autoSubscribe = BooleanType.DEFAULT;
	[Generate()]
	private DateTimeType effectiveDate = DateTimeType.DEFAULT;
	[Generate()]
	private DateTimeType expirationDate = DateTimeType.DEFAULT;
	[Generate()]
	private DateTimeType createDate = DateTimeType.DEFAULT;
	[Generate()]
	private IdType createUserId = IdType.DEFAULT;
	[Generate()]
	private IdType lastModifiedUserId = IdType.DEFAULT;
	[Generate()]
	private DateTimeType lastModifiedDate = DateTimeType.DEFAULT;

	[Generate()]
	internal PublicationType() {
	}

	[Generate()]
	internal PublicationType(Boolean isNew) {
	    this.isNew = isNew;
	}

	[Generate()]
	public static PublicationType NewInstance() {
	    return new PublicationType();
	}

	[Generate()]
	public static PublicationType GetInstance(IdType publicationTypeId) {
	    return PublicationTypeDAO.DAO.Load(publicationTypeId);
	}

	[Generate()]
	public void Update(PublicationTypeData data) {
	    name = data.Name.IsDefault ? name : data.Name;
	    emailSubject = data.EmailSubject.IsDefault ? emailSubject : data.EmailSubject;
	    emailBody = data.EmailBody.IsDefault ? emailBody : data.EmailBody;
	    emailBodyType = data.EmailBodyType.IsDefault ? emailBodyType : data.EmailBodyType;
	    mailMessageType = data.MailMessageType.IsDefault ? mailMessageType : data.MailMessageType;
	    lastSentDate = data.LastSentDate.IsDefault ? lastSentDate : data.LastSentDate;
	    frequencyInMinutes = data.FrequencyInMinutes.IsDefault ? frequencyInMinutes : data.FrequencyInMinutes;
	    providerName = data.ProviderName.IsDefault ? providerName : data.ProviderName;
	    allowSubscription = data.AllowSubscription.IsDefault ? allowSubscription : data.AllowSubscription;
	    autoSubscribe = data.AutoSubscribe.IsDefault ? autoSubscribe : data.AutoSubscribe;
	    effectiveDate = data.EffectiveDate.IsDefault ? effectiveDate : data.EffectiveDate;
	    expirationDate = data.ExpirationDate.IsDefault ? expirationDate : data.ExpirationDate;
	    Store();
	}

	[Generate()]
	public void Store() {
	    MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
	    }

	    SetAuditStamps();
	    if (isNew) {
		this.PublicationTypeId = PublicationTypeDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		PublicationTypeDAO.DAO.Update(this);
	    }
	}

	[Generate()]
	private void SetAuditStamps() {
	    IdType userId = IdType.DEFAULT;
	    if (System.Threading.Thread.CurrentPrincipal.Identity.Name.Length > 0) {
		userId = (System.Threading.Thread.CurrentPrincipal as IUserPrincipal).UserId;
	    }
	    if (IsNew) {
		this.CreateDate = DateTimeType.Now;
		this.CreateUserId = userId;
	    }
	    this.LastModifiedDate = DateTimeType.Now;
	    this.LastModifiedUserId = userId;
	}


	public MessageList Validate() {

	    MessageList errors = new MessageList();
	    if (Name.IsEmpty) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.NAME.Name));
	    }
	    if (EmailSubject.IsEmpty) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.EMAILSUBJECT.Name));
	    }
	    if (!LastSentDate.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.LASTSENTDATE.Name));
	    }
	    if (!FrequencyInMinutes.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.FREQUENCYINMINUTES.Name));
	    }
	    if (ProviderName.IsEmpty) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.PROVIDERNAME.Name));
	    }
	    if (!AllowSubscription.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.ALLOWSUBSCRIPTION.Name));
	    }
	    if (!AutoSubscribe.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.AUTOSUBSCRIBE.Name));
	    }
	    if (!EffectiveDate.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTypeFields.EFFECTIVEDATE.Name));
	    }

	    return errors;
	}


	[Generate()]
	public IdType PublicationTypeId {
	    get { return this.publicationTypeId; }
	    set { this.publicationTypeId = value; }
	}

	[Generate()]
	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	[Generate()]
	public StringType EmailSubject {
	    get { return this.emailSubject; }
	    set { this.emailSubject = value; }
	}

	[Generate()]
	public StringType EmailBody {
	    get { return this.emailBody; }
	    set { this.emailBody = value; }
	}

	[Generate()]
	public StringType EmailBodyType {
	    get { return this.emailBodyType; }
	    set { this.emailBodyType = value; }
	}

	[Generate()]
	public StringType MailMessageType {
	    get { return this.mailMessageType; }
	    set { this.mailMessageType = value; }
	}

	[Generate()]
	public DateTimeType LastSentDate {
	    get { return this.lastSentDate; }
	    set { this.lastSentDate = value; }
	}

	[Generate()]
	public IntegerType FrequencyInMinutes {
	    get { return this.frequencyInMinutes; }
	    set { this.frequencyInMinutes = value; }
	}

	[Generate()]
	public StringType ProviderName {
	    get { return this.providerName; }
	    set { this.providerName = value; }
	}

	[Generate()]
	public BooleanType AllowSubscription {
	    get { return this.allowSubscription; }
	    set { this.allowSubscription = value; }
	}

	[Generate()]
	public BooleanType AutoSubscribe {
	    get { return this.autoSubscribe; }
	    set { this.autoSubscribe = value; }
	}

	[Generate()]
	public DateTimeType EffectiveDate {
	    get { return this.effectiveDate; }
	    set { this.effectiveDate = value; }
	}

	[Generate()]
	public DateTimeType ExpirationDate {
	    get { return this.expirationDate; }
	    set { this.expirationDate = value; }
	}

	[Generate()]
	public DateTimeType CreateDate {
	    get { return this.createDate; }
	    set { this.createDate = value; }
	}

	[Generate()]
	public IdType CreateUserId {
	    get { return this.createUserId; }
	    set { this.createUserId = value; }
	}

	[Generate()]
	public IdType LastModifiedUserId {
	    get { return this.lastModifiedUserId; }
	    set { this.lastModifiedUserId = value; }
	}

	[Generate()]
	public DateTimeType LastModifiedDate {
	    get { return this.lastModifiedDate; }
	    set { this.lastModifiedDate = value; }
	}

	[Generate()]
	public void Reload() {
	    PublicationTypeDAO.DAO.Reload(this);
	}



	[Generate()]
	public override String ToString() {
	    return GetType().ToString() + "@" + PublicationTypeId.ToString();
	}

    }
}

using System;

using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Security;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.CommunicationSubscription.Dao;
using Spring2.Core.CommunicationSubscription.DataObject;



namespace Spring2.Core.CommunicationSubscription.BusinessLogic {

    /// <summary>
    /// Publication Tracking
    /// </summary>
    public class PublicationTracking : Spring2.Core.BusinessEntity.BusinessEntity, IPublicationTracking {

	[Generate()]
	private IdType publicationPrimaryKeyId = IdType.DEFAULT;
	[Generate()]
	private IdType publicationTypeId = IdType.DEFAULT;
	[Generate()]
	private DateTimeType createDate = DateTimeType.DEFAULT;
	[Generate()]
	private IdType createUserId = IdType.DEFAULT;
	[Generate()]
	private IdType lastModifiedUserId = IdType.DEFAULT;
	[Generate()]
	private DateTimeType lastModifiedDate = DateTimeType.DEFAULT;

	[Generate()]
	internal PublicationTracking() {
	}

	[Generate()]
	internal PublicationTracking(Boolean isNew) {
	    this.isNew = isNew;
	}

	[Generate()]
	public static PublicationTracking NewInstance() {
	    return new PublicationTracking();
	}

	[Generate()]
	public static PublicationTracking GetInstance(IdType publicationPrimaryKeyId) {
	    return PublicationTrackingDAO.DAO.Load(publicationPrimaryKeyId);
	}

	[Generate()]
	public void Update(PublicationTrackingData data) {
	    publicationTypeId = data.PublicationTypeId.IsDefault ? publicationTypeId : data.PublicationTypeId;
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
		this.PublicationPrimaryKeyId = PublicationTrackingDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		PublicationTrackingDAO.DAO.Update(this);
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
	    if (!PublicationTypeId.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTrackingFields.PUBLICATIONTYPEID.Name));
	    }
	    return errors;
	}


	[Generate()]
	public IdType PublicationPrimaryKeyId {
	    get { return this.publicationPrimaryKeyId; }
	    set { this.publicationPrimaryKeyId = value; }
	}

	[Generate()]
	public IdType PublicationTypeId {
	    get { return this.publicationTypeId; }
	    set { this.publicationTypeId = value; }
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
	    PublicationTrackingDAO.DAO.Reload(this);
	}



	[Generate()]
	public override String ToString() {
	    return GetType().ToString() + "@" + PublicationPrimaryKeyId.ToString();
	}

    }
}

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
	public static PublicationTracking GetInstance(IdType publicationTrackingId) {
	    return PublicationTrackingDAO.DAO.Load(publicationTrackingId);
	}

	[Generate()]
	public void Update(PublicationTrackingData data) {
	    publicationPrimaryKeyId = data.PublicationPrimaryKeyId.IsDefault ? publicationPrimaryKeyId : data.PublicationPrimaryKeyId;
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
		this.PublicationTrackingId = PublicationTrackingDAO.DAO.Insert(this);
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
	    if (!PublicationPrimaryKeyId.IsValid) {
		errors.Add(new MissingRequiredFieldError(PublicationTrackingFields.PUBLICATIONPRIMARYKEYID.Name));
	    }
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
	    publicationType = PublicationType.NewInstance();
	}



	[Generate()]
	public override String ToString() {
	    return GetType().ToString() + "@" + PublicationTrackingId.ToString();
	}

	[Generate()]
	private IdType publicationTrackingId = IdType.DEFAULT;
	[Generate()]
	private PublicationType publicationType = PublicationType.NewInstance();


	[Generate()]
	public IdType PublicationTrackingId {
	    get { return this.publicationTrackingId; }
	    set { this.publicationTrackingId = value; }
	}

	public PublicationType PublicationType {
	    get {
		if (publicationType.IsNew && PublicationTypeId.IsValid) {
		    publicationType = PublicationType.GetInstance(PublicationTypeId);
		}
		return this.publicationType as PublicationType;
	    }
	}

	[Generate()]
	IPublicationType IPublicationTracking.PublicationType {
	    get { return this.PublicationType; }
	}

    }
}

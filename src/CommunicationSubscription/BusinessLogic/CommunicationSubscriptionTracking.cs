using System;

using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.CommunicationSubscription.Dao;
using Spring2.Core.CommunicationSubscription.DataObject;


namespace Spring2.Core.CommunicationSubscription.BusinessLogic {

    /// <summary>
    /// Communication Subscription Tracking
    /// </summary>
    public class CommunicationSubscriptionTracking : Spring2.Core.BusinessEntity.BusinessEntity, ICommunicationSubscriptionTracking {

	[Generate()]
	private IdType communicationPrimaryKeyId = IdType.DEFAULT;
	[Generate()]
	private IdType communicationSubscriptionTypeId = IdType.DEFAULT;
	[Generate()]
	private DateTimeType createDate = DateTimeType.DEFAULT;
	[Generate()]
	private IdType createUserId = IdType.DEFAULT;
	[Generate()]
	private IdType lastModifiedUserId = IdType.DEFAULT;
	[Generate()]
	private DateTimeType lastModifiedDate = DateTimeType.DEFAULT;

	[Generate()]
	internal CommunicationSubscriptionTracking() {
	}

	[Generate()]
	internal CommunicationSubscriptionTracking(Boolean isNew) {
	    this.isNew = isNew;
	}

	[Generate()]
	public static CommunicationSubscriptionTracking NewInstance() {
	    return new CommunicationSubscriptionTracking();
	}

	[Generate()]
	public static CommunicationSubscriptionTracking GetInstance(IdType communicationPrimaryKeyId) {
	    return CommunicationSubscriptionTrackingDAO.DAO.Load(communicationPrimaryKeyId);
	}

	[Generate()]
	public void Update(CommunicationSubscriptionTrackingData data) {
	    communicationSubscriptionTypeId = data.CommunicationSubscriptionTypeId.IsDefault ? communicationSubscriptionTypeId : data.CommunicationSubscriptionTypeId;
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
		this.CommunicationPrimaryKeyId = CommunicationSubscriptionTrackingDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		CommunicationSubscriptionTrackingDAO.DAO.Update(this);
	    }
	}

	[Generate()]
	private void SetAuditStamps() {
	    IdType userId = IdType.DEFAULT;
	    if (System.Threading.Thread.CurrentPrincipal.Identity.Name.Length > 0) {
		//userId = (System.Threading.Thread.CurrentPrincipal as IUser).UserId;
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
	    if (!CommunicationSubscriptionTypeId.IsValid) {
		errors.Add(new MissingRequiredFieldError(CommunicationSubscriptionTrackingFields.COMMUNICATIONSUBSCRIPTIONTYPEID.Name));
	    }

	    return errors;
	}


	[Generate()]
	public IdType CommunicationPrimaryKeyId {
	    get { return this.communicationPrimaryKeyId; }
	    set { this.communicationPrimaryKeyId = value; }
	}

	[Generate()]
	public IdType CommunicationSubscriptionTypeId {
	    get { return this.communicationSubscriptionTypeId; }
	    set { this.communicationSubscriptionTypeId = value; }
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
	    CommunicationSubscriptionTrackingDAO.DAO.Reload(this);
	}



	[Generate()]
	public override String ToString() {
	    return GetType().ToString() + "@" + CommunicationPrimaryKeyId.ToString();
	}

    }
}

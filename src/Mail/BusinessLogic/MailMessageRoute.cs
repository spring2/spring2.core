using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.Mail.Dao;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.BusinessLogic {


    public class MailMessageRoute : Spring2.Core.BusinessEntity.BusinessEntity, IMailMessageRoute {

	[Generate()]
	private IdType mailMessageRouteId = IdType.DEFAULT;
	[Generate()]
	private StringType mailMessage = StringType.DEFAULT;
	[Generate()]
	private RoutingTypeEnum routingType = RoutingTypeEnum.DEFAULT;
	[Generate()]
	private ActiveStatusEnum status = ActiveStatusEnum.DEFAULT;
	[Generate()]
	private StringType emailAddress = StringType.DEFAULT;

	[Generate()]
	internal MailMessageRoute() {
	}

	[Generate()]
	internal MailMessageRoute(Boolean isNew) {
	    this.isNew = isNew;
	}


	[Generate()]
	public IdType MailMessageRouteId {
	    get { return this.mailMessageRouteId; }
	    set { this.mailMessageRouteId = value; }
	}

	[Generate()]
	public StringType MailMessage {
	    get { return this.mailMessage; }
	    set { this.mailMessage = value; }
	}

	[Generate()]
	public RoutingTypeEnum RoutingType {
	    get { return this.routingType; }
	    set { this.routingType = value; }
	}

	[Generate()]
	public ActiveStatusEnum Status {
	    get { return this.status; }
	    set { this.status = value; }
	}

	[Generate()]
	public StringType EmailAddress {
	    get { return this.emailAddress; }
	    set { this.emailAddress = value; }
	}

	[Generate()]
	public static MailMessageRoute NewInstance() {
	    return new MailMessageRoute();
	}

	[Generate()]
	public static MailMessageRoute GetInstance(IdType mailMessageRouteId) {
	    return MailMessageRouteDAO.DAO.Load(mailMessageRouteId);
	}

	[Generate()]
	public void Update(MailMessageRouteData data) {
	    mailMessageRouteId = data.MailMessageRouteId.IsDefault ? mailMessageRouteId : data.MailMessageRouteId;
	    mailMessage = data.MailMessage.IsDefault ? mailMessage : data.MailMessage;
	    routingType = data.RoutingType.IsDefault ? routingType : data.RoutingType;
	    status = data.Status.IsDefault ? status : data.Status;
	    emailAddress = data.EmailAddress.IsDefault ? emailAddress : data.EmailAddress;
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

	    if (isNew) {
		this.MailMessageRouteId = MailMessageRouteDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		MailMessageRouteDAO.DAO.Update(this);
	    }
	}

	[Generate()]
	public virtual MessageList Validate() {

	    MessageList errors = new MessageList();
	    return errors;
	}

	[Generate()]
	public void Reload() {
	    MailMessageRouteDAO.DAO.Reload(this);
	}
    }
}

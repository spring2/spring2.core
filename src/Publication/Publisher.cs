using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.Types;
using NVelocity.App;
using NVelocity;
using System.IO;
using Spring2.Core.Mail.Types;
using Spring2.Core.Mail.BusinessLogic;

namespace Spring2.Core.Publication {
    public abstract class Publisher : IPublisher {
	public virtual void Process(IPublicationType publicationType) {
	    UpdatePublicationTypeLastSent(publicationType as PublicationType);
	}

	protected virtual void UpdatePublicationTypeLastSent(PublicationType publicationType) {
	    publicationType.Update(new PublicationTypeData() { LastSentDate = DateTimeType.Now });
	}

	private static VelocityEngine velocity = null;
	private static VelocityEngine Velocity {
	    get {
		if(velocity == null) {
		    velocity = new VelocityEngine();
		    velocity.SetProperty("runtime.log.logsystem.log4net.category", "NVelocity");
		    velocity.Init();
		}
		return velocity;
	    }
	}

	protected void SendPublicationMail(StringType emailAddress, VelocityContext context, String emailSubject, String emailBody, String mailMessageType, MailBodyFormatEnum bodyType) {
	    // merge the template and context
	    StringWriter writer = new StringWriter();
	    Velocity.Evaluate(context, writer, mailMessageType, emailBody);
	    StringType body = writer.ToString();

	    writer = new StringWriter();
	    Velocity.Evaluate(context, writer, mailMessageType, emailSubject);
	    StringType subject = writer.ToString();

	    MailMessage.Create(mailMessageType, emailAddress, subject, body, bodyType);
	}
    }
}

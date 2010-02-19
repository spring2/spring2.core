using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.Types;

namespace Spring2.Core.Publication {
    public abstract class Publisher : IPublisher {
	public virtual void Process(IPublicationType publicationType) {
	    UpdatePublicationTypeLastSent(publicationType as PublicationType);
	}

	protected virtual void UpdatePublicationTypeLastSent(PublicationType publicationType) {
	    publicationType.Update(new PublicationTypeData() { LastSentDate = DateTimeType.Now });
	}
    }
}

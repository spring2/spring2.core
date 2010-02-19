using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Publication.DataObject;

namespace Spring2.Core.Publication {
    public interface IPublisher {
	void Process(IPublicationType publicationType);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Publication.DataObject;

namespace Spring2.Core.Publication { 
    public class TestPublicationServiceProvider : IPublisher {
	public TestPublicationServiceProvider() { }
	public void Process(IPublicationType publicationType) { }
    }
}

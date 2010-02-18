using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Publication {
    public class PublicationTypeException : Exception {
	public PublicationTypeException(string message) : 
	    base(message) { }
    }
}

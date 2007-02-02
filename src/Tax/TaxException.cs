using System;
using Spring2.Core.Message;

namespace Spring2.Dss.Tax {

    /// <summary>
    /// Summary description for TaxException.
    /// </summary>
    public class TaxException : Message {
    	public TaxException(String message) : base(message) {
    	}
    }

}

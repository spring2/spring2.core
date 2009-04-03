using System;
using Spring2.Core.Message;

namespace Spring2.Core.Tax {

    /// <summary>
    /// Summary description for TaxException.
    /// </summary>
    public class TaxException : Spring2.Core.Message.Message {
    	public TaxException(String message) : base(message) {
    	}
    }

}

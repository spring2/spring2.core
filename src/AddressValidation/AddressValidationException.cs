using System;
using Spring2.Core.Message;

namespace Spring2.Core.AddressValidation {

    /// <summary>
    /// Summary description for AddressValidationException.
    /// </summary>
    public class AddressValidationException : Spring2.Core.Message.Message {
	public AddressValidationException(String message) : base(message) {
	}
    }

}

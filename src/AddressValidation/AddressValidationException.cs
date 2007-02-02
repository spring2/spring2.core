using System;
using Spring2.Core.Message;

namespace Spring2.Dss.AddressValidation {

    /// <summary>
    /// Summary description for AddressValidationException.
    /// </summary>
    public class AddressValidationException : Message {
	public AddressValidationException(String message) : base(message) {
	}
    }

}

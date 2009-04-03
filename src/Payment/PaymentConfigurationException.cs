using System;

namespace Spring2.Core.Payment {

    /// <summary>
    /// Exception to indicate that there is a configuration problem and that processing will not be able to happen
    /// </summary>
    public class PaymentConfigurationException : PaymentException {
	public PaymentConfigurationException(String message) : base(string.Format("Invalid configuration: {0}", message)) {
	}
    }
	
}
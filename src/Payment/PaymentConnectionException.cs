using System;

namespace Spring2.Core.Payment {

    /// <summary>
    /// Exception to indicate that there was a problem after a request was made and that the result may be unknown and that the payment might have been processed.
    /// </summary>
    public class PaymentConnectionException : PaymentException {
	public PaymentConnectionException(String message) : base(string.Format("Unable to connect to the payment processing subsystem: {0}", message)) {
	}
    }
	
}
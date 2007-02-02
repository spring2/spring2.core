using System;

namespace Spring2.Core.Payment {
    /// <summary>
    /// Base exception for all payment related exceptions
    /// </summary>
    public class PaymentException : Exception {
	public PaymentException(string message) : base(message) {
	}
    }
}
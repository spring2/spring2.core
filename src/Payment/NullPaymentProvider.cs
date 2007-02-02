using System;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.Test {
    /// <summary>
    /// Null object provider so that there can be something to lock in the payment manager
    /// </summary>
    public class NullPaymentProvider : IPaymentProvider {

	public PaymentResult Authorize(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    throw new PaymentConfigurationException("The NullPaymentProvider is not a valid provider.");
	}

	public PaymentResult Charge(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    throw new PaymentConfigurationException("The NullPaymentProvider is not a valid provider.");
	}

    	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    throw new PaymentConfigurationException("The NullPaymentProvider is not a valid provider.");
	}
	
	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new PaymentConfigurationException("The NullPaymentProvider is not a valid provider.");
	}

	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new PaymentConfigurationException("The NullPaymentProvider is not a valid provider.");
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    throw new PaymentConfigurationException("The NullPaymentProvider is not a valid provider.");
	}

    }
}
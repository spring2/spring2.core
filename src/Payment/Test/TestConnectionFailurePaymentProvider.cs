using System;
using Spring2.Core.Types;
using NotImplementedException = System.NotImplementedException;

namespace Spring2.Core.Payment.Test {

    /// <summary>
    /// Summary description for TestPaymentProvider.
    /// </summary>
    public class TestConnectionFailurePaymentProvider : IPaymentProvider {

	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    throw new PaymentConnectionException("connection failure");
	}

	public PaymentResult Charge(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    throw new PaymentConnectionException("connection failure");
	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    throw new PaymentConnectionException("connection failure");
	}
	
    	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new PaymentConnectionException("connection failure");
	}

    	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new PaymentConnectionException("connection failure");
	}
    	
	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    throw new PaymentConnectionException("connection failure");
	}

    }
}

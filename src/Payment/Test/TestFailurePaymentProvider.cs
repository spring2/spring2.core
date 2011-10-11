using System;
using Spring2.Core.Types;
using NotImplementedException = System.NotImplementedException;

namespace Spring2.Core.Payment.Test {

    /// <summary>
    /// Summary description for TestPaymentProvider.
    /// </summary>
    public class TestFailurePaymentProvider : IPaymentProvider {

	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    throw new PaymentFailureException(GetResult());
	}

	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    throw new PaymentFailureException(GetResult());
	}

	public PaymentResult Charge(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    throw new PaymentFailureException(GetResult());
	}

	public PaymentResult Charge(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    throw new PaymentFailureException(GetResult());
	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    throw new PaymentFailureException(GetResult());
	}
	
    	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new PaymentFailureException(GetResult());
	}

    	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new PaymentFailureException(GetResult());
	}
    	
	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    throw new PaymentFailureException(GetResult());
	}

    	private PaymentResult GetResult() {
    	    PaymentResult result = new PaymentResult();
    	    result.ResultCode = "99";
    	    result.ResultMessage = "Decline";
    	    return result;
    	}
    }
}

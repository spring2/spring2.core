using System;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.Test {

    /// <summary>
    /// Summary description for TestPaymentProvider.
    /// </summary>
    public class TestPaymentProvider : IPaymentProvider {

	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    return GetResult();
	}

	public PaymentResult Charge(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    return GetResult();
	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    return GetResult();
	}
	
    	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    return GetResult();
	}

    	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    return GetResult();
	}
    	
	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    return GetResult();
	}

    	private PaymentResult GetResult() {
    	    PaymentResult result = new PaymentResult();
    	    result.ResultCode = "0";
    	    result.ResultMessage = "Success";
    	    return result;
    	}
    	
    }
}

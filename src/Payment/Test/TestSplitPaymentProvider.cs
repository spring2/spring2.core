using System;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.Test {

    /// <summary>
    /// Summary description for TestPaymentProvider.
    /// </summary>
    public class TestSplitPaymentProvider : ISplitPaymentProvider {

	public PaymentResult Authorize(CurrencyType amount, StringType address, StringType postalCode, StringType providerAccountNumber, StringType creditCardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    return GetResult();
	}

	public PaymentResult Charge(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    return GetResult();
	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType transactionId) {
	    return GetResult();
	}

	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount) {
	    return GetResult();
	}

	public PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId) {
	    return GetResult();
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId, CurrencyType amount) {
	    return GetResult();
	}

	public PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber) {
	    return GetResult();
	}

	public PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber) {
	    return GetResult();
	}

    	private PaymentResult GetResult() {
    	    PaymentResult result = new PaymentResult();
    	    result.ResultCode = "00";
    	    result.ResultMessage = "Success";
    	    return result;
    	}
    	
    }
}

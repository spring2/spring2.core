using System;
using Spring2.Core.Types;
using Spring2.Core.Configuration;

namespace Spring2.Core.Payment.Test {

    /// <summary>
    /// Summary description for TestPaymentProvider.
    /// </summary>
    public class TestSplitPaymentProvider : ISplitPaymentProvider {

	public PaymentResult Authorize(CurrencyType amount, StringType address, StringType postalCode, StringType providerAccountNumber, StringType creditCardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Charge(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType totalAmount, CurrencyType commissionableAmount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMaster) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = totalAmount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Credit(StringType providerAccountNumber, CurrencyType amount, StringType invoiceNumber) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount, CurrencyType commissionableAmount, DecimalType originalFractionToSplit) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = refundAmount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId) {
	    PaymentResult result = GetResult();
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId, CurrencyType amount) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Split(StringType sourceAccount, CurrencyType amount, StringType transactionNumber) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    result.AccountNumber = sourceAccount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    result.AccountNumber = sourceAccount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber) {
	    PaymentResult result = GetResult();
	    result.TransactionAmount = amount;
	    result.AccountNumber = recipientAccount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult PingAccount(StringType proPayAccountEmail, StringType proPayPayAccountNumber, StringType ssn) {
	    PaymentResult result = GetResult();
	    result.AccountNumber = proPayPayAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	//public CurrencyType GetBalance(StringType providerAccountNumber) {
	//    PaymentResult result = GetResult();
	//    if(result.ResultCode != "00") {
	//	throw new FundsUnavailableException();
	//    }
	//    return new CurrencyType(1000);
	//}

    	private PaymentResult GetResult() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
    	    PaymentResult result = new PaymentResult();
	    result.ResultCode = currentConfig.Settings["SplitPayment.Test.ResultCode"]; // "00";
	    result.ResultMessage = currentConfig.Settings["SplitPayment.Test.ResultMesage"]; // "Success";
    	    return result;
    	}

	private void CheckForSuccess(PaymentResult result) {
	    if (result.ResultCode != "00") {
		PaymentResult exceptionResult = new PaymentResult();
		exceptionResult.AccountNumber = "TestAccountNumber";
		exceptionResult.ApprovalNumber = "TestApprovalNumber";
		exceptionResult.AuthorizationNumber = "TestAuthNumber";
		exceptionResult.AVSResponseCode = "";
		exceptionResult.CVVResponseCode = "";
		exceptionResult.ReferenceNumber = "TestReference";
		exceptionResult.ResponseCode = 22;
		exceptionResult.ResultCode = "1";
		exceptionResult.ResultMessage = "This is a test failure message.";
		exceptionResult.TransactionAmount = new CurrencyType(100.00);
		exceptionResult.TransactionDateTime = DateTime.Now;
		exceptionResult.TransactionId = "444";
		exceptionResult.ValidAddress = BooleanType.FALSE;
		exceptionResult.ValidCvv = BooleanType.FALSE;
		exceptionResult.ValidPostalCode = BooleanType.FALSE;
		throw new PaymentFailureException(exceptionResult, "Failed to process the payment. {0}: {1}.", exceptionResult.ResultCode, "(ProPay) '" + exceptionResult.ResultMessage + "'");
	    }
	}
    	
    }
}

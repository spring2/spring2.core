using System;
using System.Reflection;
using Spring2.Core.Types;
using Spring2.Core.Configuration;

namespace Spring2.Core.Payment.Test {

    /// <summary>
    /// Summary description for TestPaymentProvider.
    /// </summary>
    public class TestSplitPaymentProvider : ISplitPaymentProvider {

	public PaymentResult Authorize(CurrencyType amount, StringType address, StringType postalCode, StringType providerAccountNumber, StringType creditCardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Authorize.Exception");
	    PaymentResult result = GetResult("Authorize");
	    result.TransactionAmount = amount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Charge(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Charge.Exception");
	    PaymentResult result = GetResult("Charge");
	    result.TransactionAmount = amount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType totalAmount, CurrencyType commissionableAmount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMaster) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.ChargeWithSplit.Exception");
	    PaymentResult result = GetResult("ChargeWithSplit");
	    result.TransactionAmount = totalAmount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Credit(StringType providerAccountNumber, CurrencyType amount, StringType invoiceNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Credit.Exception");
	    PaymentResult result = GetResult("Credit");
	    result.TransactionAmount = amount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount, CurrencyType commissionableAmount, DecimalType originalFractionToSplit) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Refund.Exception");
	    PaymentResult result = GetResult("Refund");
	    result.TransactionAmount = refundAmount;
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Settle.Exception");
	    PaymentResult result = GetResult("Settle");
	    result.AccountNumber = providerAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId, CurrencyType amount) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Void.Exception");
	    PaymentResult result = GetResult("Void");
	    result.TransactionAmount = amount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Split(StringType sourceAccount, CurrencyType amount, StringType transactionNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Split.Exception");
	    PaymentResult result = GetResult("Split");
	    result.TransactionAmount = amount;
	    result.AccountNumber = sourceAccount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.Split.Exception");
	    PaymentResult result = GetResult("Split");
	    result.TransactionAmount = amount;
	    result.AccountNumber = sourceAccount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.VoidSplit.Exception");
	    PaymentResult result = GetResult("VoidSplit");
	    result.TransactionAmount = amount;
	    result.AccountNumber = recipientAccount;
	    CheckForSuccess(result);
	    return result;
	}

	public PaymentResult PingAccount(StringType proPayAccountEmail, StringType proPayPayAccountNumber, StringType ssn) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.PingAccount.Exception");
	    PaymentResult result = GetResult("PingAccount");
	    result.AccountNumber = proPayPayAccountNumber;
	    CheckForSuccess(result);
	    return result;
	}

	public CurrencyType GetBalance(StringType providerAccountNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.GetBalance.Exception");
	    PaymentResult result = GetResult("GetBalance");
	    if(result.ResultCode != "00") {
		throw new FundsUnavailableException();
	    }
	    return new CurrencyType(1000);
	}

	public StringType GetTransactionStatus(StringType accountNumber, StringType transactionNumber) {
	    SpecialExceptionThrowIfNecessary("SplitPayment.Test.GetTransactionStatus.Exception");
	    return "CCDebitSettled";
	}

    	private PaymentResult GetResult(String requestingMethodName) {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
    	    PaymentResult result = new PaymentResult();
	    result.ResultCode = currentConfig.Settings["SplitPayment.Test." + requestingMethodName + ".ResultCode"]; // "00";
	    result.ResultMessage = currentConfig.Settings["SplitPayment.Test." + requestingMethodName + ".ResultMessage"]; // "Success";
	    if (result.ResultCode == null || result.ResultCode == String.Empty) {
		result.ResultCode = currentConfig.Settings["SplitPayment.Test.ResultCode"];
		if (result.ResultCode == null || result.ResultCode == String.Empty) {
		    result.ResultCode = "00";
		}
	    }
	    if (result.ResultMessage == null || result.ResultMessage == String.Empty) {
		result.ResultMessage = currentConfig.Settings["SplitPayment.Test.ResultMessage"];
		if (result.ResultMessage == null || result.ResultMessage == String.Empty) {
		    result.ResultMessage = "SUCCESS";
		}
	    }
    	    return result;
    	}

	private void SpecialExceptionThrowIfNecessary(String configSetting) {
	    Exception ex = GetSpecialException(configSetting);
	    if(ex != null) {
		throw ex;
	    }
	}

	private Exception GetSpecialException(String configSetting) {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    String specialExceptionName = currentConfig.Settings[configSetting];
	    Exception ex = null;
	    if(specialExceptionName != null && specialExceptionName != String.Empty) {
		char[] splitChar = { ',' };
		String[] classAndParams = specialExceptionName.Split(splitChar);
		Type type = Type.GetType(classAndParams[0]);
		if(type == null) {
		    return new Exception("Unable to reflect exception '" + classAndParams[0] + "'");
		}
		Type[] typesOfConstructorParam = classAndParams.Length>1?new Type[classAndParams.Length - 1]:Type.EmptyTypes;
		object[] paramValues = new object[classAndParams.Length - 1];
		for(int i=1;i<classAndParams.Length;i++) {
		    typesOfConstructorParam[i-1] = Type.GetType("System.String");
		}
		ConstructorInfo constructorInfo = type.GetConstructor(typesOfConstructorParam);
		object o = constructorInfo.Invoke(paramValues);
		ex = ( Exception )o;
	    }
	    return ex;
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
		exceptionResult.ResultCode = result.ResultCode;
		exceptionResult.ResultMessage = result.ResultMessage;
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

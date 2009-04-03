using System;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.Test {
    /// <summary>
    /// Null object provider so that there can be something to lock in the payment manager
    /// </summary>
    public class NullSplitPaymentProvider : ISplitPaymentProvider {

	public PaymentResult Authorize(CurrencyType amount, StringType address, StringType postalCode, StringType providerAccountNumber, StringType creditCardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Charge(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType totalAmount, CurrencyType commissionableAmount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMaster) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Credit(StringType providerAccountNumber, CurrencyType amount, StringType invoiceNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount, CurrencyType commissionableAmount, DecimalType originalFractionToSplit) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId, CurrencyType amount) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Split(StringType sourceAccount, CurrencyType amount, StringType transactionNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult PingAccount(StringType proPayAccountEmail, StringType proPayPayAccountNumber, StringType ssn) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public CurrencyType GetBalance(StringType providerAccountNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public StringType GetTransactionStatus(StringType accountNumber, StringType transactionNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

    }
}
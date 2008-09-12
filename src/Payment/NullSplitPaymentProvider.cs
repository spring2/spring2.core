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

	public PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMasterAccount) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Credit(StringType providerAccountNumber, CurrencyType amount, StringType invoiceNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount, DecimalType originalSplitFraction) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId, CurrencyType amount) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

	public PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber) {
	    throw new PaymentConfigurationException("The NullSplitPaymentProvider is not a valid provider.");
	}

    }
}